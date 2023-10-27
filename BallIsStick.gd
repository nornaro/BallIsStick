extends Node2D

@export var pointcount = 0


func add_point(value):
	pointcount += value
	
func get_point():
	return pointcount
	
func _ready() -> void:
	set_mouse_confined_to_window()

func set_mouse_confined_to_window() -> void:
	Input.set_mouse_mode(Input.MOUSE_MODE_CONFINED)

func _on_window_focus_out() -> void:
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)

func _on_window_focus_in() -> void:
	set_mouse_confined_to_window()
