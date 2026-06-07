<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X FairyGUI for Unity

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.fairygui.unity)](https://github.com/GameFrameX/com.gameframex.unity.fairygui.unity/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 개요

[GameFrameX](https://github.com/GameFrameX/GameFrameX)를 위해 재패키징된 FairyGUI for Unity. FairyGUI는 크로스 플랫폼 UI 에디터 및 UI 프레임워크입니다.

### 공식 버전에서의 변경 사항

1. Unity Package Manager 지원 추가
2. 안티 스트리핑 스크립트 `FairyGUICroppingHelper` 추가
3. 스트립 필터링을 위한 `link.xml` 추가
4. 비동기 에셋 번들 로딩 콜백 누락 버그 수정
5. WeChat 미니 게임 및 Douyin 미니 게임 키보드 입력 적응 추가
6. WeChat 미니 게임용 매크로 `ENABLE_WECHAT_MINI_GAME` 추가 (Douyin과 동시에 활성화하지 마세요)
7. Douyin 미니 게임용 매크로 `ENABLE_DOUYIN_MINI_GAME` 추가 (WeChat과 동시에 활성화하지 마세요)
8. Unity 에디터 `Tools` > `FairyGUI` 메뉴에서 빠른 토글 추가
9. 픽셀화 기능 추가

### 픽셀화 기능

#### 픽셀화 활성화

Unity 프로젝트 설정에 매크로 `ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED` 추가:

`Edit > Project Settings > Player > Scripting Define Symbols`

#### 지원되는 셰이더

- FairyGUI-Image.shader

#### 매개변수 조정

셰이더의 `_PixelSize` 매개변수 조정:

- 값이 클수록: 픽셀 블록이 작음 (더 세밀함)
- 값이 작을수록: 픽셀 블록이 큼 (더 거침)

권장 값:
- 가벼운 픽셀화: 64-128
- 중간 픽셀화: 32-64
- 눈에 띄는 픽셀화: 16-32
- 강한 픽셀화: 8-16
- 극단적 픽셀화: 4-8

#### 참고 사항

1. 매크로 정의를 수정한 후 프로젝트를 재컴파일해야 합니다
2. 매크로가 정의되지 않은 경우 성능 오버헤드가 없습니다
3. 텍스트 픽셀화는 가독성에 영향을 줄 수 있으므로 주의해서 사용하세요

### FairyGUI 소개

FairyGUI는 많은 게임 엔진을 위한 UI 미들웨어입니다. [에디터 다운로드](https://www.fairygui.com/product.html)

렌더링 효율성 측면에서 FairyGUI는 DrawCall 최적화에 고유한 `FairyBatching` 기술을 사용하여 NGUI, UGUI의 기존 최적화 기술보다 더 효율적이고 제어하기 쉽습니다.

기능 측면에서는 `텍스트와 그래픽 혼합`, `이모지 입력`, `가상 리스트`, `순환 리스트`, `픽셀 수준 클릭 감지`, `곡면 UI`, `제스처`, `파티클과 모델의 UI 교차`, `타이핑 효과` 등 일반적인 UI 제작의 어려움을 기본적으로 지원합니다.

또한 마우스, 단일 터치, 멀티 터치, VR 컨트롤러 등 모든 입력 방식을 캡슐화하여 개발자가 동일한 코드로 상호작용을 처리할 수 있습니다.

## 빠른 시작

### 설치

Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:

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

`scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.fairygui.unity": "5.3.3"
  }
}
```


## 문서 및 자료

- [FairyGUI 튜토리얼](https://www.fairygui.com/docs/guide/index.html)
- [GameFrameX 문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: 467608841 / 233840761

## 변경 로그

변경 로그는 [Releases](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/releases)에서 확인하세요.

## 라이선스

이 프로젝트는 [MIT 라이선스](https://github.com/gameframex/com.gameframex.unity.fairygui.unity/blob/main/LICENSE)에 따라 배포됩니다.
