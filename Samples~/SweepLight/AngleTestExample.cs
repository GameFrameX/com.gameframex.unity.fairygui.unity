using UnityEngine;
using FairyGUI;

namespace FairyGUI.Examples
{
    /// <summary>
    /// 扫光角度测试示例
    /// 测试0-360度角度范围的扫光效果
    /// </summary>
    public class AngleTestExample : MonoBehaviour
    {
        /// <summary>
        /// 测试图像组件数组
        /// </summary>
        private GImage[] _testImages;

        /// <summary>
        /// 主UI组件
        /// </summary>
        private GComponent _mainView;

        /// <summary>
        /// 当前测试角度
        /// </summary>
        private float _currentAngle = 0f;

        /// <summary>
        /// 角度增量
        /// </summary>
        private float _angleStep = 45f;

        void Start()
        {
            // 初始化FairyGUI
            GRoot.inst.SetContentScaleFactor(1920, 1080, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);

            // 创建主视图
            _mainView = new GComponent();
            _mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
            GRoot.inst.AddChild(_mainView);

            // 创建测试图像
            CreateTestImages();

            // 创建控制按钮
            CreateControlButtons();

            // 开始角度测试
            StartAngleTest();
        }

        /// <summary>
        /// 创建测试图像
        /// </summary>
        private void CreateTestImages()
        {
            _testImages = new GImage[8];
            
            // 创建8个图像，排列成圆形
            for (int i = 0; i < 8; i++)
            {
                _testImages[i] = new GImage();
                _testImages[i].SetSize(150, 80);
                
                // 计算圆形排列的位置
                float angle = i * 45f * Mathf.Deg2Rad;
                float radius = 300f;
                float centerX = GRoot.inst.width * 0.5f;
                float centerY = GRoot.inst.height * 0.5f;
                
                float x = centerX + Mathf.Cos(angle) * radius - 75f;
                float y = centerY + Mathf.Sin(angle) * radius - 40f;
                
                _testImages[i].SetXY(x, y);
                
                // 设置不同的颜色
                Color[] colors = {
                    Color.red, Color.green, Color.blue, Color.yellow,
                    Color.cyan, Color.magenta, new Color(1f, 0.5f, 0f), new Color(0.5f, 0f, 1f)
                };
                _testImages[i].color = colors[i];
                
                _mainView.AddChild(_testImages[i]);
                
                // 添加扫光效果，每个图像使用不同的角度
                float sweepAngle = i * 45f;
                _testImages[i].AddSweepLight(
                    lightTime: 1.0f,
                    lightThick: 0.4f,
                    nextTime: 2.0f,
                    lightAngle: sweepAngle,
                    lightIntensity: 2.0f
                );
                
                Debug.Log($"图像 {i + 1}: 扫光角度 = {sweepAngle}度");
            }
        }

        /// <summary>
        /// 创建控制按钮
        /// </summary>
        private void CreateControlButtons()
        {
            // 角度递增按钮
            var btnIncreaseAngle = CreateButton("角度+45°", 50, 50, () => {
                IncreaseAngle();
            });

            // 角度递减按钮
            var btnDecreaseAngle = CreateButton("角度-45°", 200, 50, () => {
                DecreaseAngle();
            });

            // 随机角度按钮
            var btnRandomAngle = CreateButton("随机角度", 350, 50, () => {
                SetRandomAngles();
            });

            // 重置角度按钮
            var btnResetAngle = CreateButton("重置角度", 500, 50, () => {
                ResetAngles();
            });

            // 显示当前角度信息
            var angleInfo = new GTextField();
            angleInfo.SetSize(300, 30);
            angleInfo.SetXY(650, 55);
            angleInfo.text = "当前基础角度: 0°";
            angleInfo.color = Color.white;
            _mainView.AddChild(angleInfo);

            // 定时更新角度信息
            InvokeRepeating(nameof(UpdateAngleInfo), 0f, 0.1f);
        }

        /// <summary>
        /// 创建按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="onClick">点击事件</param>
        /// <returns>创建的按钮</returns>
        private GButton CreateButton(string text, float x, float y, System.Action onClick)
        {
            var btn = new GButton();
            btn.SetSize(120, 40);
            btn.SetXY(x, y);
            btn.title = text;
            btn.onClick.Add(() => onClick?.Invoke());
            _mainView.AddChild(btn);
            return btn;
        }

        /// <summary>
        /// 开始角度测试
        /// </summary>
        private void StartAngleTest()
        {
            Debug.Log("扫光角度测试开始！");
            Debug.Log("8个图像分别使用0°, 45°, 90°, 135°, 180°, 225°, 270°, 315°角度");
            Debug.Log("按空格键可以动态调整所有角度");
        }

        /// <summary>
        /// 增加角度
        /// </summary>
        private void IncreaseAngle()
        {
            _currentAngle += _angleStep;
            if (_currentAngle >= 360f) _currentAngle = 0f;
            
            UpdateAllAngles();
            Debug.Log($"所有角度增加 {_angleStep}°，当前基础角度: {_currentAngle}°");
        }

        /// <summary>
        /// 减少角度
        /// </summary>
        private void DecreaseAngle()
        {
            _currentAngle -= _angleStep;
            if (_currentAngle < 0f) _currentAngle = 315f;
            
            UpdateAllAngles();
            Debug.Log($"所有角度减少 {_angleStep}°，当前基础角度: {_currentAngle}°");
        }

        /// <summary>
        /// 设置随机角度
        /// </summary>
        private void SetRandomAngles()
        {
            for (int i = 0; i < _testImages.Length; i++)
            {
                float randomAngle = Random.Range(0f, 360f);
                _testImages[i].UpdateSweepLightParameters(
                    lightAngle: randomAngle
                );
                Debug.Log($"图像 {i + 1}: 随机角度 = {randomAngle:F1}°");
            }
        }

        /// <summary>
        /// 重置角度
        /// </summary>
        private void ResetAngles()
        {
            _currentAngle = 0f;
            for (int i = 0; i < _testImages.Length; i++)
            {
                float angle = i * 45f;
                _testImages[i].UpdateSweepLightParameters(
                    lightAngle: angle
                );
            }
            Debug.Log("所有角度已重置为初始值");
        }

        /// <summary>
        /// 更新所有角度
        /// </summary>
        private void UpdateAllAngles()
        {
            for (int i = 0; i < _testImages.Length; i++)
            {
                float newAngle = (_currentAngle + i * 45f) % 360f;
                _testImages[i].UpdateSweepLightParameters(
                    lightAngle: newAngle
                );
            }
        }

        /// <summary>
        /// 更新角度信息显示
        /// </summary>
        private void UpdateAngleInfo()
        {
            var angleInfo = _mainView.GetChildAt(_mainView.numChildren - 1) as GTextField;
            if (angleInfo != null)
            {
                angleInfo.text = $"当前基础角度: {_currentAngle}°";
            }
        }

        void Update()
        {
            // 按空格键动态调整角度
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncreaseAngle();
            }

            // 按R键随机角度
            if (Input.GetKeyDown(KeyCode.R))
            {
                SetRandomAngles();
            }

            // 按T键重置角度
            if (Input.GetKeyDown(KeyCode.T))
            {
                ResetAngles();
            }
        }

        void OnDestroy()
        {
            // 清理资源
            if (_mainView != null)
            {
                _mainView.Dispose();
            }
        }
    }
}