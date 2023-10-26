extends Node

var values = [0,0,1,1,1,1,2,2,3,4,5,6,6]
var colors = [Color.WHITE,Color.YELLOW,Color.DEEP_SKY_BLUE,Color.GREEN,Color.DEEP_PINK,Color.RED,Color.BLACK]

func _ready():
	var children = get_children()
	for child in children:
		var value = values.pick_random()
		child.gravity_scale = value
		child.modulate = colors[value]
		if value == 6:
			child.get_node("Label").text = "X"
			continue
		child.get_node("Label").text = str(value)


func _on_new_pressed():
	if !$"../RoundTimer".is_stopped():
		return
	_ready()


func _on_reset_pressed():
	_on_new_pressed();
