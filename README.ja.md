<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## プロジェクト概要

[GameFrameX](https://github.com/GameFrameX/GameFrameX) 用に再パッケージされた FairyGUI for Unity。FairyGUI はクロスプラットフォームの UI エディタおよび UI フレームワークです。

### 公式版からの変更点

1. Unity Package Manager サポートの追加
2. アンチストリッピングスクリプト `FairyGUICroppingHelper` の追加
3. ストリップフィルタリング用 `link.xml` の追加
4. 非同期アセットバンドル読み込みのコールバックがないバグの修正
5. WeChat / Douyin ミニゲーム用インラインキーボードブリッジと `WECHAT_MINI_GAME` / `DOUYIN_MINI_GAME` マクロの削除（6.0.0 の破壊的変更：ミニゲーム SDK に一切依存しない純粋な UI パッケージへ回帰）；ミニゲームのキーボード適応は [operationkeyboard.fairygui](https://github.com/gameframex/com.gameframex.unity.operationkeyboard.fairygui) バインディングパッケージ経由に移行（WeChat / Alipay / Kuaishou / Douyin の 4 プラットフォーム、下記の移行ガイドを参照）
6. ピクセル化機能の追加

#### ミニゲームキーボードの移行（6.0.0 破壊的変更）

6.0.0 以降、`Fairygui.Runtime` はミニゲーム用キーボード適応を一切インラインしません（旧 `GTextInputWeChat` / `GTextInputDouYin`、`Wx` / `StarkWebGL` アセンブリ参照、`WECHAT_MINI_GAME` / `DOUYIN_MINI_GAME` マクロをすべて削除）。インラインブリッジに依存していた WeChat / Douyin プロジェクトは以下の手順で移行してください（動作は等価、中国語入力を含む）：

1. プロジェクトから `ENABLE_WECHAT_MINI_GAME` / `ENABLE_DOUYIN_MINI_GAME` のスクリプト定義シンボルを削除
2. バインディングパッケージ `com.gameframex.unity.operationkeyboard.fairygui` をインストール（versionDefines で本パッケージを検出し、発見次第バインド；フォーカスで Attach / ブラーで Detach、パネル変更は不要）
3. 対応するチャネルパッケージをインストール：`com.gameframex.unity.operationkeyboard.wechatminigame` / `.douyinminigame` / `.alipayminigame` / `.kuaishouminigame`（チャネル未登録の Editor / ネイティブプラットフォームではファサードが no-op、ネイティブキーボードの経路は完全に保持されます）

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

### インストール

以下のいずれかの方法を選択してください：

1. Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：
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

   `scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

2. `manifest.json` の `dependencies` に直接追加：
   ```json
   {
      "com.gameframex.unity.fairygui.unity": "https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git"
   }
   ```
3. Unity の **Package Manager** で **Git URL** を使用して追加：`https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git`
4. リポジトリを Unity プロジェクトの `Packages` ディレクトリにクローンしてください。自動的に読み込まれます。
## ドキュメントとリソース

- [FairyGUI チュートリアル](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: 467608841 / 233840761

## 変更履歴

変更履歴は [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases) をご覧ください。


## 依存関係

| パッケージ | 説明 |
|----------|------|
| (无) | - |

## ライセンス

詳しくは [LICENSE.md](LICENSE.md) をご参照ください。
