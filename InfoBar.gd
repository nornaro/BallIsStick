extends Area2D


func _on_mouse_entered():
	$PropertyName.show()
	$PropertyValue.show()
	pass # Replace with function body.


func _on_mouse_exited():
	$PropertyName.hide()
	$PropertyValue.hide()
	pass # Replace with function body.
