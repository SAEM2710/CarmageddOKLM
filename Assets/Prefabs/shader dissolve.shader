Shader "Custom/Transparent"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
      _SliceAmount ("Slice Amount", Range(0.0, 1.0)) = 0.5
       _BurnSize ("Burn Size", Range(0.0, 1.0)) = 0.15
 _BurnRamp ("Burn Ramp (RGB)", 2D) = "white" {}
    
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
      LOD 200
	//Cull Off

        CGPROGRAM
 
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0
 
  sampler2D _MainTex;
      sampler2D _SliceGuide;
      float _SliceAmount;
 sampler2D _BurnRamp;
 float _BurnSize;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color; 
          
        struct Input {
            float2 uv_MainTex;
          float2 uv_SliceGuide;
          float _SliceAmount;
        };
   
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            clip(tex2D (_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }

    FallBack "Standard"
}