using UnityEngine;
using System.Collections.Generic;

namespace FairyGUI
{
    /// <summary>
    /// 扫光效果管理器
    /// 提供扫光材质的统一管理和便捷的API接口
    /// </summary>
    public static class SweepLightManager
    {
        /// <summary>
        /// 扫光材质缓存
        /// </summary>
        private static Dictionary<string, Material> _materialCache = new Dictionary<string, Material>();

        /// <summary>
        /// 扫光Shader引用
        /// </summary>
        private static Shader _sweepLightShader;

        /// <summary>
        /// 默认扫光参数配置
        /// </summary>
        public struct SweepLightConfig
        {
            /// <summary>
            /// 扫光时间（秒）
            /// </summary>
            /// <value>扫光从开始到结束的持续时间</value>
            public float lightTime;

            /// <summary>
            /// 扫光厚度
            /// </summary>
            /// <value>扫光带的宽度，范围通常为0-1</value>
            public float lightThick;

            /// <summary>
            /// 下次扫光间隔时间（秒）
            /// </summary>
            /// <value>两次扫光之间的等待时间</value>
            public float nextTime;

            /// <summary>
            /// 扫光角度
            /// </summary>
            /// <value>扫光的倾斜角度，范围0-360度</value>
            public float lightAngle;

            /// <summary>
            /// 扫光强度
            /// </summary>
            /// <value>扫光的亮度强度，范围0-3</value>
            public float lightIntensity;

            /// <summary>
            /// 创建默认配置
            /// </summary>
            /// <returns>默认的扫光参数配置</returns>
            public static SweepLightConfig Default()
            {
                return new SweepLightConfig
                {
                    lightTime = 0.6f,
                    lightThick = 0.3f,
                    nextTime = 2.0f,
                    lightAngle = 45.0f,
                    lightIntensity = 1.4f
                };
            }

            /// <summary>
            /// 创建快速扫光配置
            /// </summary>
            /// <returns>快速扫光参数配置</returns>
            public static SweepLightConfig Fast()
            {
                return new SweepLightConfig
                {
                    lightTime = 0.3f,
                    lightThick = 0.2f,
                    nextTime = 1.0f,
                    lightAngle = 30.0f,
                    lightIntensity = 2.0f
                };
            }

            /// <summary>
            /// 创建慢速扫光配置
            /// </summary>
            /// <returns>慢速扫光参数配置</returns>
            public static SweepLightConfig Slow()
            {
                return new SweepLightConfig
                {
                    lightTime = 1.2f,
                    lightThick = 0.4f,
                    nextTime = 3.0f,
                    lightAngle = 60.0f,
                    lightIntensity = 1.0f
                };
            }
        }

        /// <summary>
        /// 扫光Shader属性
        /// </summary>
        /// <value>获取扫光Shader，如果未加载则自动加载</value>
        public static Shader SweepLightShader
        {
            get
            {
                if (_sweepLightShader == null)
                {
                    _sweepLightShader = Shader.Find("FairyGUI/ImageSweepLight");
                    if (_sweepLightShader == null)
                    {
                        Debug.LogError("SweepLightManager: 未找到 FairyGUI/ImageSweepLight Shader");
                    }
                }
                return _sweepLightShader;
            }
        }

        /// <summary>
        /// 创建扫光材质
        /// </summary>
        /// <param name="config">扫光参数配置</param>
        /// <returns>配置好的扫光材质</returns>
        public static Material CreateSweepLightMaterial(SweepLightConfig config)
        {
            if (SweepLightShader == null)
                return null;

            Material material = new Material(SweepLightShader);
            ApplyConfigToMaterial(material, config);
            material.EnableKeyword("SWEEP_LIGHT");
            
            return material;
        }

        /// <summary>
        /// 创建默认扫光材质
        /// </summary>
        /// <returns>使用默认配置的扫光材质</returns>
        public static Material CreateDefaultSweepLightMaterial()
        {
            return CreateSweepLightMaterial(SweepLightConfig.Default());
        }

        /// <summary>
        /// 获取或创建缓存的扫光材质
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="config">扫光参数配置</param>
        /// <returns>缓存的扫光材质</returns>
        public static Material GetOrCreateCachedMaterial(string configName, SweepLightConfig config)
        {
            if (_materialCache.TryGetValue(configName, out Material cachedMaterial))
            {
                if (cachedMaterial != null)
                    return cachedMaterial;
                else
                    _materialCache.Remove(configName);
            }

            Material newMaterial = CreateSweepLightMaterial(config);
            if (newMaterial != null)
            {
                _materialCache[configName] = newMaterial;
            }
            
            return newMaterial;
        }

        /// <summary>
        /// 应用配置到材质
        /// </summary>
        /// <param name="material">目标材质</param>
        /// <param name="config">扫光参数配置</param>
        public static void ApplyConfigToMaterial(Material material, SweepLightConfig config)
        {
            if (material == null) return;

            material.SetFloat("_LightTime", config.lightTime);
            material.SetFloat("_LightThick", config.lightThick);
            material.SetFloat("_NextTime", config.nextTime);
            material.SetFloat("_LightAngle", config.lightAngle);
            material.SetFloat("_LightIntensity", config.lightIntensity);
        }

        /// <summary>
        /// 为GImage添加扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <param name="config">扫光参数配置</param>
        public static void AddSweepLightToImage(GImage image, SweepLightConfig config)
        {
            if (image == null) return;

            Material sweepMaterial = CreateSweepLightMaterial(config);
            if (sweepMaterial != null)
            {
                image.material = sweepMaterial;
            }
        }

        /// <summary>
        /// 为GImage添加默认扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        public static void AddDefaultSweepLightToImage(GImage image)
        {
            AddSweepLightToImage(image, SweepLightConfig.Default());
        }

        /// <summary>
        /// 移除GImage的扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        public static void RemoveSweepLightFromImage(GImage image)
        {
            if (image == null) return;

            // 恢复到默认材质或null
            image.material = null;
        }

        /// <summary>
        /// 启用材质的扫光效果
        /// </summary>
        /// <param name="material">目标材质</param>
        public static void EnableSweepLight(Material material)
        {
            if (material != null)
            {
                material.EnableKeyword("SWEEP_LIGHT");
            }
        }

        /// <summary>
        /// 禁用材质的扫光效果
        /// </summary>
        /// <param name="material">目标材质</param>
        public static void DisableSweepLight(Material material)
        {
            if (material != null)
            {
                material.DisableKeyword("SWEEP_LIGHT");
            }
        }

        /// <summary>
        /// 清理材质缓存
        /// </summary>
        public static void ClearMaterialCache()
        {
            foreach (var kvp in _materialCache)
            {
                if (kvp.Value != null)
                {
                    if (Application.isPlaying)
                        Object.Destroy(kvp.Value);
                    else
                        Object.DestroyImmediate(kvp.Value);
                }
            }
            _materialCache.Clear();
        }

        /// <summary>
        /// 获取预设配置
        /// </summary>
        /// <param name="presetName">预设名称（"default", "fast", "slow"）</param>
        /// <returns>对应的扫光配置</returns>
        public static SweepLightConfig GetPresetConfig(string presetName)
        {
            switch (presetName.ToLower())
            {
                case "fast":
                    return SweepLightConfig.Fast();
                case "slow":
                    return SweepLightConfig.Slow();
                case "default":
                default:
                    return SweepLightConfig.Default();
            }
        }
    }
}