using Godot;

public partial class RoundTimer : Timer
{
	public override void _Ready()
	{
		CheckBallVelocities();
	}

	public bool CheckBallVelocities()
	{
		var balls = GetTree().GetNodesInGroup("ball");
		foreach (RigidBody2D ball in balls)
		{
			if (ball.LinearVelocity.Length() < 0.01f)
			{
				ball.LinearVelocity=Vector2.Zero;
				ball.AngularVelocity=0;
				var newMaterial = (PhysicsMaterial)ball.PhysicsMaterialOverride.Duplicate(true);
				newMaterial.Absorbent=true;
				newMaterial.Bounce=1;
				ball.PhysicsMaterialOverride=newMaterial;
				ball.Sleeping=true;
				continue;
			}
			//Stop();
			Start(5);
			return true;
		}
		return false;
	}
	private void _on_timeout()
	{
		if (CheckBallVelocities())
		{
			return;
		}
		Stop();
		var balls = GetTree().GetNodesInGroup("ball");
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
		var holes = GetTree().GetNodesInGroup("pointer");
		int pointcount = (int)GetParent().Get("pointcount");
		foreach (Area2D hole in holes)
		{
			pointcount += (int)hole.Get("points");
			hole.Set("points", 0);
		}
		GetParent().Set("pointcount", pointcount);
		GetNode<RichTextLabel>("../Control/Points").Text = pointcount.ToString();
	}
}
