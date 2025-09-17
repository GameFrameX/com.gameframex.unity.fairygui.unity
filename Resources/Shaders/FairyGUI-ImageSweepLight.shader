// FairyGUI扫光效果Shader
// 基于FairyGUI-Image.shader扩展，添加扫光效果

Shader "FairyGUI/ImageSweepLight"
{
    Properties
    {
        // 主纹理
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

        // FairyGUI标准参数
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        _BlendSrcFactor ("Blend SrcFactor", Float) = 5
        _BlendDstFactor ("Blend DstFactor", Float) = 10

        // 扫光参数
        [Header(Sweep Light Settings)]
        _LightTime ("Light Time", Float) = 0.6
        _LightThick ("Light Thick", Float) = 0.3
        _NextTime ("Next Light Time", Float) = 2
        _LightAngle ("Light Angle", Range(0, 360)) = 45
        _LightIntensity ("Light Intensity", Range(0, 3)) = 1.4

        // 像素化参数
        [Header(Pixelation Settings)]
        _PixelSize ("Pixel Size", Float) = 1
    }

    SubShader
    {
        LOD 100

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog
        {
            Mode Off
        }
        Blend [_BlendSrcFactor] [_BlendDstFactor], One One
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma multi_compile _ COMBINED
            #pragma multi_compile _ GRAYED COLOR_FILTER
            #pragma multi_compile _ CLIPPED SOFT_CLIPPED ALPHA_MASK
            #pragma multi_compile _ PIXELATED
            #pragma multi_compile _ SWEEP_LIGHT
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            /// <summary>
            /// 顶点输入结构体
            /// </summary>
            struct appdata_t
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float4 texcoord : TEXCOORD0;
            };

            /// <summary>
            /// 顶点到片元结构体
            /// </summary>
            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float4 texcoord : TEXCOORD0;

                #ifdef CLIPPED
                    float2 clipPos : TEXCOORD1;
                #endif

                #ifdef SOFT_CLIPPED
                    float2 clipPos : TEXCOORD1;
                #endif
            };

            // 纹理和参数声明
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            #ifdef COMBINED
                sampler2D _AlphaTex;
            #endif

            CBUFFER_START(UnityPerMaterial)
                #ifdef CLIPPED
                float4 _ClipBox = float4(-2, -2, 0, 0);
                #endif

                #ifdef SOFT_CLIPPED
                float4 _ClipBox = float4(-2, -2, 0, 0);
                float4 _ClipSoftness = float4(0, 0, 0, 0);
                #endif

                #ifdef PIXELATED
                float _PixelSize;
                #endif

                // 扫光参数
                half _LightTime;
                half _LightThick;
                half _NextTime;
                half _LightAngle;
                half _LightIntensity;
            CBUFFER_END

            #ifdef COLOR_FILTER
                float4x4 _ColorMatrix;
                float4 _ColorOffset;
                float _ColorOption = 0;
            #endif

            /// <summary>
            /// 顶点着色器
            /// </summary>
            /// <param name="v">顶点输入数据</param>
            /// <returns>处理后的顶点数据</returns>
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                #if !defined(UNITY_COLORSPACE_GAMMA) && (UNITY_VERSION >= 550)
                    o.color.rgb = GammaToLinearSpace(v.color.rgb);
                    o.color.a = v.color.a;
                #else
                o.color = v.color;
                #endif

                #ifdef CLIPPED
                    o.clipPos = mul(unity_ObjectToWorld, v.vertex).xy * _ClipBox.zw + _ClipBox.xy;
                #endif

                #ifdef SOFT_CLIPPED
                    o.clipPos = mul(unity_ObjectToWorld, v.vertex).xy * _ClipBox.zw + _ClipBox.xy;
                #endif

                return o;
            }

            /// <summary>
            /// 片元着色器 - 实现扫光效果
            /// </summary>
            /// <param name="i">顶点着色器输出的数据</param>
            /// <returns>最终的像素颜色</returns>
            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.texcoord.xy / i.texcoord.w;

                #ifdef PIXELATED
                    float2 texSize = 1.0 / _MainTex_TexelSize.xy;
                    float2 pixelatedUV = floor(uv * texSize / _PixelSize) * _PixelSize / texSize;
                    uv = pixelatedUV;
                #endif

                // 采样主纹理
                fixed4 col = tex2D(_MainTex, uv) * i.color;

                #ifdef COMBINED
                    col.a *= tex2D(_AlphaTex, i.texcoord.xy / i.texcoord.w).g;
                #endif

                #ifdef GRAYED
                    fixed grey = dot(col.rgb, fixed3(0.299, 0.587, 0.114));
                    col.rgb = fixed3(grey, grey, grey);
                #endif

                // 扫光效果计算
                #ifdef SWEEP_LIGHT
                    // 计算当前时间在扫光周期中的位置
                    fixed currentTimePassed = fmod(_Time.y, _LightTime + _NextTime);
                    
                    // 只在扫光时间内计算效果
                    if (currentTimePassed <= _LightTime)
                    {
                        // 计算扫光进度 (0-1)
                        fixed lightProgress = currentTimePassed / _LightTime;
                        
                        // 将角度转换为弧度，并标准化到0-2π范围
                        fixed angleRad = fmod(_LightAngle, 360.0) * 0.0174532925; // 度转弧度
                        
                        // 计算扫光方向向量 (单位向量)
                        fixed2 lightDir = fixed2(cos(angleRad), sin(angleRad));
                        
                        // 将UV坐标转换为以中心为原点的坐标系 (-0.5 到 0.5)
                        fixed2 centeredUV = uv - 0.5;
                        
                        // 计算扫光线从对角线一端扫到另一端的距离
                        // 使用对角线长度作为扫光范围，确保完全覆盖整个纹理
                        fixed diagonalLength = sqrt(2.0) * 0.5; // 对角线长度的一半
                        
                        // 计算当前像素到扫光线的距离
                        // 扫光线从 -diagonalLength 扫到 +diagonalLength
                        fixed sweepDistance = (lightProgress * 2.0 - 1.0) * diagonalLength;
                        fixed pixelDistance = dot(centeredUV, lightDir) - sweepDistance;
                        
                        // 计算扫光带的厚度范围
                        fixed halfThick = _LightThick * 0.5;
                        
                        // 判断当前像素是否在扫光带内
                        if (abs(pixelDistance) < halfThick)
                        {
                            // 计算扫光强度 (中心最亮，边缘渐暗)
                            fixed normalizedDistance = abs(pixelDistance) / halfThick;
                            fixed lightStrength = 1.0 - normalizedDistance;
                            
                            // 使用smoothstep创建更平滑的过渡效果
                            lightStrength = smoothstep(0.0, 1.0, lightStrength);
                            
                            // 保存原始透明度
                            half originalAlpha = col.a;
                            
                            // 应用扫光效果 - 增加亮度，避免过度曝光
                            fixed3 lightEffect = col.rgb * (_LightIntensity * lightStrength);
                            col.rgb = saturate(col.rgb + lightEffect);
                            
                            // 恢复原始透明度
                            col.a = originalAlpha;
                        }
                    }
                #endif

                #ifdef SOFT_CLIPPED
                    float2 factor;
                    float2 condition = step(i.clipPos.xy, 0);
                    float4 clip_softness = _ClipSoftness * float4(condition, 1 - condition);
                    factor.xy = (1.0 - abs(i.clipPos.xy)) * (clip_softness.xw + clip_softness.zy);
                    col.a *= clamp(min(factor.x, factor.y), 0.0, 1.0);
                #endif

                #ifdef CLIPPED
                    float2 factor = abs(i.clipPos);
                    col.a *= step(max(factor.x, factor.y), 1);
                #endif

                #ifdef COLOR_FILTER
                    if (_ColorOption == 0)
                    {
                        fixed4 col2 = col;
                        col2.r = dot(col, _ColorMatrix[0]) + _ColorOffset.x;
                        col2.g = dot(col, _ColorMatrix[1]) + _ColorOffset.y;
                        col2.b = dot(col, _ColorMatrix[2]) + _ColorOffset.z;
                        col2.a = dot(col, _ColorMatrix[3]) + _ColorOffset.w;
                        col = col2;
                    }
                    else //premultiply alpha
                        col.rgb *= col.a;
                #endif

                #ifdef ALPHA_MASK
                    clip(col.a - 0.001);
                #endif

                return col;
            }
            ENDCG
        }
    }
}