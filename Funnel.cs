using Godot;
using System;

public partial class Funnel : Sprite2D
{
	private PackedScene scene;
	private int ball_counter = 0;
	private int round_counter = 3;
	private int new_cost = 100;

	public override void _Ready()
	{
		scene = (PackedScene)ResourceLoader.Load("res://ball.tscn");
		NewPosition();
	}

	private void NewPosition()
	{
		Position = new Vector2((int)(GD.Randi() % 341) - 170, Position.Y);
	}

	private void SpawnBall()
	{
		var ball = (Node2D)scene.Instantiate();
		ball.GlobalPosition = GlobalPosition - new Vector2(0, -10);
		GetNode("../Balls").AddChild(ball);		
		UpdateCounterText();
	}
	
	public void UpdateCounterText()
	{
		var control = GetNode<RichTextLabel>("../Control/Counter");
		if (control == null)
		{
			return;
		}

		var ballCount = GetTree().GetNodesInGroup("ball").Count;
		control.Text = $"[color=white]{ballCount.ToString()}[/color]";
	}
	private void _on_start_pressed()
	{	
		if (!GetNode<Timer>("../RoundTimer").IsStopped())
		{
			return;			
		}
		GetNode<Timer>("Timer").Start();
		ball_counter = (int)(GD.Randi() % 5) + 1;
		round_counter -= 1;
		GetNode<Timer>("../RoundTimer").Start();
		GetNode<RichTextLabel>("../Control/CountDown").SetProcess(true);
	}

	private void _on_new_pressed()
	{
		if (!GetNode<Timer>("../RoundTimer").IsStopped())
		{
			return;
		}
		GetParent().Set("pointcount", (int)GetParent().Get("pointcount")-new_cost);
		new_round();
	}
	private void new_round()
	{
		NewPosition();
		clear();
		round_counter = 3;
	}

	private void _on_reset_pressed()
	{
		GetParent().Set("pointcount", 0);
		new_round();
		GetNode<Timer>("Timer").Stop();
		GetNode<Timer>("../RoundTimer").Stop();
	}

	private void clear()
	{
		GetTree().CallGroup("ball", "queue_free");
		GetTree().CallGroup("rod", "queue_free");
	}

	private void _on_timer_timeout()
	{
		if (ball_counter > 0)
		{
			SpawnBall();
			ball_counter--;
			return;
		}
		GetNode<Timer>("Timer").Stop();
	}
	private void _on_round_timer_timeout()
	{
		if (round_counter != 0)
		{
			return;
		}
		new_round();
	}
}



