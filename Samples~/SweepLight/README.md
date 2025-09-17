# FairyGUI 扫光效果示例

本示例展示了如何在FairyGUI中使用扫光效果。

## 功能特性

- **多种使用方式**: 支持扩展方法、专用组件、管理器等多种使用方式
- **预设配置**: 提供默认、快速、慢速等预设配置
- **动态参数调整**: 支持运行时动态调整扫光参数
- **材质管理**: 自动管理扫光材质的创建和缓存

## 使用方法

### 1. 扩展方法方式（推荐）

```csharp
// 添加默认扫光效果
image.AddDefaultSweepLight();

// 添加快速扫光效果
image.AddFastSweepLight();

// 添加自定义扫光效果
image.AddSweepLight(
    lightTime: 0.8f,        // 扫光时间
    lightThick: 0.5f,       // 扫光厚度
    nextTime: 1.5f,         // 下次扫光间隔
    lightAngle: 30f,        // 扫光角度
    lightIntensity: 2.5f    // 扫光强度
);

// 移除扫光效果
image.RemoveSweepLight();

// 启用/禁用扫光效果
image.EnableSweepLight();
image.DisableSweepLight();

// 检查是否有扫光效果
bool hasSweepLight = image.HasSweepLight();

// 动态更新参数
image.UpdateSweepLightParameters(
    lightTime: 1.0f,
    lightIntensity: 2.0f
);
```

### 2. 专用组件方式

```csharp
// 创建扫光图像组件
GSweepLightImage sweepImage = new GSweepLightImage();

// 设置扫光参数
sweepImage.lightTime = 0.8f;
sweepImage.lightThick = 0.35f;
sweepImage.nextTime = 2.5f;
sweepImage.lightAngle = 50f;
sweepImage.lightIntensity = 1.8f;

// 启用/禁用扫光效果
sweepImage.sweepLightEnabled = true;

// 开始/停止扫光
sweepImage.StartSweepLight();
sweepImage.StopSweepLight();

// 批量设置参数
sweepImage.SetSweepLightParameters(0.5f, 0.4f, 1.8f, 60f, 2.0f);
```

### 3. 管理器方式

```csharp
// 创建扫光材质
var config = SweepLightManager.SweepLightConfig.Default();
Material sweepMaterial = SweepLightManager.CreateSweepLightMaterial(config);

// 为图像添加扫光效果
SweepLightManager.AddDefaultSweepLightToImage(image);

// 获取预设配置
var fastConfig = SweepLightManager.GetPresetConfig("fast");
var slowConfig = SweepLightManager.GetPresetConfig("slow");

// 清理材质缓存
SweepLightManager.ClearMaterialCache();
```

## 扫光参数说明

| 参数 | 类型 | 范围 | 说明 |
|------|------|------|------|
| lightTime | float | > 0 | 扫光从开始到结束的持续时间（秒） |
| lightThick | float | 0-1 | 扫光带的宽度 |
| nextTime | float | > 0 | 两次扫光之间的等待时间（秒） |
| lightAngle | float | 0-90 | 扫光的倾斜角度（度） |
| lightIntensity | float | 0-3 | 扫光的亮度强度倍数 |

## 预设配置

### 默认配置 (Default)
- 扫光时间: 0.6秒
- 扫光厚度: 0.3
- 间隔时间: 2.0秒
- 扫光角度: 45度
- 扫光强度: 1.4倍

### 快速配置 (Fast)
- 扫光时间: 0.3秒
- 扫光厚度: 0.2
- 间隔时间: 1.0秒
- 扫光角度: 30度
- 扫光强度: 2.0倍

### 慢速配置 (Slow)
- 扫光时间: 1.2秒
- 扫光厚度: 0.4
- 间隔时间: 3.0秒
- 扫光角度: 60度
- 扫光强度: 1.0倍

## 运行示例

1. 将 `SweepLightExample.cs` 脚本添加到场景中的GameObject上
2. 运行场景
3. 使用界面上的按钮测试不同的扫光效果
4. 按空格键随机调整扫光参数
5. 按T键查看扫光状态信息

## 注意事项

1. 确保项目中包含 `FairyGUI/ImageSweepLight` Shader
2. 扫光效果基于时间实现，需要在运行时才能看到效果
3. 材质会自动管理，但建议在不需要时及时清理缓存
4. 扫光效果与FairyGUI的其他功能（如裁剪、颜色滤镜等）兼容

## 性能建议

1. 使用材质缓存避免重复创建相同配置的材质
2. 不需要扫光效果时及时禁用或移除
3. 避免频繁动态调整参数
4. 合理设置扫光间隔时间，避免过于频繁的扫光