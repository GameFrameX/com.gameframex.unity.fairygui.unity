<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 項目簡介

基於 FairyGUI for Unity 的二次分發，主要服務於 [GameFrameX](https://github.com/GameFrameX/GameFrameX)。FairyGUI 是一個跨平台的 UI 編輯器和 UI 框架。

### 改動功能

1. 新增 `Packages` 的支援
2. 新增 `FairyGUICroppingHelper` 防裁剪腳本
3. 新增 `link.xml` 的裁剪過濾
4. 修復 `非同步載入資源包` 沒有回呼的 BUG
5. 新增 `微信小遊戲` 和 `抖音小遊戲` 的輸入框拉不起鍵盤的適配
6. 新增 `微信小遊戲的巨集定義ENABLE_WECHAT_MINI_GAME` 不開啟巨集定義將不會生效鍵盤適配（注意：不要和抖音同時開啟）
7. 新增 `抖音小遊戲的巨集定義ENABLE_DOUYIN_MINI_GAME` 不開啟巨集定義將不會生效鍵盤適配（注意：不要和微信同時開啟）
8. 新增 `抖音小遊戲` 和 `微信小遊戲` 的快捷開啟和關閉在編輯器的 `Tools` > `FairyGUI` > 對應的選單快速設定
9. 新增 `像素化功能` 支援影像的像素化效果

### 像素化功能使用說明

#### 啟用像素化功能

在 Unity 專案設定中新增巨集定義：`ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED`

路徑：`Edit > Project Settings > Player > Scripting Define Symbols`

#### 支援的著色器

- FairyGUI-Image.shader（影像）

#### 參數調整

修改著色器中的 `_PixelSize` 參數：

- 值越大：像素塊越小（更精細）
- 值越小：像素塊越大（更粗糙）

推薦值：
- 輕微像素化：64-128
- 中等像素化：32-64
- 明顯像素化：16-32
- 強烈像素化：8-16
- 極端像素化：4-8

#### 注意事項

1. 修改巨集定義後需要重新編譯專案
2. 未定義巨集時不會有效能開銷
3. 文字像素化可能影響可讀性，請謹慎使用

### 關於 FairyGUI

FairyGUI 是一個適用於許多遊戲引擎的 UI 中介軟體。[下載編輯器](https://www.fairygui.com/product.html)

在執行效率方面，FairyGUI 對 DrawCall 最佳化使用了特有的 `FairyBatching` 技術，相比 NGUI、UGUI 的傳統最佳化技術更加高效而且容易控制，特別是對動靜耦合越來越複雜的 UI 設計更是應付自如。

在功能方面，FairyGUI 對傳統 UI 製作痛點都有很好的內建支援，例如 `圖文混排`（包括文字和動畫混排），`表情輸入`（直接支援鍵盤上的表情），`虛擬列表`、`循環列表`，`像素級點擊檢測`，`曲面 UI`，`手勢`，`粒子和模型穿插UI`，`打字效果`等。

FairyGUI 還對所有輸入方式進行了完整的封裝，無論是滑鼠、單點觸控、多點觸控還是 VR 手柄輸入，開發者都可以使用相同的程式碼處理互動。

## 快速開始

### 安裝

編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

`scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.fairygui.unity": "5.3.3"
  }
}
```

## 文檔與資源

- [FairyGUI 教程](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX 文檔](https://gameframex.doc.alianblank.com)

## 社區與支援

- QQ群: 467608841 / 233840761

## 更新日誌

查看 [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) 了解更新日誌。


## 依賴

| 套件 | 說明 |
|------|------|
| (无) | - |

## 開源協議

詳見 [LICENSE.md](LICENSE.md) 檔案。
