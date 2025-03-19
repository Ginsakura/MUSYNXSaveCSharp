using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows.Input;

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
        Version = "";
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
        FramelessWindow = false;
        TransparentColor = "#FFFFFF";
        Default4Keys = false;
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

    public String Version { get; set; }
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
    public Boolean FramelessWindow { get; set; }
    public String TransparentColor { get; set; }
    public Boolean Default4Keys { get; set; }
    public Int32 DefaultDiffcute { get; set; }

    private void LoadConfig()
    {
        if (!File.Exists(StaticObject.ConfigFilePath))
        {
            //_logger.LogError($"File \"{StaticObject.ConfigFilePath}\" does not exist.");
            return;
        }

        try
        {
            using var fileStream = File.OpenRead(StaticObject.ConfigFilePath);
            var config = JsonSerializer.Deserialize<Dictionary<String, object>>(fileStream);

            if (config != null)
            {
                foreach (var (key, value) in config)
                {
                    switch (key)
                    {
                        case "Version": Version = value.ToString() ?? "0.0.0.0"; break;
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
                        case "FramelessWindow": FramelessWindow = Convert.ToBoolean(value); break;
                        case "TransparentColor": TransparentColor = value.ToString() ?? "#FFFFFF"; break;
                        case "Default4Keys": Default4Keys = Convert.ToBoolean(value); break;
                        case "DefaultDiffcute": DefaultDiffcute = Convert.ToInt32(value); break;
                    }
                }

                LoggerFilter = LogLevelMap[LoggerFilterString];
                //_logger.LogInformation($"Configuration loaded from \"{StaticObject.ConfigFilePath}\".");
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, $"Failed to load configuration from \"{StaticObject.ConfigFilePath}\".");
        }
    }

    public void SaveConfig()
    {
        var configData = new Dictionary<String, object>
        {
            { "Version", Version },
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
            { "FramelessWindow", FramelessWindow },
            { "TransparentColor", TransparentColor },
            { "Default4Keys", Default4Keys },
            { "DefaultDiffcute", DefaultDiffcute }
        };

        String directory = Path.GetDirectoryName(StaticObject.ConfigFilePath) ?? Path.Combine(Directory.GetCurrentDirectory(), "musync_data");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            using var fileStream = File.OpenWrite(StaticObject.ConfigFilePath);
            JsonSerializer.Serialize(fileStream, configData, StaticObject.jsonSerializerOptions);
            //_logger.LogInformation($"Configuration saved to \"{StaticObject.ConfigFilePath}\".");
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, $"Failed to save configuration to \"{StaticObject.ConfigFilePath}\".");
        }
    }

    public static void CompressLogFile()
    {
        if (!Directory.Exists(StaticObject.LogCompressDir))
        {
            Directory.CreateDirectory(StaticObject.LogCompressDir);
        }

        Int32 nextIndex = Directory.GetFiles(StaticObject.LogCompressDir).Length;
        String compressLogName = $"log.{nextIndex}.gz";

        if (File.Exists(StaticObject.LogFilePath))
        {
            using FileStream fIn = File.OpenRead(StaticObject.LogFilePath);
            using GZipStream fOut = new(File.Create(Path.Combine(StaticObject.LogCompressDir, compressLogName)), CompressionMode.Compress);
            fIn.CopyTo(fOut);

            File.Delete(StaticObject.LogFilePath);
        }
    }
}