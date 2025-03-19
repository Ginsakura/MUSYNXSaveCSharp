# 同步音律喵赛克 Steam端 存档解析工具
MUSYNX Steam Client Savefile Decode & Analyze Tool

Python版本: [MUSYNCSave](https://github.com/Ginsakura/MUSYNCSave)

[down_svg]: https://img.shields.io/github/downloads/ginsakura/MUSYNCSaveCSharp/total?label=All%20Downloads
[all_release]: https://github.com/Ginsakura/MUSYNCSaveCSharp/releases
[commit_svg]: https://img.shields.io/github/commit-activity/t/ginsakura/MUSYNCSaveCSharp?label=All%20Commits
[commit]: https://github.com/Ginsakura/MUSYNCSaveCSharp/commits
[license_svg]: https://img.shields.io/github/license/ginsakura/MUSYNCSaveCSharp?label=License
[![All releases][down_svg]][all_release]
[![All commit activity (branch)][commit_svg]][commit]
[![License][license_svg]](./LICENSE)

[latest_prerelease_svg]: https://img.shields.io/github/v/release/ginsakura/MUSYNCSaveCSharp?display_name=release&label=Latest%20PreRelease&include_prereleases
[all_tags]: https://github.com/Ginsakura/MUSYNCSaveCSharp/tags
[![Latest tag][latest_prerelease_svg]][all_tags]

[latest_release_svg]: https://img.shields.io/github/v/release/ginsakura/MUSYNCSaveCSharp?display_name=release&label=Latest%20Release
[release]: https://github.com/Ginsakura/MUSYNCSaveCSharp/releases/latest
[![latest release][latest_release_svg]][release]

## How to use

1. [English (English, `en-us`) v1.2.6 rc2](How_to_use.en.md)
2. [简体中文 (Simplified Chinese, `zh-Hans`) v1.2.6 rc2](How_to_use.zh.md)

## 界面展示

<details>
<summary>界面展示</summary>

![主页面](./ReadmeResources/main.png "主页面")
![score-diff页面](./ReadmeResources/score-diff.png "score-diff页面")
![HitDelay页面](./ReadmeResources/HitDelay.png "HitDelay页面")
![HitAnalyze-Pie&Bar页面](./ReadmeResources/HitAnalyze-Pie&Bar.png "HitAnalyze-Pie&Bar页面")
![HitAnalyze-Line页面](./ReadmeResources/HitAnalyze-Line.png "HitAnalyze-Line页面")
![AllHitAnalyze-Pie页面](./ReadmeResources/AllHitAnalyze.png "AllHitAnalyze页面")
![AvgAcc-SYNC.Rate回归分析页面](./ReadmeResources/AvgAcc-SYNC.Rate.png "AvgAcc-SYNC.Rate回归分析")

</details>

## 计划
### 未来的计划
- [ ] 提供全球排行榜显示功能 (需要调用SteamAPI)
- [ ] 将提供一个文档来演示使用方法 (在写了,咕咕咕)
- [ ] 使用文件夹内指定文件名的方式自定义美化UI
- [ ] 主程序目录通过exe文件进行判断
### 已完成的计划

## 功能控制模块
<details>
<summary>控制参数详解</summary>
<style>
  table { margin: 0 auto; }
</style>

于`./musync_data/ExtraFunction.cfg`文件中启用/禁用对应功能

|            配置项           |     默认值     | 值类型  |                                配置说明                                |
|-----------------------------|----------------|---------|------------------------------------------------------------------------|
| `Version`                   | `自动获取`     | string  |  |
| `LoggerFilter`              | 'INFO'         | string  |  |
| `Acc_Sync`                  | false          | boolean | 是否启用Acc-Synx图表                                                    |
| `CheckUpdate`               | false          | boolean | 是否启用自动检查更新                                                    |
| `DLLInjection`              | false          | boolean | 是否启用DLL注入以开启`高级功能`                                         |
| `SystemDPI`                 | `自动获取`     | string  | 读取系统DPI, 提供DPI窗体修正(未实现)                                    |
| `DonutChartinHitDelay`      | false          | boolean | 是否在单次游玩统计中显示击打延迟环形图                                  |
| `DonutChartinAllHitAnalyze` | false          | boolean | 是否在全局统计中显示击打延迟环形图                                      |
| `NarrowDelayInterval`       | true           | boolean | 是否在单次游玩统计中使用更狭窄的击打区间来计算平均偏移值(Delay)<br>[true=±45ms,false=±90ms]|
| `ConsoleAlpha`              | 75             | int     | 游戏控制台窗口的不透明度<br>(取值范围[0,100],100为完全不透明,不建议取值在30以下)|
| `ConsoleFont`               | '霞鹜文楷等宽' | string  | 游戏控制台窗口的字体                                                    |
| `ConsoleFontSize`           | 36             | int     | 游戏控制台窗口的字号                                                    |
| `MainExecPath`              | `自动获取`     | string  | 游戏主程序所在的路径                                                    |
| `ChangeConsoleStyle`        | false          | boolean | 是否启用自定义喵赛克游戏本体控制台窗口样式                              |
| `FramelessWindow`           | false          | boolean | 是否启用无边框窗口样式(未完成)                                          |
| `TransparentColor`          | '#FFFFFF'      | string<br>boolean | (仅限新样式)设置透明色<br>设置为false即为关闭透明色           |
| `DefaultKeys`               | false          | boolean | `高级功能`中默认`键数`参数 (true为4K, false为6K)                        |
| `DefaultDiffcute`           | 0              | int     | `高级功能`中默认`难度`参数 (0为Easy, 1为Hard, 2为Inferno)               |

</details>

### 高级功能简介
下列组件对游戏客户端有修改,请谨慎使用

*** $\color{Red}{ 注意备份 注意备份 注意备份 }$ ***

$\color{Red}{请自行决定是否使用,使用过程中出现任何意外}$
$\color{Red}{后果自负,开发者概不负责}$

HitDelay模块用法:启用DLL注入后,在本次游戏进行首次谱面游玩时会打开一个cmd窗口 $\color{Red}{请勿关闭该窗口}$

- `HitDelayFix.dll` 被精心修改过的客户端文件,原始文件为`./MUSYNX_Data/Managed/Assembly-CSharp.dll`.
- `HitDelayLine.py` 用于读取控制台中的击打信息生成可视化数据表,标题栏提供三个统计信息.
    - `AvgDelay` 平均击打延迟,即所有击打的平均值,能够一定程度上提示游戏延迟应该调整的数值(可能有较大偏差,仅供参考).
    比如游戏内判定补偿是+010ms,AvgDelay数值为-5ms,那么就应将游戏内判定补偿减少5ms,但是具体需要调整多少请多次测试.
    - `AllKeys` 谱面中存在note的数目.
    - `AvgAcc` 平均击打偏差,即所有击打的绝对值的平均值,该值总为正数.
    该值反应了您当前谱面本次游玩击打Key的时机的精准度,该值与您本次游玩的结算成绩有一定的关联:

    该值越小,就说明您击打的越精准,(在该值小于45ms时，您的)分值就会越高.

## 更新日志
<!--
### Version 
#### PreRelease
1. 更新
    1. xxx
2. 修复
    1. xxx
3. 优化
    1. xxx
-->
### Version 1.0.0
#### PreRelease
1. 更新
    1. xxx
2. 修复
    1. xxx
3. 优化
    1. xxx

<!--## 旧版本更新日志 (1.0.0 - 1.2.8rc5)
<details>
  <summary>旧版本更新日志</summary>
</details>-->

## 声明：妾身 ***不会*** 对存档文件进行 ***写*** 操作