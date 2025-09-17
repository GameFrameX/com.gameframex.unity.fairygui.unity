using UnityEngine;
using FairyGUI;

namespace FairyGUI.Examples
{
    /// <summary>
    /// 扫光效果示例
    /// 展示如何在FairyGUI中使用扫光效果
    /// </summary>
    public class SweepLightExample : MonoBehaviour
    {
        /// <summary>
        /// UI包名称
        /// </summary>
        [Header("UI设置")]
        public string packageName = "SweepLightDemo";
        
        /// <summary>
        /// UI组件名称
        /// </summary>
        public string componentName = "Main";

        /// <summary>
        /// 主UI组件
        /// </summary>
        private GComponent _mainView;

        /// <summary>
        /// 测试图像组件
        /// </summary>
        private GImage _testImage1;
        private GImage _testImage2;
        private GImage _testImage3;

        /// <summary>
        /// 扫光图像组件
        /// </summary>
        private GSweepLightImage _sweepLightImage;

        /// <summary>
        /// 初始化
        /// </summary>
        void Start()
        {
            // 初始化FairyGUI
            InitializeFairyGUI();
            
            // 创建测试UI
            CreateTestUI();
            
            // 设置扫光效果
            SetupSweepLightEffects();
        }

        /// <summary>
        /// 初始化FairyGUI
        /// </summary>
        private void InitializeFairyGUI()
        {
            // 如果有UI包，加载UI包
            if (!string.IsNullOrEmpty(packageName))
            {
                UIPackage.AddPackage(packageName);
            }
        }

        /// <summary>
        /// 创建测试UI
        /// </summary>
        private void CreateTestUI()
        {
            // 创建主视图
            if (!string.IsNullOrEmpty(packageName) && !string.IsNullOrEmpty(componentName))
            {
                _mainView = UIPackage.CreateObject(packageName, componentName).asCom;
            }
            else
            {
                _mainView = new GComponent();
                _mainView.SetSize(Screen.width, Screen.height);
            }

            GRoot.inst.AddChild(_mainView);

            // 创建测试图像
            CreateTestImages();
            
            // 创建控制按钮
            CreateControlButtons();
        }

        /// <summary>
        /// 创建测试图像
        /// </summary>
        private void CreateTestImages()
        {
            // 创建普通图像用于对比
            _testImage1 = new GImage();
            _testImage1.SetSize(200, 100);
            _testImage1.SetXY(50, 100);
            _testImage1.color = Color.cyan;
            _mainView.AddChild(_testImage1);

            // 创建带扫光效果的图像
            _testImage2 = new GImage();
            _testImage2.SetSize(200, 100);
            _testImage2.SetXY(300, 100);
            _testImage2.color = Color.yellow;
            _mainView.AddChild(_testImage2);

            // 创建另一个扫光图像
            _testImage3 = new GImage();
            _testImage3.SetSize(200, 100);
            _testImage3.SetXY(550, 100);
            _testImage3.color = Color.magenta;
            _mainView.AddChild(_testImage3);

            // 创建专用扫光图像组件
            _sweepLightImage = new GSweepLightImage();
            _sweepLightImage.SetSize(200, 100);
            _sweepLightImage.SetXY(175, 250);
            _sweepLightImage.color = Color.green;
            _mainView.AddChild(_sweepLightImage);
        }

