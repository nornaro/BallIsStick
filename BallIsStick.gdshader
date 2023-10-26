shader_type canvas_item;

uniform float fade_time : hint_range(0, 1) = 0.5; // You can control this from GDScript

void fragment() {
	COLOR.a *= fade_time;
}