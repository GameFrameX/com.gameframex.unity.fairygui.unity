该库主要服务于 `https://github.com/AlianBlank/GameFrameX` 作为子库使用。

# 使用方式(三种方式)

1. 直接在 `manifest.json` 文件中添加以下内容
   ```json
      {"com.alianblank.gameframex.unity.fairygui.unity": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.fairygui.unity.git"}
    ```
2. 在Unity 的`Packages Manager` 中使用`Git URL` 的方式添加库,地址为：https://github.com/AlianBlank/com.alianblank.gameframex.unity.fairygui.unity.git

3. 直接下载仓库放置到Unity 项目的`Packages` 目录下。会自动加载识别

# 改动功能

1. 增加 `Packages` 的支持
2. 增加 `FairyGUICroppingHelper` 防裁剪脚本
3. 增加 `link.xml` 的裁剪过滤
4. 修复 `异步加载资源包` 没有回调的BUG
5. 增加 `微信小游戏` 和 `抖音小游戏` 的输入框拉不起键盘的适配

FairyGUI for Unity
====

FairyGUI是一个适用于许多游戏引擎的UI中间件。<br>
[下载编辑器](https://www.fairygui.com/product.html)

在运行效率方面，FairyGUI对DrawCall优化使用了特有的`FairyBatching`技术，相比NGUI、UGUI的传统优化技术更加高效而且容易控制，特别是对动静耦合越来越复杂的UI设计更是应付自如。<br>

在功能方面，FairyGUI对传统UI制作痛点都有很好的内置支持，例如`图文混排`（包括文字和动画混排），`表情输入`（直接支持键盘上的表情），`虚拟列表`、`循环列表`，`像素级点击检测`，`曲面 UI`, `手势`，`粒子和模型穿插UI`，`打字效果`等。<br>

FairyGUI还对所有输入方式进行了完整的封装，无论是鼠标、单点触摸、多点触摸还是VR手柄输入，开发者都可以使用相同的代码处理交互。<br>

![](images/2015-11-10_000547.png)

![](images/2015-11-10_001320.png)

![](images/2015-11-10_001445.png)

![](images/2015-11-10_001516.png)

![](images/2016-06-15_010207.png)


学习
====

[教程](https://www.fairygui.com/docs/guide/index.html)

License
====
MIT