extends Node2D

@export var pointcount = 0


func add_point(value):
	pointcount += value
	
func get_point():
	return pointcount
