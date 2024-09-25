extends Control

func _on_volume_value_changed(value:float) -> void:
	AudioServer.set_bus_volume_db(0,value/5)


#func _on_check_box_toggled(toggled_on:bool) -> void:
#	AudioServer.set_bus_volume_db(0,-80)


func _on_mute_toggled(toggled_on):
	pass # Replace with function body.
