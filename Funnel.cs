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
		var ball = (RigidBody2D)scene.Instantiate();
		ball.GlobalPosition = GlobalPosition - new Vector2(0, -10);
		ball.GravityScale = (float)GetNode<VSlider>("../Border/BorderControl/Gravity").Value;
		ball.Mass = (float)GetNode<VSlider>("../Border/BorderControl/Mass").Value;
		ball.Inertia = (float)GetNode<VSlider>("../Border/BorderControl/Mass").Value;
		ball.PhysicsMaterialOverride.Bounce = (float)GetNode<VSlider>("../Border/BorderControl/Bounce").Value;
		ball.PhysicsMaterialOverride.Friction = (float)GetNode<VSlider>("../Border/BorderControl/Friction").Value;

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
	public void new_round()
	{
		NewPosition();
		clear();
		round_counter = 3;
	}
	public bool get_round()
	{
		return (round_counter == 0);
	}
	private void _on_reset_pressed()
	{
		GetParent().Set("pointcount", 0);
		new_round();
		GetNode<Timer>("Timer").Stop();
		GetNode<Timer>("../RoundTimer").Stop();
		GetNode<RichTextLabel>("../Control/Points").Text = "0";
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
}



