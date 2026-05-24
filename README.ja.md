<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160"/>

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/gameframex/com.gameframex.unity.fairygui.unity)](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/blob/main/LICENSE)
[![Version](https://img.shields.io/github/v/release/gameframex/com.gameframex.unity.fairygui.unity)](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases)
[![Documentation](https://img.shields.io/badge/Documentation-ドキュメント-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · [QQグループ](https://qm.qq.com/q/5kbDVBdUeS) · **言語**

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

---

## プロジェクト概要

[GameFrameX](https://github.com/GameFrameX/GameFrameX) 用に再パッケージされた FairyGUI for Unity。FairyGUI はクロスプラットフォームの UI エディタおよび UI フレームワークです。

### 公式版からの変更点

1. Unity Package Manager サポートの追加
2. アンチストリッピングスクリプト `FairyGUICroppingHelper` の追加
3. ストリップフィルタリング用 `link.xml` の追加
4. 非同期アセットバンドル読み込みのコールバックがないバグの修正
5. WeChat ミニゲームと Douyin ミニゲームのキーボード入力適応の追加
6. WeChat ミニゲーム用マクロ `ENABLE_WECHAT_MINI_GAME` の追加（Douyin と同時に有効にしないでください）
7. Douyin ミニゲーム用マクロ `ENABLE_DOUYIN_MINI_GAME` の追加（WeChat と同時に有効にしないでください）
8. Unity エディタの `Tools` > `FairyGUI` メニューから素早い切り替えを追加
9. ピクセル化機能の追加

### ピクセル化機能

#### ピクセル化の有効化

Unity プロジェクト設定にマクロ `ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED` を追加：

`Edit > Project Settings > Player > Scripting Define Symbols`

#### サポートされるシェーダー

- FairyGUI-Image.shader

#### パラメータ調整

シェーダーの `_PixelSize` パラメータを調整：

- 値が大きい：ピクセルブロックが小さい（より精細）
- 値が小さい：ピクセルブロックが大きい（より粗い）

推奨値：
- 軽度のピクセル化：64-128
- 中程度のピクセル化：32-64
- 顕著なピクセル化：16-32
- 強いピクセル化：8-16
- 極端なピクセル化：4-8

#### 注意事項

1. マクロ定義を変更した後、プロジェクトを再コンパイルする必要があります
2. マクロが定義されていない場合、パフォーマンスオーバーヘッドはありません
3. テキストのピクセル化は可読性に影響する可能性があるため、注意して使用してください

### FairyGUI について

FairyGUI は多くのゲームエンジン向けの UI ミドルウェアです。[エディタをダウンロード](https://www.fairygui.com/product.html)

レンダリング効率の面では、FairyGUI は DrawCall 最適化に独自の `FairyBatching` 技術を使用しており、NGUI や UGUI の従来の最適化技術よりも効率的で制御が容易です。

機能面では、`テキストとグラフィックの混在`、`絵文字入力`、`仮想リスト`、`ループリスト`、`ピクセルレベルのクリック検出`、`曲面 UI`、`ジェスチャー`、`パーティクルとモデルの UI インターリーブ`、`タイプライター効果`など、一般的な UI の課題を組み込みでサポートしています。

また、マウス、シングルタッチ、マルチタッチ、VR コントローラーなど、すべての入力方法をカプセル化しているため、開発者は同じコードでインタラクションを処理できます。

## クイックスタート

### インストール方法（いずれかを選択）

1. `manifest.json` の `dependencies` に以下を追加：
   ```json
   {
      "com.gameframex.unity.fairygui.unity": "https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git"
   }
   ```
2. Unity の `Packages Manager` で `Git URL` を使用して追加：`https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git`
3. リポジトリを直接ダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置すると、自動的に読み込まれます。

## ドキュメントとリソース

- [FairyGUI チュートリアル](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- [QQグループ](https://qm.qq.com/q/5kbDVBdUeS)

## 変更履歴

変更履歴は [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) をご覧ください。

## ライセンス

このプロジェクトは [MIT ライセンス](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/blob/main/LICENSE) の下で公開されています。
