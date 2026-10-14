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
5. Removed the inline WeChat / Douyin mini-game keyboard bridges and the `WECHAT_MINI_GAME` / `DOUYIN_MINI_GAME` macros (6.0.0 breaking change: back to a pure UI package with zero mini-game SDK dependencies); mini-game keyboard adaptation now goes through the [operationkeyboard.fairygui](https://github.com/gameframex/com.gameframex.unity.operationkeyboard.fairygui) binding package (all four platforms: WeChat / Alipay / Kuaishou / Douyin, see migration notes below)
6. Added pixelation feature for image pixelation effects

#### Mini-game Keyboard Migration (6.0.0 Breaking Change)

As of 6.0.0 `Fairygui.Runtime` no longer inlines any mini-game keyboard adaptation (the former `GTextInputWeChat` / `GTextInputDouYin` classes, the `Wx` / `StarkWebGL` assembly references and the `WECHAT_MINI_GAME` / `DOUYIN_MINI_GAME` macros are all removed). Projects relying on the inline bridges migrate as follows (behavior-equivalent, Chinese input included):

1. Remove the `ENABLE_WECHAT_MINI_GAME` / `ENABLE_DOUYIN_MINI_GAME` scripting define symbols from your project
2. Install the binding package `com.gameframex.unity.operationkeyboard.fairygui` (probes this package via versionDefines and binds on discovery; focus attaches / blur detaches with zero panel changes)
3. Install the matching channel package: `com.gameframex.unity.operationkeyboard.wechatminigame` / `.douyinminigame` / `.alipayminigame` / `.kuaishouminigame` (with no channel registered the facade is a no-op on Editor / native platforms and the native keyboard path stays intact)

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

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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
     ],
     "dependencies": {
       "com.gameframex.unity.fairygui.unity": "6.0.0"
     }
   }
   ```

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.fairygui.unity": "https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.
## Documentation & Resources

- [FairyGUI Tutorial](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: 467608841 / 233840761

## Changelog

See [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) for changelog.


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |

## License

See [LICENSE.md](LICENSE.md) for license information.
