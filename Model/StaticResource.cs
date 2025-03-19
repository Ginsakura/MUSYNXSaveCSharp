using System.IO;
using System.Text.Json;

namespace MUSYNCSaveDecode.Model;

internal class StaticResource
{
    /// <summary>
    /// 游戏文件名
    /// </summary>
    public const String ProcessName = "MUSYNX.exe";

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
    public static readonly String LogFilePath = Path.Combine(DataDir, "log.txt");

    /// <summary>
    /// Compress Log Files Dir
    /// </summary>
    public static readonly String LogCompressDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
}