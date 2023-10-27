extends Area2D

var points

func _ready():
	points = 0

func _on_body_entered(_body):
	if $"..".gravity_scale == 6:
		points -= 50
		return
	points += $"..".gravity_scale

func _on_body_exited(_body):
	if $"..".gravity_scale == 6:
		points += 50
		return
	points -= $"..".gravity_scale
