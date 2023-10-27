extends Area2D

class_name Hole

func count_points():
	var points = 0
	for body in get_overlapping_bodies():
		if body.gravity_scale == 6:
			points -= 50
			continue
		points += body.gravity_scale
	return points

