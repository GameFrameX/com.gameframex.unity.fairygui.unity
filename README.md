该库主要服务于 `https://github.com/GameFrameX/GameFrameX` 作为子库使用。

# 使用方式(三种方式)

1. 直接在 `manifest.json` 文件中添加以下内容
   ```json
      {"com.gameframex.unity.fairygui.unity": "https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git"}
    ```
2. 在Unity 的`Packages Manager` 中使用`Git URL` 的方式添加库,地址为：https://github.com/gameframex/com.gameframex.unity.fairygui.unity.git

3. 直接下载仓库放置到Unity 项目的`Packages` 目录下。会自动加载识别

# 改动功能

1. 增加 `Packages` 的支持
2. 增加 `FairyGUICroppingHelper` 防裁剪脚本
3. 增加 `link.xml` 的裁剪过滤
4. 修复 `异步加载资源包` 没有回调的BUG
5. 增加 `微信小游戏` 和 `抖音小游戏` 的输入框拉不起键盘的适配
6. 增加 `微信小游戏的宏定义ENABLE_WECHAT_MINI_GAME` 不开启宏定义将不会生效键盘适配(注意。不要和抖音同时开启。)
7. 增加 `抖音小游戏的宏定义ENABLE_DOUYIN_MINI_GAME` 不开启宏定义将不会生效键盘适配(注意。不要和微信同时开启。)
8. 增加 `抖音小游戏` 和 `微信小游戏` 的快捷开启和关闭在编辑器的`Tools`> `FairyGUI`> 对应的菜单快速设置
9. 增加 `像素化功能` 支持图像的像素化效果

## 像素化功能使用说明

### 启用像素化功能

在 Unity 项目设置中添加宏定义：`ENABLE_GAMEFRAMEX_FAIRYGUI_PIXELATED`

路径：`Edit > Project Settings > Player > Scripting Define Symbols`

### 支持的着色器

- FairyGUI-Image.shader（图像）

### 参数调整

修改着色器中的 `_PixelSize` 参数：

- 值越大：像素块越小（更精细）
- 值越小：像素块越大（更粗糙）

推荐值：
- 轻微像素化：64-128
- 中等像素化：32-64
- 明显像素化：16-32
- 强烈像素化：8-16
- 极端像素化：4-8

### 注意事项

1. 修改宏定义后需要重新编译项目
2. 未定义宏时不会有性能开销
3. 文本像素化可能影响可读性，请谨慎使用

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