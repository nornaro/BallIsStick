using Godot;

public partial class HoleArea : Area2D
{
	public int CountPoints()
	{
		int points = 0;
		Godot.Collections.Array<Node2D> overlappingBodies = GetOverlappingBodies();

		foreach (RigidBody2D body in overlappingBodies)
		{
			if (body.Freeze)
			{
				continue;
			}
			if (GetParent<RigidBody2D>().GravityScale == 6)
			{
				points -= 50;
				continue;
			}
			points += (int)GetParent<RigidBody2D>().GravityScale;
		}
		return points;
	}
}
