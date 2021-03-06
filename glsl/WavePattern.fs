#version 410 core
in vec2 uv;
out vec4 fragColor;

uniform vec2 iResolution;
uniform float iTime; 

float PI = 3.14159265359;

mat2 rotate2d(float angle)
{
    angle *= 2. * PI;
    return mat2(cos(angle), sin(angle), -sin(angle), cos(angle));
}

// define the colors
vec3 col1 = vec3(0.451,0.059,1.);
vec3 col2 = vec3(1.,0.349,0.239);

void main()
{
    float zoom = 1.3;
    float time = iTime * 0.1; 
    
    vec2 st = uv - 0.5;
    st.y *= iResolution.y / iResolution.x;

    float col = 1.0f;
    
    st *= 200.0 * zoom;
    

    float size = (cos((st.x * 0.01) + time * PI * 2.) * 0.5 + 0.5) * 0.3 + 0.2;

    vec2 orgSt = st;

    // let's go sideways
    st *= rotate2d(0.125);
    
    // and offset
    st.x += (iTime * 0.4);

    //st *= rotate2d(angle);

    float id = st.x +floor(st.y);

    st = fract(st) - 0.5;
    vec2 lst= st;

    if(mod(id, 2.0) > 1.0) 
        lst.y *= 2.0;
    else  
        lst.x *= 2.0;

    col = length(lst);
    col = smoothstep(size, size + 0.2, col);

    fragColor = vec4(mix(col1, col2, col),1.0);
}
  