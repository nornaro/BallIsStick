using Godot;
using System;
using System.Linq;

public partial class Field : MeshInstance2D
{

	private bool inside = true;
	private PackedScene scene;
	
	public override void _Ready()
	{
		scene = (PackedScene)GD.Load("res://rod.tscn");
	}

	public override void _Input(InputEvent @event)
	{
		if (!@event.IsActionReleased("place"))
			if (GetTree().GetNodesInGroup("selected").Count != 0)
				GetTree().GetFirstNodeInGroup("selected").RemoveFromGroup("selected");
		if (!inside)
			return;

		if (@event.IsActionPressed("place"))
		{
			var rod = scene.Instantiate();
			((Node2D)rod).GlobalPosition = GetGlobalMousePosition();
			GetNode("Rods").AddChild(rod);
			rod.AddToGroup("selected");
		}
	}

	private void _on_area_2d_mouse_entered()
	{
		inside = false;
	}

	private void _on_area_2d_mouse_exited()
	{
		inside = true;
	}
}
