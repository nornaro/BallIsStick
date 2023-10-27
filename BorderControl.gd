extends Control

func _on_gravity_value_changed(value):
	$PropertyName.text = "Gravity"
	$PropertyValue.text = str(value)

func _on_mass_value_changed(value):
	$PropertyName.text = "Mass"
	$PropertyValue.text = str(value)

func _on_bounce_value_changed(value):
	$PropertyName.text = "Bounce"
	$PropertyValue.text = str(value)

func _on_friction_value_changed(value):
	$PropertyName.text = "Friction"
	$PropertyValue.text = str(value)

func _on_property_value_text_changed(new_text):
	get_node(str($PropertyName.text)).value=float($PropertyValue.text)

func _on_property_name_gui_input(event):
	if event is InputEventMouseButton and event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
		$ItemList.show()

func _on_item_list_item_clicked(index, _at_position, _mouse_button_index):
	$ItemList.hide()
	var text = $ItemList.get_item_text(index)
	$PropertyName.text = text
	var node = get_node(text)
	$PropertyValue.text = str(node.value)

func _on_area_2d_mouse_entered():
	$PropertyName.show()
	$PropertyValue.show()

func _on_area_2d_mouse_exited():
	$PropertyName.hide()
	$PropertyValue.hide()
