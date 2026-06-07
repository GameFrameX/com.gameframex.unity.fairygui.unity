<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

基于 FairyGUI for Unity 的二次分发，主要服务于 [GameFrameX](https://github.com/GameFrameX/GameFrameX)。FairyGUI 是一个跨平台的 UI 编辑器和 UI 框架。

### 改动功能

1. 增加 `Packages` 的支持
2. 增加 `FairyGUICroppingHelper` 防裁剪脚本
3. 增加 `link.xml` 的裁剪过滤
4. 修复 `异步加载资源包` 没有回调的BUG
5. 增加 `微信小游戏` 和 `抖音小游戏` 的输入框拉不起键盘的适配
6. 增加 `微信小游戏的宏定义ENABLE_WECHAT_MINI_GAME` 不开启宏定义将不会生效键盘适配（注意：不要和抖音同时开启）
7. 增加 `抖音小游戏的宏定义ENABLE_DOUYIN_MINI_GAME` 不开启宏定义将不会生效键盘适配（注意：不要和微信同时开启）
8. 增加 `抖音小游戏` 和 `微信小游戏` 的快捷开启和关闭在编辑器的 `Tools` > `FairyGUI` > 对应的菜单快速设置
9. 增加 `像素化功能` 支持图像的像素化效果

### 像素化功能使用说明

#### 启用像素化功能

在 Unity 项目设置中添加宏定义：`ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED`

路径：`Edit > Project Settings > Player > Scripting Define Symbols`

#### 支持的着色器

- FairyGUI-Image.shader（图像）

#### 参数调整

修改着色器中的 `_PixelSize` 参数：

- 值越大：像素块越小（更精细）
- 值越小：像素块越大（更粗糙）

推荐值：
- 轻微像素化：64-128
- 中等像素化：32-64
- 明显像素化：16-32
- 强烈像素化：8-16
- 极端像素化：4-8

#### 注意事项

1. 修改宏定义后需要重新编译项目
2. 未定义宏时不会有性能开销
3. 文本像素化可能影响可读性，请谨慎使用

### 关于 FairyGUI

FairyGUI 是一个适用于许多游戏引擎的 UI 中间件。[下载编辑器](https://www.fairygui.com/product.html)

在运行效率方面，FairyGUI 对 DrawCall 优化使用了特有的 `FairyBatching` 技术，相比 NGUI、UGUI 的传统优化技术更加高效而且容易控制，特别是对动静耦合越来越复杂的 UI 设计更是应付自如。

在功能方面，FairyGUI 对传统 UI 制作痛点都有很好的内置支持，例如 `图文混排`（包括文字和动画混排），`表情输入`（直接支持键盘上的表情），`虚拟列表`、`循环列表`，`像素级点击检测`，`曲面 UI`，`手势`，`粒子和模型穿插UI`，`打字效果`等。

FairyGUI 还对所有输入方式进行了完整的封装，无论是鼠标、单点触摸、多点触摸还是 VR 手柄输入，开发者都可以使用相同的代码处理交互。

## 快速开始

### 安装

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.fairygui.unity": "5.3.3"
  }
}
```

## 文档与资源

- [FairyGUI 教程](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX 文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: 467608841 / 233840761

## 更新日志

查看 [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) 了解更新日志。


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
