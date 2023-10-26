extends CollisionShape2D

func _ready():
	get_parent().continuous_cd = true
	get_parent().set_continuous_collision_detection_mode(2)
