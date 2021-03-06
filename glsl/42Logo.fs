#version 410 core
in vec2 uv;
out vec4 fragColor;



/*
The 42 Logo in GLSL
by Sander ter Braak
*/

uniform vec2 iResolution;
uniform float iTime; 

float PI = 3.14159265359;

mat2 rotate2d(float angle)
{
    angle *= 2. * PI;
    return mat2(cos(angle), sin(angle), -sin(angle), cos(angle));
}

float rect(vec2 p , vec2 pos , vec2 size)
{
    // smoothing
    float s = 0.002;

    float x = smoothstep(0.5, 0.5 + (s / size.x), abs(p.x- pos.x) / size.x);
    float y = smoothstep(0.5, 0.5 + (s / size.y), abs(p.y- pos.y) / size.y) ;

    return 1.0 - max(x,y);
}

// twist a rect so we can make the logo
float paralellogram(vec2 p , vec2 pos , vec2 size, float angle)
{
    if(angle > 0.) pos.x += p.y * angle;
    else pos.y -= p.x * angle;
    return rect(p, pos, size);
}

void main()
{

    float zoom = 0.016; 
    float angle = 0.05; 
    float spacing = 0.91; 
    float angle2 = 0.9; 
    float time = 0.0f;


    vec2 st = uv - 0.5;

    st.y *= iResolution.y / iResolution.x;

    st *= rotate2d(angle);
    st *= zoom * 200.0;


    // let's offset the X by half
    st.x += (step(mod(st.y, 2.0), 1.0) - 0.5)   * iTime * 0.015 * cos(floor(st.y) * 3.014 * 2.0);

    
    st = fract(st) - 0.5;
    st *= spacing + 1.0f;



    float col = 0.0;

    // let's dot the 4 first
    col += paralellogram(st, vec2(-0.3757,0.1852), vec2(0.1775, 0.4092), 0.867);
    col += rect(st, vec2(-0.2155, -0.1021), vec2(0.532, 0.1655));
    col += rect(st, vec2(-0.038, -0.2874), vec2(0.177, 0.205));



    // and then the 2
    col += rect(st, vec2(0.304, 0.083), vec2(0.355, 0.614));

    // remove the elements from the rect
    col -= paralellogram(st, vec2(0.215, -0.063), vec2(0.1776, 0.203), -1.153);
    col -= paralellogram(st, vec2(0.393, -0.473), vec2(0.1776, 0.203), -1.153);


    // let's make sure that we stay in the 0 and 1 range
    col = clamp(col, 0.0, 1.0);

    col = abs(1.0 - (0.8 + (uv.y * 0.52)) - col * 0.2);

    fragColor = vec4(vec3(col), 1.0);
} 
  