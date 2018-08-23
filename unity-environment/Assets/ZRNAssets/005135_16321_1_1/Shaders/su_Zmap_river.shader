// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3068,x:33245,y:32689,varname:node_3068,prsc:2|diff-5442-RGB,spec-5537-OUT,gloss-812-OUT;n:type:ShaderForge.SFN_Color,id:5442,x:32820,y:32530,ptovrint:False,ptlb:main_color,ptin:_main_color,varname:node_5442,prsc:2,glob:False,c1:0.3294118,c2:0.4156863,c3:0.4431373,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8605,x:32607,y:32818,varname:node_8605,prsc:2,tex:2d1aa6f28e9894b4395e59af116e7eca,ntxv:3,isnm:False|UVIN-4812-UVOUT,TEX-1911-TEX;n:type:ShaderForge.SFN_Slider,id:812,x:32756,y:32730,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_node_4390_copy,prsc:2,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2dAsset,id:1911,x:31886,y:32891,ptovrint:False,ptlb:nise_texture,ptin:_nise_texture,varname:node_1911,tex:2d1aa6f28e9894b4395e59af116e7eca,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:4812,x:32392,y:32575,varname:node_4812,prsc:2,spu:1,spv:0.25|DIST-2522-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8227,x:32007,y:32721,ptovrint:False,ptlb:tex_scroll_A,ptin:_tex_scroll_A,varname:node_8227,prsc:2,glob:False,v1:0.15;n:type:ShaderForge.SFN_Time,id:5963,x:32007,y:32575,varname:node_5963,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2522,x:32187,y:32575,varname:node_2522,prsc:2|A-5963-TSL,B-8227-OUT;n:type:ShaderForge.SFN_Tex2d,id:4862,x:32607,y:32935,varname:node_4862,prsc:2,tex:2d1aa6f28e9894b4395e59af116e7eca,ntxv:0,isnm:False|UVIN-4252-UVOUT,TEX-1911-TEX;n:type:ShaderForge.SFN_Panner,id:4252,x:32417,y:33033,varname:node_4252,prsc:2,spu:1,spv:0.1|DIST-4457-OUT;n:type:ShaderForge.SFN_ValueProperty,id:819,x:32040,y:33179,ptovrint:False,ptlb:tex_scroll_B,ptin:_tex_scroll_B,varname:_node_8227_copy,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_Time,id:1455,x:32040,y:33033,varname:node_1455,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4457,x:32220,y:33033,varname:node_4457,prsc:2|A-1455-TSL,B-819-OUT;n:type:ShaderForge.SFN_Multiply,id:5537,x:32834,y:32818,varname:node_5537,prsc:2|A-8605-RGB,B-4862-RGB;proporder:5442-812-1911-8227-819;pass:END;sub:END;*/

Shader "su/su_Zmap_river" {
    Properties {
        _main_color ("main_color", Color) = (0.3294118,0.4156863,0.4431373,1)
        _Gloss ("Gloss", Range(0, 1)) = 0.5
        _nise_texture ("nise_texture", 2D) = "white" {}
        _tex_scroll_A ("tex_scroll_A", Float ) = 0.15
        _tex_scroll_B ("tex_scroll_B", Float ) = 0.1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
			uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _main_color;
            uniform float _Gloss;
            uniform sampler2D _nise_texture; uniform float4 _nise_texture_ST;
            uniform float _tex_scroll_A;
            uniform float _tex_scroll_B;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 node_5963 = _Time + _TimeEditor;
                float2 node_4812 = (i.uv0+(node_5963.r*_tex_scroll_A)*float2(1,0.25));
                float4 node_8605 = tex2D(_nise_texture,TRANSFORM_TEX(node_4812, _nise_texture));
                float4 node_1455 = _Time + _TimeEditor;
                float2 node_4252 = (i.uv0+(node_1455.r*_tex_scroll_B)*float2(1,0.1));
                float4 node_4862 = tex2D(_nise_texture,TRANSFORM_TEX(node_4252, _nise_texture));
                float3 specularColor = (node_8605.rgb*node_4862.rgb);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _main_color.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
			uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _main_color;
            uniform float _Gloss;
            uniform sampler2D _nise_texture; uniform float4 _nise_texture_ST;
            uniform float _tex_scroll_A;
            uniform float _tex_scroll_B;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 node_5963 = _Time + _TimeEditor;
                float2 node_4812 = (i.uv0+(node_5963.r*_tex_scroll_A)*float2(1,0.25));
                float4 node_8605 = tex2D(_nise_texture,TRANSFORM_TEX(node_4812, _nise_texture));
                float4 node_1455 = _Time + _TimeEditor;
                float2 node_4252 = (i.uv0+(node_1455.r*_tex_scroll_B)*float2(1,0.1));
                float4 node_4862 = tex2D(_nise_texture,TRANSFORM_TEX(node_4252, _nise_texture));
                float3 specularColor = (node_8605.rgb*node_4862.rgb);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _main_color.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
