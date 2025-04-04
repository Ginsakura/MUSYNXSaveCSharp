using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Timers;
using static MUSYNCSaveDecode.Model.StaticResource;
using Timer = System.Timers.Timer;

namespace MUSYNCSaveDecode.Model;

/// <summary>
/// 日志记录器类
/// </summary>
public class Logger
{
    private LogLevel _currentLogLevel = LogLevel.Verbose; // 当前日志级别
    private readonly string _logFilePath; // 日志文件路径
    private readonly FileStream _logFileStream; // 文件流对象
    private bool _isWritten = false; // 标志位，用于标记是否有数据写入
    private readonly Timer _timer; // 定时器
    private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger()); // 日志对象的单例实例

    // 私有构造函数，防止外部直接实例化
    private Logger()
    {
        // 设置日志文件路径，可以根据需要修改
        _logFilePath = LogFilePath;
        // 打开文件流，设置为追加模式
        _logFileStream = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, FileOptions.None);
        // 初始化定时器，每隔5秒检查一次
        _timer = new Timer(5000);
        _timer.Elapsed += Timer_Elapsed;
        _timer.Start();
    }

    // 提供访问单例实例的公共属性
    public static Logger Instance => _instance.Value;

    // 设置日志级别
    public void SetLogLevel(LogLevel level)
    {
        _currentLogLevel = level;
    }

    // 定时器回调方法
    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        // 检查标志位
        if (_isWritten)
        {
            // 将缓冲区的数据写入硬盘
            _logFileStream.Flush();
            _isWritten &= false; // 重置标志位
            Debug("数据已写入硬盘", "Logger");
        }
    }

    public void Close()
    {
        _timer.Stop();
        _timer.Dispose();
        _logFileStream.Flush();
        _logFileStream.Close();
        _logFileStream.Dispose();
        Console.WriteLine("文件流和定时器已关闭");
    }

    // 日志记录的公共方法
    public void Log(LogLevel level, string message, string functionName = "")
    {
        // 根据日志级别过滤
        if (level >= _currentLogLevel)
        {
            string formattedMessage = FormatLogMessage(level, message, functionName);
            WriteToConsole(formattedMessage);
            WriteToFile(formattedMessage);
        }
    }

    // 不同级别的日志方法
    public void Debug(string message, string functionName = "")
    {
        Log(LogLevel.Verbose, message, functionName);
    }

    public void Info(string message, string functionName = "")
    {
        Log(LogLevel.Information, message, functionName);
    }

    public void Warn(string message, string functionName = "")
    {
        Log(LogLevel.Warning, message, functionName);
    }

    public void Error(string message, string functionName = "")
    {
        Log(LogLevel.Error, message, functionName);
    }

    public void Fatal(string message, string functionName = "")
    {
        Log(LogLevel.Critical, message, functionName);
    }

    // 格式化日志消息
    private string FormatLogMessage(LogLevel level, string message, string functionName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{LogCount} - ");
        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.Append($" - [{level.ToString().ToUpper()}]");
        if (!string.IsNullOrEmpty(functionName))
        {
            sb.Append($" - [{functionName}]");
        }
        sb.Append($" - {message}\r\n");
        IncrementLogCount();
        return sb.ToString();
    }

    // 写入控制台
    private void WriteToConsole(string message)
    {
        Console.Write(message);
    }

    // 写入数据到文件流
    public void WriteToFile(string data)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        lock (this)
        {
            _logFileStream.Write(dataBytes, 0, dataBytes.Length);
        }
        _isWritten |= true; // 标记数据已写入
    }
}