extends Line2D

var breakpoints : Array = []
var fade_time = 1.0
var decrement = 0.01

func _process(delta):
	fade_time -= decrement * delta
	material.set_shader_parameter("fade_time", fade_time)

func draw_random_line():
	breakpoints.clear()
	var current_point = Vector2(randf() * 1, randf() * 860)-Vector2(190,440)
	breakpoints.append(current_point)
	var num_turns = randi() % 11 + 3
	
	for _i in range(num_turns):
		var angle_degrees = [90, 45, 30][randi() % 3]
		angle_degrees *= -1 if randf() < 0.5 else 1
		var angle_radians = deg_to_rad(angle_degrees)
		var distance = randf() * 200 + 50
		var new_point = current_point + Vector2(cos(angle_radians), sin(angle_radians)) * distance
		breakpoints.append(new_point)
		current_point = new_point
	
	self.points = breakpoints
