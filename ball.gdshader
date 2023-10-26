shader_type canvas_item;

// Light parameters
uniform vec2 light_pos = vec2(100.0, 100.0);
uniform vec4 light_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform float light_intensity = 1.0;

// Material parameters
uniform float metallic = 0.5;
uniform float roughness = 0.3;

void fragment() {
    // Calculate light direction
    vec2 light_dir = light_pos - FRAGCOORD.xy;
    float distance_to_light = length(light_dir);
    light_dir = normalize(light_dir);

    // Simple Lambertian shading for diffuse
    float diff = max(0.0, dot(light_dir, vec2(0.0, -1.0)));

    // Simple Phong-like reflection for specular
    vec2 reflect_dir = reflect(-light_dir, vec2(0.0, -1.0));
    float spec = pow(max(0.0, dot(reflect_dir, vec2(0.0, -1.0))), 1.0 / max(roughness, 0.001));

    // Mix between metallic and diffuse
    vec4 base_color = mix(COLOR, light_color, metallic);

    // Final color output
    vec4 col = (base_color * diff + light_color * spec * (1.0 - metallic)) * light_intensity;
    col.a = COLOR.a;

    COLOR = col;
}