        /// <summary>
        /// 创建控制按钮
        /// </summary>
        private void CreateControlButtons()
        {
            // 添加默认扫光效果按钮
            var btnDefault = CreateButton("默认扫光", 50, 400, () => {
                _testImage2.AddDefaultSweepLight();
                Debug.Log("已添加默认扫光效果");
            });

            // 添加快速扫光效果按钮
            var btnFast = CreateButton("快速扫光", 200, 400, () => {
                _testImage2.AddFastSweepLight();
                Debug.Log("已添加快速扫光效果");
            });

            // 添加慢速扫光效果按钮
            var btnSlow = CreateButton("慢速扫光", 350, 400, () => {
                _testImage2.AddSlowSweepLight();
                Debug.Log("已添加慢速扫光效果");
            });

            // 自定义扫光效果按钮
            var btnCustom = CreateButton("自定义扫光", 500, 400, () => {
                _testImage3.AddSweepLight(
                    lightTime: 0.8f,
                    lightThick: 0.5f,
                    nextTime: 1.5f,
                    lightAngle: 30f,
                    lightIntensity: 2.5f
                );
                Debug.Log("已添加自定义扫光效果");
            });

            // 移除扫光效果按钮
            var btnRemove = CreateButton("移除扫光", 50, 450, () => {
                _testImage2.RemoveSweepLight();
                _testImage3.RemoveSweepLight();
                Debug.Log("已移除扫光效果");
            });

            // 启用/禁用扫光按钮
            var btnToggle = CreateButton("切换扫光", 200, 450, () => {
                if (_testImage2.HasSweepLight())
                {
                    _testImage2.DisableSweepLight();
                    Debug.Log("已禁用扫光效果");
                }
                else
                {
                    _testImage2.EnableSweepLight();
                    Debug.Log("已启用扫光效果");
                }
            });

            // 专用扫光组件测试按钮
            var btnSweepComponent = CreateButton("专用组件", 350, 450, () => {
                _sweepLightImage.StartSweepLight();
                _sweepLightImage.SetSweepLightParameters(0.5f, 0.4f, 1.8f, 60f, 2.0f);
                Debug.Log("已启动专用扫光组件");
            });

            // 停止专用扫光组件按钮
            var btnStopSweep = CreateButton("停止专用", 500, 450, () => {
                _sweepLightImage.StopSweepLight();
                Debug.Log("已停止专用扫光组件");
            });
        }

        /// <summary>
        /// 创建按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="onClick">点击事件</param>
        /// <returns>创建的按钮组件</returns>
        private GButton CreateButton(string text, float x, float y, System.Action onClick)
        {
            var button = new GButton();
            button.SetSize(120, 40);
            button.SetXY(x, y);
            button.title = text;
            button.onClick.Add(() => onClick?.Invoke());
            _mainView.AddChild(button);
            return button;
        }

        /// <summary>
        /// 设置扫光效果
        /// </summary>
        private void SetupSweepLightEffects()
        {
            // 为第二个图像添加默认扫光效果
            _testImage2.AddDefaultSweepLight();

            // 为专用扫光组件设置参数
            if (_sweepLightImage != null)
            {
                _sweepLightImage.lightTime = 0.8f;
                _sweepLightImage.lightThick = 0.35f;
                _sweepLightImage.nextTime = 2.5f;
                _sweepLightImage.lightAngle = 50f;
                _sweepLightImage.lightIntensity = 1.8f;
                _sweepLightImage.sweepLightEnabled = true;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            // 测试动态参数调整
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TestDynamicParameterAdjustment();
            }

            // 测试扫光状态检查
            if (Input.GetKeyDown(KeyCode.T))
            {
                TestSweepLightStatus();
            }
        }

        /// <summary>
        /// 测试动态参数调整
        /// </summary>
        private void TestDynamicParameterAdjustment()
        {
            if (_testImage2.HasSweepLight())
            {
                // 随机调整扫光参数
                _testImage2.UpdateSweepLightParameters(
                    lightTime: Random.Range(0.3f, 1.2f),
                    lightIntensity: Random.Range(1.0f, 3.0f),
                    lightAngle: Random.Range(15f, 75f)
                );
                Debug.Log("已随机调整扫光参数");
            }
        }

        /// <summary>
        /// 测试扫光状态检查
        /// </summary>
        private void TestSweepLightStatus()
        {
            Debug.Log($"图像2扫光状态: {_testImage2.HasSweepLight()}");
            Debug.Log($"图像3扫光状态: {_testImage3.HasSweepLight()}");
            
            if (_sweepLightImage != null)
            {
                Debug.Log($"专用扫光组件状态: {_sweepLightImage.sweepLightEnabled}");
                Debug.Log($"当前扫光参数: 时间={_sweepLightImage.lightTime}, 厚度={_sweepLightImage.lightThick}, 角度={_sweepLightImage.lightAngle}");
            }
        }

        /// <summary>
        /// 销毁时清理
        /// </summary>
        void OnDestroy()
        {
            // 清理材质缓存
            SweepLightManager.ClearMaterialCache();
            
            // 移除UI包
            if (!string.IsNullOrEmpty(packageName))
            {
                UIPackage.RemovePackage(packageName);
            }
        }
    }
}