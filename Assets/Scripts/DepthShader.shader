Shader "Custom/DepthShader"
{
    SubShader{
        Tags {"Queue" = "Geometry-1"}
        ColorMask 0
        ZWrite On
        Pass {}
    }    
}