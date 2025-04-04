using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace MUSYNCSaveDecode.Model;

public static class StaticResource
{
    private static readonly object _Lock = new();

    private static Int32 _LogCount = 0;
    public static Int32 LogCount
    {
        get { lock (_Lock) { return _LogCount; } }
    }

    public static void IncrementLogCount()
    {
        lock (_Lock) { _LogCount++; }
    }

    /// <summary>
    /// 游戏文件名
    /// </summary>
    public const String ProcessName = "MUSYNX";

    /// <summary>
    /// 默认存档路径提示
    /// </summary>
    public const String SavePath = "Input SaveFile or AnalyzeFile Path (savedata.sav)";

    /// <summary>
    /// 仓库地址提示
    /// </summary>
    public const String RepoTip = "点击打开GitHub仓库 点个Star吧，秋梨膏";

    /// <summary>
    /// 软件更新提示
    /// </summary>
    public const String UpdateTip = "检测到新版本，点击打开GitHub仓库";

    /// <summary>
    /// DLL文件路径
    /// </summary>
    public const String AssemblyPath = "MUSYNX_Data/Managed/Assembly-CSharp.dll";

    /// <summary>
    /// 语言代码到语言名称的映射
    /// </summary>
    public static readonly Dictionary<string, string> languageNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "zh-cn", "简体中文" },
            { "zh-tw", "繁體中文" },
            { "en-us", "English (US)" },
            { "fr-fr", "Français (France)" },
            { "de-de", "Deutsch (Deutschland)" },
            // 可以继续添加其他语言映射
        };

    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevel
    {
        Off,
        Verbose,
        Information,
        Warning,
        Error,
        Critical
    }

    /// <summary>
    /// Json序列化设置
    /// </summary>
    public static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };

    /// <summary>
    /// Data Dir
    /// </summary>
    public static readonly String DataDir = Path.Combine(Directory.GetCurrentDirectory(), "musync_data");

    /// <summary>
    /// Config File Path
    /// </summary>
    public static readonly String ConfigFilePath = Path.Combine(DataDir, "bootcfg.json");

    /// <summary>
    /// Log File Path
    /// </summary>
    public static readonly String LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

    /// <summary>
    /// Compress Log Files Dir
    /// </summary>
    public static readonly String LogCompressDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");

    public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Toolkit.StringToColor("#000000"));
    public static readonly SolidColorBrush PinkColor = new SolidColorBrush(Toolkit.StringToColor("#FF7F7F"));
    public static readonly SolidColorBrush GreenColor = new SolidColorBrush(Toolkit.StringToColor("#98E22B"));
    public static readonly SolidColorBrush RedColor = new SolidColorBrush(Toolkit.StringToColor("#FF0505"));
    public static readonly SolidColorBrush OrangeColor = new SolidColorBrush(Toolkit.StringToColor("#FFBF7F"));
}