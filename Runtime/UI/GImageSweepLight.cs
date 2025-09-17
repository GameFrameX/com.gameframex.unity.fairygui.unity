using UnityEngine;
using FairyGUI.Utils;

namespace FairyGUI
{
    /// <summary>
    /// 带扫光效果的图像组件
    /// 继承自GImage，添加扫光效果的参数控制功能
    /// </summary>
    public class GImageSweepLight : GImage
    {
        /// <summary>
        /// 扫光材质
        /// </summary>
        private Material _sweepLightMaterial;

        /// <summary>
        /// 扫光时间（秒）
        /// </summary>
        /// <value>扫光从开始到结束的持续时间</value>
        public float lightTime
        {
            get { return GetMaterialFloat("_LightTime", 0.6f); }
            set { SetMaterialFloat("_LightTime", value); }
        }

        /// <summary>
        /// 扫光厚度
        /// </summary>
        /// <value>扫光带的宽度，范围通常为0-1</value>
        public float lightThick
        {
            get { return GetMaterialFloat("_LightThick", 0.3f); }
            set { SetMaterialFloat("_LightThick", value); }
        }

        /// <summary>
        /// 下次扫光间隔时间（秒）
        /// </summary>
        /// <value>两次扫光之间的等待时间</value>
        public float nextTime
        {
            get { return GetMaterialFloat("_NextTime", 2.0f); }
            set { SetMaterialFloat("_NextTime", value); }
        }

        /// <summary>
        /// 扫光角度
        /// </summary>
        /// <value>扫光的倾斜角度，范围0-360度</value>
        public float lightAngle
        {
            get { return GetMaterialFloat("_LightAngle", 45.0f); }
            set { SetMaterialFloat("_LightAngle", Mathf.Clamp(value, 0f, 360f)); }
        }

        /// <summary>
        /// 扫光强度
        /// </summary>
        /// <value>扫光的亮度强度，范围0-3</value>
        public float lightIntensity
        {
            get { return GetMaterialFloat("_LightIntensity", 1.4f); }
            set { SetMaterialFloat("_LightIntensity", Mathf.Clamp(value, 0f, 3f)); }
        }

        /// <summary>
        /// 是否启用扫光效果
        /// </summary>
        /// <value>true表示启用扫光效果，false表示禁用</value>
        public bool sweepLightEnabled
        {
            get { return _sweepLightMaterial != null && _sweepLightMaterial.IsKeywordEnabled("SWEEP_LIGHT"); }
            set
            {
                EnsureSweepLightMaterial();
                if (value)
                {
                    _sweepLightMaterial.EnableKeyword("SWEEP_LIGHT");
                }
                else
                {
                    _sweepLightMaterial.DisableKeyword("SWEEP_LIGHT");
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GImageSweepLight()
        {
        }

        /// <summary>
        /// 创建显示对象
        /// </summary>
        protected override void CreateDisplayObject()
        {
            base.CreateDisplayObject();
            InitializeSweepLightMaterial();
        }

        /// <summary>
        /// 初始化扫光材质
        /// </summary>
        private void InitializeSweepLightMaterial()
        {
            // 加载扫光Shader
            Shader sweepLightShader = Shader.Find("FairyGUI/ImageSweepLight");
            if (sweepLightShader != null)
            {
                _sweepLightMaterial = new Material(sweepLightShader);
                _content.material = _sweepLightMaterial;

                // 设置默认参数
                SetDefaultSweepLightParameters();
            }
            else
            {
                Debug.LogWarning("GSweepLightImage: 未找到 FairyGUI/ImageSweepLight Shader");
            }
        }

        /// <summary>
        /// 设置默认扫光参数
        /// </summary>
        private void SetDefaultSweepLightParameters()
        {
            if (_sweepLightMaterial == null)
            {
                return;
            }

            _sweepLightMaterial.SetFloat("_LightTime", 0.6f);
            _sweepLightMaterial.SetFloat("_LightThick", 0.3f);
            _sweepLightMaterial.SetFloat("_NextTime", 2.0f);
            _sweepLightMaterial.SetFloat("_LightAngle", 45.0f);
            _sweepLightMaterial.SetFloat("_LightIntensity", 1.4f);

            // 默认启用扫光效果
            _sweepLightMaterial.EnableKeyword("SWEEP_LIGHT");
        }

        /// <summary>
        /// 确保扫光材质已创建
        /// </summary>
        private void EnsureSweepLightMaterial()
        {
            if (_sweepLightMaterial == null)
            {
                InitializeSweepLightMaterial();
            }
        }

        /// <summary>
        /// 获取材质的浮点参数值
        /// </summary>
        /// <param name="propertyName">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        private float GetMaterialFloat(string propertyName, float defaultValue)
        {
            if (_sweepLightMaterial != null && _sweepLightMaterial.HasProperty(propertyName))
            {
                return _sweepLightMaterial.GetFloat(propertyName);
            }

            return defaultValue;
        }

        /// <summary>
        /// 设置材质的浮点参数值
        /// </summary>
        /// <param name="propertyName">参数名称</param>
        /// <param name="value">参数值</param>
        private void SetMaterialFloat(string propertyName, float value)
        {
            EnsureSweepLightMaterial();
            if (_sweepLightMaterial != null && _sweepLightMaterial.HasProperty(propertyName))
            {
                _sweepLightMaterial.SetFloat(propertyName, value);
            }
        }

        /// <summary>
        /// 开始扫光效果
        /// 重置时间参数以立即开始新的扫光循环
        /// </summary>
        public void StartSweepLight()
        {
            sweepLightEnabled = true;
        }

        /// <summary>
        /// 停止扫光效果
        /// </summary>
        public void StopSweepLight()
        {
            sweepLightEnabled = false;
        }

        /// <summary>
        /// 设置扫光参数
        /// </summary>
        /// <param name="lightTime">扫光时间</param>
        /// <param name="lightThick">扫光厚度</param>
        /// <param name="nextTime">下次扫光间隔</param>
        /// <param name="lightAngle">扫光角度</param>
        /// <param name="lightIntensity">扫光强度</param>
        public void SetSweepLightParameters(float lightTime, float lightThick, float nextTime, float lightAngle, float lightIntensity)
        {
            this.lightTime = lightTime;
            this.lightThick = lightThick;
            this.nextTime = nextTime;
            this.lightAngle = lightAngle;
            this.lightIntensity = lightIntensity;
        }

        /// <summary>
        /// 销毁时清理资源
        /// </summary>
        public override void Dispose()
        {
            if (_sweepLightMaterial != null)
            {
                if (Application.isPlaying)
                {
                    Object.Destroy(_sweepLightMaterial);
                }
                else
                {
                    Object.DestroyImmediate(_sweepLightMaterial);
                }

                _sweepLightMaterial = null;
            }

            base.Dispose();
        }
    }
}