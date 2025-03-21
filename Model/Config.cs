using System.IO;
using System.Text.Json;

namespace MUSYNCSaveDecode.Model;

public class Config
{
    public String Language { get; set; } = "zh-cn";
    public String LoggerFilterString { get; set; } = "INFO";
    public StaticResource.LogLevel LoggerFilter { get; set; } = StaticResource.LogLevel.Information;
    public Boolean Acc_Sync { get; set; } = false;
    public Boolean CheckUpdate { get; set; } = true;
    public Boolean DLLInjection { get; set; } = false;
    public Int32 SystemDPI { get; set; } = 100;
    public Boolean DonutChartinHitDelay { get; set; } = true;
    public Boolean DonutChartinAllHitAnalyze { get; set; } = true;
    public Boolean NarrowDelayInterval { get; set; } = true;
    public Int32 ConsoleAlpha { get; set; } = 75;
    public String ConsoleFont { get; set; } = "霞鹜文楷等宽";
    public Int32 ConsoleFontSize { get; set; } = 36;
    public String MainExecPath { get; set; } = "";
    public Boolean ChangeConsoleStyle { get; set; } = true;
    public Boolean DefaultKeys { get; set; } = false;
    public Int32 DefaultDiffcute { get; set; } = 0;

    private static Config? _instance;
    private static readonly object _lock = new();
    //private static readonly ViewModel vm = ViewModel.Instance;
    private static readonly Logger _logger = Logger.Instance;

    /// <summary>
    /// 日志等级映射
    /// </summary>
    private static readonly Dictionary<String, StaticResource.LogLevel> LogLevelMap = new(){
        {"NOTSET", StaticResource.LogLevel.Off},
        {"DEBUG", StaticResource.LogLevel.Verbose},
        {"INFO", StaticResource.LogLevel.Information},
        {"WARN", StaticResource.LogLevel.Warning},
        {"WARNING", StaticResource.LogLevel.Warning},
        {"ERROR", StaticResource.LogLevel.Error},
        {"FATAL", StaticResource.LogLevel.Critical},
        {"CRITICAL", StaticResource.LogLevel.Critical},
    };

    private Config()
    {
        LoadConfig();
    }

    public static Config Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) { _instance ??= new Config(); }
            }
            return _instance;
        }
    }

    public void LoadConfig()
    {
        Config? loadedConfig = null;
        if (File.Exists(StaticResource.ConfigFilePath))
        {
            try
            {
                using var fileStream = File.OpenRead(StaticResource.ConfigFilePath);
                loadedConfig = JsonSerializer.Deserialize<Config>(fileStream);
            }
            catch (Exception ex)
            {
                // 处理反序列化错误，例如记录日志
                _logger.Error($"配置文件加载失败: {ex.Message}", "LoadConfig");
            }
        }

        if (loadedConfig != null)
        {
            // 将加载的配置应用到当前实例
            Language = loadedConfig.Language;
            LoggerFilterString = loadedConfig.LoggerFilterString;
            LoggerFilter = LogLevelMap[loadedConfig.LoggerFilterString];
            Acc_Sync = loadedConfig.Acc_Sync;
            CheckUpdate = loadedConfig.CheckUpdate;
            DLLInjection = loadedConfig.DLLInjection;
            SystemDPI = loadedConfig.SystemDPI;
            DonutChartinHitDelay = loadedConfig.DonutChartinHitDelay;
            DonutChartinAllHitAnalyze = loadedConfig.DonutChartinAllHitAnalyze;
            NarrowDelayInterval = loadedConfig.NarrowDelayInterval;
            ConsoleAlpha = loadedConfig.ConsoleAlpha;
            ConsoleFont = loadedConfig.ConsoleFont;
            ConsoleFontSize = loadedConfig.ConsoleFontSize;
            MainExecPath = loadedConfig.MainExecPath;
            ChangeConsoleStyle = loadedConfig.ChangeConsoleStyle;
            DefaultKeys = loadedConfig.DefaultKeys;
            DefaultDiffcute = loadedConfig.DefaultDiffcute;
        }
    }

    public void SaveConfig()
    {
        // 创建一个选项对象，用于格式化Json
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };// 格式化输出

        // 使用反射排除 LoggerFilter 属性
        var properties = typeof(Config).GetProperties()
            .Where(p => p.Name != nameof(LoggerFilter))
            .ToDictionary(p => p.Name, p => p.GetValue(this));

        // 序列化配置对象（排除 LoggerFilter）
        string jsonString = JsonSerializer.Serialize(properties, options);

        try
        {
            File.WriteAllText(StaticResource.ConfigFilePath, jsonString);
        }
        catch (Exception ex)
        {
            // 处理序列化或文件写入错误，例如记录日志
            _logger.Error($"配置文件保存失败: {ex.Message}", "SaveConfig");
        }
    }
}