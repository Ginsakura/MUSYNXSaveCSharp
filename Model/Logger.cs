using System.Diagnostics;
using System.IO;

namespace MUSYNCSaveDecode.Model;

public partial class Logger
{
    private static Logger? _instance;
    private static readonly object _Lock = new();
    // 初始化 TraceSource
    private static readonly TraceSource traceSource = new("MUSYNC Save Decoder Logger");

    /// <summary>
    /// 公共的静态属性，用于访问单例实例
    /// </summary>
    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                // 同步锁定以确保线程安全
                lock (_Lock) { _instance ??= new Logger(); }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 私有构造函数，防止外部实例化
    /// </summary>
    private Logger()
    {
        // 添加控制台监听器
        TextWriterTraceListener consoleListener = new(Console.Out)
        {
            Name = "ConsoleListener"
        };
        traceSource.Listeners.Add(consoleListener);

        // 添加文件监听器
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        TextWriterTraceListener fileListener = new(logFilePath)
        {
            Name = "FileListener"
        };
        traceSource.Listeners.Add(fileListener);

        // 设置日志格式
        traceSource.Switch.Level = Config.Instance.LoggerFilter;
        traceSource.Listeners.Add(new ConsoleTraceListener());
        traceSource.TraceInformation("Logger initialized.");
    }

    public void Log(string message, TraceEventType eventType = TraceEventType.Information)
    {
        traceSource.TraceEvent(eventType, 0, message);
        traceSource.Flush();
    }
}