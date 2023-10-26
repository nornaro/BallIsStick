extends Timer

@onready var scene = preload("res://line_2d.tscn")
var lines = []
var colors = [Color.RED,Color.GREEN,Color.BLUE]

func _ready():
	lines.resize(3)
	for i in lines.size():
		lines[i] = scene.instantiate()
		add_child(lines[i])
	
func _on_timeout():
	#$"../../Funnel".SpawnBall()
	pass
	
#func _process(delta):
#	var line = lines[randi_range(0,lines.size()-1)]
#	line.draw_random_line()
#	line.default_color=colors[randi_range(0,2)]
