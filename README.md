<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Project Overview

FairyGUI for Unity, repackaged for [GameFrameX](https://github.com/GameFrameX/GameFrameX). FairyGUI is a cross-platform UI editor and UI framework.

### Modifications from Official Version

1. Added Unity Package Manager support
2. Added `FairyGUICroppingHelper` anti-stripping script
3. Added `link.xml` for strip filtering
4. Fixed bug where async asset bundle loading had no callback
5. Added keyboard input adaptation for WeChat Mini Game and Douyin Mini Game
6. Added `ENABLE_WECHAT_MINI_GAME` macro for WeChat Mini Game keyboard adaptation (do not enable simultaneously with Douyin)
7. Added `ENABLE_DOUYIN_MINI_GAME` macro for Douyin Mini Game keyboard adaptation (do not enable simultaneously with WeChat)
8. Added quick toggle in Unity Editor: `Tools` > `FairyGUI` > corresponding menu
9. Added pixelation feature for image pixelation effects

### Pixelation Feature

#### Enable Pixelation

Add the macro `ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED` in Unity Project Settings:

`Edit > Project Settings > Player > Scripting Define Symbols`

#### Supported Shaders

- FairyGUI-Image.shader

#### Parameter Adjustment

Adjust the `_PixelSize` parameter in the shader:

- Larger value: smaller pixel blocks (finer)
- Smaller value: larger pixel blocks (coarser)

Recommended values:
- Slight pixelation: 64-128
- Medium pixelation: 32-64
- Noticeable pixelation: 16-32
- Strong pixelation: 8-16
- Extreme pixelation: 4-8

#### Notes

1. Recompile the project after modifying macro definitions
2. No performance overhead when the macro is not defined
3. Text pixelation may affect readability - use with caution

### About FairyGUI

FairyGUI is a UI middleware for many game engines. [Download Editor](https://www.fairygui.com/product.html)

In terms of rendering efficiency, FairyGUI uses a unique `FairyBatching` technology for DrawCall optimization, which is more efficient and easier to control than traditional optimization techniques used by NGUI and UGUI.

FairyGUI has built-in support for common UI pain points such as `mixed text and graphics` (including text with animation), `emoji input`, `virtual lists`, `loop lists`, `pixel-level click detection`, `curved UI`, `gestures`, `particles and models interleaved with UI`, `typewriter effects`, and more.

FairyGUI also encapsulates all input methods - mouse, single touch, multi-touch, and VR controller - so developers can handle interactions with the same code.

## Quick Start

### Installation

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

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

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.fairygui.unity": "5.3.3"
  }
}
```


## Documentation & Resources

- [FairyGUI Tutorial](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: 467608841 / 233840761

## Changelog

See [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) for changelog.

## License

This project is licensed under the [MIT License](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/blob/main/LICENSE).
