using System.IO;
using System.Text.Json;

namespace MUSYNCSaveDecode.Model;

internal class StaticObject
{
    /// <summary>
    /// 游戏文件名
    /// </summary>
    public const string ProcessName = "MUSYNC.exe";

    /// <summary>
    /// Json序列化设置
    /// </summary>
    public static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };

    /// <summary>
    /// Data Dir
    /// </summary>
    public static readonly string DataDir = Path.Combine(Directory.GetCurrentDirectory(), "musync_data");

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