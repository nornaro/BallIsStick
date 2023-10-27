using Godot;

public partial class RoundTimer : Timer
{
	public override void _Process(double delta)
	{
		if(CheckBallVelocities())
		{
			Start(2);
		}
	}
	public bool CheckBallVelocities()
	{
		var balls = GetTree().GetNodesInGroup("ball");
		foreach (RigidBody2D ball in balls)
		{
			if (ball.Position.Y < 850)
			{
				return true;
			}
			if (ball.LinearVelocity.Length() > 0.5f)
			{
				return true;
			}
			ball.LinearVelocity=Vector2.Zero;
			ball.AngularVelocity=0;
			var newMaterial = (PhysicsMaterial)ball.PhysicsMaterialOverride.Duplicate(true);
			newMaterial.Absorbent=true;
			newMaterial.Bounce=1;
			ball.PhysicsMaterialOverride=newMaterial;
			ball.Sleeping=true;
		}
		return false;
	}
	private void _on_round_timer_timeout()
	{
		var balls = GetTree().GetNodesInGroup("ball");
		var holes = GetTree().GetNodesInGroup("pointer");
		int pointcount = (int)GetParent().Get("pointcount");
		foreach (HoleArea hole in holes)
		{
			pointcount += hole.CountPoints();
		}
		if (GetNode<Funnel>("../Funnel").get_round())
		{
			GetNode<Funnel>("../Funnel").new_round();
		}
		foreach (RigidBody2D ball in balls)
		{
			if (ball.LinearVelocity.Length() < 1f)
			{
				if (ball.Position.Y < 850)
				{
					continue;
				}
				ball.LinearVelocity=Vector2.Zero;
				ball.AngularVelocity=0;
				ball.Freeze=true;
				continue;
			}
		}
		GetParent().Set("pointcount", pointcount);
		GD.Print(pointcount);
		GetNode<RichTextLabel>("../Control/Points").Text = pointcount.ToString();
	}
}
