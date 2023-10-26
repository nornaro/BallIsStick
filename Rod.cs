using Godot;
using System;
using System.Linq;

public partial class Rod : StaticBody2D
{
	private int speed = 20;
	private bool mousePointOver = false;
	private Vector2 velocity;

	public override void _PhysicsProcess(double delta)
	{
		if(GetGroups().Contains("selected"))
		{
			CallDeferred("PrepareMoveTowardsMouse", delta);
			CallDeferred("DeferredMoveAndCollide", velocity);
			velocity = Vector2.Zero;
		}
	}

	public void PrepareMoveTowardsMouse(double delta)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direction = mousePosition - GlobalPosition;

		if (direction.Length() < 1.0)
			return;

		velocity = direction * (float)(speed * delta);
	}
	
	private void DeferredMoveAndCollide(Vector2 velocity)
	{
		MoveAndCollide(velocity);
	}
	
	public override void _Input(InputEvent @event)
	{
		if (!mousePointOver)
			return;

		if (@event.IsActionReleased("remove"))
			QueueFree();
	}

	private void _on_area_2d_mouse_entered()
	{
		mousePointOver = true;
	}

	private void _on_area_2d_mouse_exited()
	{
		mousePointOver = false;
	}

	private void _on_area_2d_body_exited(Node2D body)
	{
		if (body.GetClass() != "RigidBody2D")
			return;
		
		var rigidBody = (RigidBody2D)body;
		
		if (rigidBody.LinearVelocity.Length() <= 1000)
			rigidBody.LinearVelocity *= 1.1f;
	}
}
