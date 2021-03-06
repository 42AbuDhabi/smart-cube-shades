
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


void main()
{
    float time = iTime * 0.05;
    float zoom = 2.0;
    float angle = iTime * 0.003;


    vec2 st = uv - 0.5;

    st.y *= iResolution.y / iResolution.x;


    st *= 4.0;
    st.x = abs(mod(st.x, 2.0) - 1.0f);
    st *= 1.5;


    // rotate around center
    st.x -= 0.5;
    st *= rotate2d(angle);
    st.x += 0.5;

    float p = 0.5f;


    float col = cos(mod((cos(st.x * 100.0f * zoom) * 0.5 + 0.5) + time * 2.0, 2.0) * PI);
    float twotex = 0.2;

    col = (col * 0.5)+0.5;
    col = smoothstep(p -twotex, p + twotex, cos(col * ((uv.y - 0.5) * 0.4) + 0.5) - 0.4);
    col *= 0.2;

    fragColor = vec4( vec3(col), 1.0);
}
  