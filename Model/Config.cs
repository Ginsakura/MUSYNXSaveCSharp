using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows.Input;
using Windows.Globalization;

namespace MUSYNCSaveDecode.Model;

public partial class Config
{
    /// <summary>
    /// 静态实例对象变量
    /// </summary>
    /// <remarks>全局唯一</remarks>
    private static Config? _instance;
    private static readonly object _Lock = new();
    private static readonly ViewModel vm = ViewModel.Instance;

    ///// <summary>
    ///// 日志等级枚举
    ///// </summary>
    //public enum LogLevel
    //{
    //    NOTSET = 0,
    //    DEBUG,
    //    INFO,
    //    WARNING,
    //    ERROR,
    //    CRITICAL,
    //}

    /// <summary>
    /// 日志等级映射
    /// </summary>
    private static readonly Dictionary<String, SourceLevels> LogLevelMap = new(){
        {"NOTSET", SourceLevels.Off},
        {"DEBUG", SourceLevels.Verbose},
        {"INFO", SourceLevels.Information},
        {"WARN", SourceLevels.Warning},
        {"WARNING", SourceLevels.Warning},
        {"ERROR", SourceLevels.Error},
        {"FATAL", SourceLevels.Critical},
        {"CRITICAL", SourceLevels.Critical},
    };

    private Config()
    {
        CompressLogFile();
        // Initialize default values
        Language = "zh-CN";
        LoggerFilterString = "DEBUG";
        LoggerFilter = SourceLevels.Information;
        Acc_Sync = false;
        CheckUpdate = true;
        DLLInjection = false;
        SystemDPI = 100;
        DonutChartinHitDelay = true;
        DonutChartinAllHitAnalyze = true;
        NarrowDelayInterval = true;
        ConsoleAlpha = 75;
        ConsoleFont = "霞鹜文楷等宽";
        ConsoleFontSize = 36;
        MainExecPath = "";
        ChangeConsoleStyle = true;
        DefaultKeys = false;
        DefaultDiffcute = 0;

        LoadConfig();
    }

    public static Config Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_Lock) { _instance ??= new Config(); }
            }
            return _instance;
        }
    }

    public String Language { get; set; }
    public String LoggerFilterString { get; set; }
    public SourceLevels LoggerFilter { get; set; }
    public Boolean Acc_Sync { get; set; }
    public Boolean CheckUpdate { get; set; }
    public Boolean DLLInjection { get; set; }
    public Int32 SystemDPI { get; set; }
    public Boolean DonutChartinHitDelay { get; set; }
    public Boolean DonutChartinAllHitAnalyze { get; set; }
    public Boolean NarrowDelayInterval { get; set; }
    public Int32 ConsoleAlpha { get; set; }
    public String ConsoleFont { get; set; }
    public Int32 ConsoleFontSize { get; set; }
    public String MainExecPath { get; set; }
    public Boolean ChangeConsoleStyle { get; set; }
    public Boolean DefaultKeys { get; set; }
    public Int32 DefaultDiffcute { get; set; }

    private void LoadConfig()
    {
        if (!File.Exists(StaticResource.ConfigFilePath))
        {
            //_logger.LogError($"File \"{StaticResource.ConfigFilePath}\" does not exist.");
            return;
        }

        try
        {
            using var fileStream = File.OpenRead(StaticResource.ConfigFilePath);
            var config = JsonSerializer.Deserialize<Dictionary<String, object>>(fileStream);

            if (config != null)
            {
                foreach (var (key, value) in config)
                {
                    switch (key)
                    {
                        case "Language": Language = value.ToString() ?? "zh-CN"; break;
                        case "LoggerFilterString": LoggerFilterString = value.ToString() ?? "INFO"; break;
                        case "Acc_Sync": Acc_Sync = Convert.ToBoolean(value); break;
                        case "CheckUpdate": CheckUpdate = Convert.ToBoolean(value); break;
                        case "DLLInjection": DLLInjection = Convert.ToBoolean(value); break;
                        case "SystemDPI": SystemDPI = Convert.ToInt32(value); break;
                        case "DonutChartinHitDelay": DonutChartinHitDelay = Convert.ToBoolean(value); break;
                        case "DonutChartinAllHitAnalyze": DonutChartinAllHitAnalyze = Convert.ToBoolean(value); break;
                        case "NarrowDelayInterval": NarrowDelayInterval = Convert.ToBoolean(value); break;
                        case "ConsoleAlpha": ConsoleAlpha = Convert.ToInt32(value); break;
                        case "ConsoleFont": ConsoleFont = value.ToString() ?? "霞鹜文楷等宽"; break;
                        case "ConsoleFontSize": ConsoleFontSize = Convert.ToInt32(value); break;
                        case "MainExecPath": MainExecPath = value.ToString() ?? ""; break;
                        case "ChangeConsoleStyle": ChangeConsoleStyle = Convert.ToBoolean(value); break;
                        case "DefaultKeys": DefaultKeys = Convert.ToBoolean(value); break;
                        case "DefaultDiffcute": DefaultDiffcute = Convert.ToInt32(value); break;
                    }
                }

                LoggerFilter = LogLevelMap[LoggerFilterString];
                //_logger.LogInformation($"Configuration loaded from \"{StaticResource.ConfigFilePath}\".");
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, $"Failed to load configuration from \"{StaticResource.ConfigFilePath}\".");
        }
    }

    public void SaveConfig()
    {
        var configData = new Dictionary<String, object>
        {
            { "LoggerFilter", LoggerFilterString },
            { "Acc_Sync", Acc_Sync },
            { "CheckUpdate", CheckUpdate },
            { "DLLInjection", DLLInjection },
            { "SystemDPI", SystemDPI },
            { "DonutChartinHitDelay", DonutChartinHitDelay },
            { "DonutChartinAllHitAnalyze", DonutChartinAllHitAnalyze },
            { "NarrowDelayInterval", NarrowDelayInterval },
            { "ConsoleAlpha", ConsoleAlpha },
            { "ConsoleFont", ConsoleFont },
            { "ConsoleFontSize", ConsoleFontSize },
            { "MainExecPath", MainExecPath },
            { "ChangeConsoleStyle", ChangeConsoleStyle },
            { "DefaultKeys", DefaultKeys },
            { "DefaultDiffcute", DefaultDiffcute }
        };

        String directory = Path.GetDirectoryName(StaticResource.ConfigFilePath) ?? Path.Combine(Directory.GetCurrentDirectory(), "musync_data");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            using var fileStream = File.OpenWrite(StaticResource.ConfigFilePath);
            JsonSerializer.Serialize(fileStream, configData, StaticResource.jsonSerializerOptions);
            //_logger.LogInformation($"Configuration saved to \"{StaticResource.ConfigFilePath}\".");
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, $"Failed to save configuration to \"{StaticResource.ConfigFilePath}\".");
        }
    }

    public static void CompressLogFile()
    {
        if (!Directory.Exists(StaticResource.LogCompressDir))
        {
            Directory.CreateDirectory(StaticResource.LogCompressDir);
        }

        Int32 nextIndex = Directory.GetFiles(StaticResource.LogCompressDir).Length;
        String compressLogName = $"log.{nextIndex}.gz";

        if (File.Exists(StaticResource.LogFilePath))
        {
            using FileStream fIn = File.OpenRead(StaticResource.LogFilePath);
            using GZipStream fOut = new(File.Create(Path.Combine(StaticResource.LogCompressDir, compressLogName)), CompressionMode.Compress);
            fIn.CopyTo(fOut);

            File.Delete(StaticResource.LogFilePath);
        }
    }
}