using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// GImage扩展方法
    /// 为GImage组件提供便捷的扫光效果操作方法
    /// </summary>
    public static class GImageExtensions
    {
        /// <summary>
        /// 为图像添加扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <param name="lightTime">扫光时间（秒）</param>
        /// <param name="lightThick">扫光厚度</param>
        /// <param name="nextTime">下次扫光间隔时间（秒）</param>
        /// <param name="lightAngle">扫光角度（0-90度）</param>
        /// <param name="lightIntensity">扫光强度（0-3）</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage AddSweepLight(this GImage image, float lightTime = 0.6f, float lightThick = 0.3f, float nextTime = 2.0f, float lightAngle = 45.0f, float lightIntensity = 1.4f)
        {
            if (image == null)
            {
                return image;
            }

            var config = new SweepLightManager.SweepLightConfig
            {
                lightTime = lightTime,
                lightThick = lightThick,
                nextTime = nextTime,
                lightAngle = lightAngle,
                lightIntensity = lightIntensity
            };

            SweepLightManager.AddSweepLightToImage(image, config);
            return image;
        }

        /// <summary>
        /// 为图像添加默认扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage AddDefaultSweepLight(this GImage image)
        {
            if (image == null)
            {
                return image;
            }

            SweepLightManager.AddDefaultSweepLightToImage(image);
            return image;
        }

        /// <summary>
        /// 为图像添加快速扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage AddFastSweepLight(this GImage image)
        {
            if (image == null)
            {
                return image;
            }

            SweepLightManager.AddSweepLightToImage(image, SweepLightManager.SweepLightConfig.Fast());
            return image;
        }

        /// <summary>
        /// 为图像添加慢速扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage AddSlowSweepLight(this GImage image)
        {
            if (image == null)
            {
                return image;
            }

            SweepLightManager.AddSweepLightToImage(image, SweepLightManager.SweepLightConfig.Slow());
            return image;
        }

        /// <summary>
        /// 为图像添加预设扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <param name="presetName">预设名称（"default", "fast", "slow"）</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage AddSweepLightPreset(this GImage image, string presetName)
        {
            if (image == null)
            {
                return image;
            }

            var config = SweepLightManager.GetPresetConfig(presetName);
            SweepLightManager.AddSweepLightToImage(image, config);
            return image;
        }

        /// <summary>
        /// 移除图像的扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage RemoveSweepLight(this GImage image)
        {
            if (image == null)
            {
                return image;
            }

            SweepLightManager.RemoveSweepLightFromImage(image);
            return image;
        }

        /// <summary>
        /// 启用图像的扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage EnableSweepLight(this GImage image)
        {
            if (image == null || image.material == null)
            {
                return image;
            }

            SweepLightManager.EnableSweepLight(image.material);
            return image;
        }

        /// <summary>
        /// 禁用图像的扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage DisableSweepLight(this GImage image)
        {
            if (image == null || image.material == null)
            {
                return image;
            }

            SweepLightManager.DisableSweepLight(image.material);
            return image;
        }

        /// <summary>
        /// 检查图像是否有扫光效果
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>true表示有扫光效果，false表示没有</returns>
        public static bool HasSweepLight(this GImage image)
        {
            if (image == null || image.material == null)
            {
                return false;
            }

            return image.material.shader != null && image.material.shader.name == "FairyGUI/ImageSweepLight" && image.material.IsKeywordEnabled("SWEEP_LIGHT");
        }

        /// <summary>
        /// 更新图像的扫光参数
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <param name="lightTime">扫光时间（秒）</param>
        /// <param name="lightThick">扫光厚度</param>
        /// <param name="nextTime">下次扫光间隔时间（秒）</param>
        /// <param name="lightAngle">扫光角度（0-90度）</param>
        /// <param name="lightIntensity">扫光强度（0-3）</param>
        /// <returns>返回图像组件本身，支持链式调用</returns>
        public static GImage UpdateSweepLightParameters(this GImage image, float? lightTime = null, float? lightThick = null, float? nextTime = null, float? lightAngle = null, float? lightIntensity = null)
        {
            if (image == null || image.material == null)
            {
                return image;
            }

            var material = image.material;

            if (lightTime.HasValue && material.HasProperty("_LightTime"))
            {
                material.SetFloat("_LightTime", lightTime.Value);
            }

            if (lightThick.HasValue && material.HasProperty("_LightThick"))
            {
                material.SetFloat("_LightThick", lightThick.Value);
            }

            if (nextTime.HasValue && material.HasProperty("_NextTime"))
            {
                material.SetFloat("_NextTime", nextTime.Value);
            }

            if (lightAngle.HasValue && material.HasProperty("_LightAngle"))
            {
                material.SetFloat("_LightAngle", Mathf.Clamp(lightAngle.Value, 0f, 90f));
            }

            if (lightIntensity.HasValue && material.HasProperty("_LightIntensity"))
            {
                material.SetFloat("_LightIntensity", Mathf.Clamp(lightIntensity.Value, 0f, 3f));
            }

            return image;
        }

        /// <summary>
        /// 获取图像的扫光参数配置
        /// </summary>
        /// <param name="image">目标图像组件</param>
        /// <returns>当前的扫光参数配置，如果没有扫光效果则返回默认配置</returns>
        public static SweepLightManager.SweepLightConfig GetSweepLightConfig(this GImage image)
        {
            if (image == null || image.material == null)
            {
                return SweepLightManager.SweepLightConfig.Default();
            }

            var material = image.material;

            return new SweepLightManager.SweepLightConfig
            {
                lightTime = material.HasProperty("_LightTime") ? material.GetFloat("_LightTime") : 0.6f,
                lightThick = material.HasProperty("_LightThick") ? material.GetFloat("_LightThick") : 0.3f,
                nextTime = material.HasProperty("_NextTime") ? material.GetFloat("_NextTime") : 2.0f,
                lightAngle = material.HasProperty("_LightAngle") ? material.GetFloat("_LightAngle") : 45.0f,
                lightIntensity = material.HasProperty("_LightIntensity") ? material.GetFloat("_LightIntensity") : 1.4f
            };
        }
    }
}