using Godot;
using System;

public partial class CountDown : RichTextLabel
{
	Timer myTimer;

	public override void _Ready()
	{
		myTimer = GetNode<Timer>("../../RoundTimer");
	}
	public override void _Process(double delta)
	{
		CheckRemainingTime();
	}
	public void CheckRemainingTime()
	{
		if (myTimer != null)
		{
			Visible = true;
			var remainingTime = myTimer.TimeLeft;
			Text = ((int)remainingTime).ToString();			
			if (remainingTime == 0)
			{
				Visible = false;
				SetProcess(false);
				return;
			}
			
		}
	}
}
