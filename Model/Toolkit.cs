using Microsoft.Win32;
using System.IO.Compression;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace MUSYNCSaveDecode.Model;

public static class Toolkit
{
    private static readonly Logger _logger = Logger.Instance;

    /// <summary>
    /// 获取系统DPI
    /// </summary>
    /// <returns>double[]: [dpiX, dpiY]</returns>
    public static double[] GetSystemDPI()
    {
        // 创建并启动Stopwatch实例
        Stopwatch stopwatch = Stopwatch.StartNew();

        // 执行目标函数
        {
            Visual window = Application.Current.MainWindow;
            PresentationSource source = PresentationSource.FromVisual(window);
            if (source != null)
            {
                double dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                double dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
                Console.WriteLine($"Window DPI: X = {dpiX}, Y = {dpiY}");
                return [dpiX, dpiY];
            }
        }

        // 停止计时
        stopwatch.Stop();
        // 获取经过的时间，以纳秒为单位
        TimeSpan elapsed = (TimeSpan)stopwatch.Elapsed;
        _logger.Debug($"Function Running... [{elapsed.TotalMilliseconds:F3}ms]", "GetSystemDPI");
        return [0.0, 0.0];
    }

    /// <summary>
    /// 修改游戏控制台样式
    /// </summary>
    public static void ChangeConsoleStyle()
    {
        // 创建并启动Stopwatch实例
        Stopwatch stopwatch = Stopwatch.StartNew();

        // 执行目标函数
        {
            // 修改控制台样式
            Console.WriteLine("Changing Console Style...");

            // 假设 Config 是一个自定义类，用于存储配置信息
            Config config = Config.Instance;
            if (config.MainExecPath == string.Empty) return;
            string execPath = config.MainExecPath.Replace('/', '_') + "musynx.exe";
            Console.WriteLine($"execPath: {execPath}");

            // 打开 HKEY_CURRENT_USER\Console 注册表项
            RegistryKey? consoleKey = Registry.CurrentUser.OpenSubKey("Console", writable: true);
            consoleKey ??= Registry.CurrentUser.CreateSubKey("Console");

            // 创建子项
            using RegistryKey execKey = consoleKey.CreateSubKey(execPath);
            if (execKey != null)
            {
                // 设置 CodePage
                execKey.SetValue("CodePage", 65001, RegistryValueKind.DWord);

                // 设置 WindowSize
                execKey.SetValue("WindowSize", 262174, RegistryValueKind.DWord);

                // 设置 WindowAlpha
                int alphaValue = (int)(config.ConsoleAlpha * 255 / 100);
                execKey.SetValue("WindowAlpha", alphaValue, RegistryValueKind.DWord);

                // 设置字体名称
                execKey.SetValue("FaceName", "霞鹜文楷等宽", RegistryValueKind.String);

                // 设置字体大小
                int fontSizeValue = config.ConsoleFontSize << 16;
                execKey.SetValue("FontSize", fontSizeValue, RegistryValueKind.DWord);
            }
        }

        // 停止计时
        stopwatch.Stop();
        // 获取经过的时间，以纳秒为单位
        TimeSpan elapsed = (TimeSpan)stopwatch.Elapsed;
        _logger.Debug($"Function Running... [{elapsed.TotalMilliseconds:F3}ms]", "ChangeConsoleStyle");
    }

    /// <summary>
    /// 压缩日志文件
    /// </summary>
    public static void CompressLogFile()
    {
        // 创建并启动Stopwatch实例
        Stopwatch stopwatch = Stopwatch.StartNew();

        // 执行目标函数
        {
            // 检查压缩日志文件夹是否存在
            if (!Directory.Exists(StaticResource.LogCompressDir))
            {
                Directory.CreateDirectory(StaticResource.LogCompressDir);
            }

            Int32 nextIndex = Directory.GetFiles(StaticResource.LogCompressDir).Length;
            String compressLogName = $"log.{nextIndex}.gz";

            // 检查日志文件是否存在
            if (File.Exists(StaticResource.LogFilePath))
            {
                // 压缩日志文件
                FileStream fIn = File.OpenRead(StaticResource.LogFilePath);
                GZipStream fOut = new(File.Create(Path.Combine(StaticResource.LogCompressDir, compressLogName)), CompressionMode.Compress);
                fIn.CopyTo(fOut);

                // 关闭文件流
                fIn.Close();
                fIn.Dispose();

                // 关闭压缩流
                fOut.Flush();
                fOut.Close();
                fOut.Dispose();

                // 删除原始日志文件
                File.Delete(StaticResource.LogFilePath);
            }
        }

        // 停止计时
        stopwatch.Stop();
        // 获取经过的时间，以纳秒为单位
        TimeSpan elapsed = (TimeSpan)stopwatch.Elapsed;
        _logger.Debug($"Function Running... [{elapsed.TotalMilliseconds:F3}ms]", "CompressLogFile");
    }

    /// <summary>
    /// 将 RGB 或 ARGB 字符串转换为 System.Windows.Media.Color 类型。
    /// </summary>
    /// <param name="colorString">RGB 或 ARGB 字符串，格式为 "#RRGGBB" 或 "#AARRGGBB"。</param>
    /// <returns>System.Windows.Media.Color</returns>
    public static Color StringToColor(String colorString, ColorFormat format = ColorFormat.RGB)
    {
        // 确保字符串以 "#" 开头
        if (!colorString.StartsWith("#"))
        {
            throw new ArgumentException("Color string must start with '#' character.");
        }

        // 去掉 "#" 符号
        colorString = colorString.TrimStart('#');

        // 检查字符串长度，确定是 RGB 还是 ARGB
        if (colorString.Length == 6) // RGB 格式
        {
            byte r = byte.Parse(colorString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(colorString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(colorString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return Color.FromRgb(r, g, b); // 使用默认的 Alpha 值 255
        }
        else if (colorString.Length == 3)
        {
            byte r = byte.Parse(colorString.Substring(0, 1) + colorString.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(colorString.Substring(1, 1) + colorString.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(colorString.Substring(2, 1) + colorString.Substring(2, 1), System.Globalization.NumberStyles.HexNumber);
            return Color.FromRgb(r, g, b); // 使用默认的 Alpha 值 255
        }
        else if (colorString.Length == 8) // ARGB 格式
        {
            byte a = byte.Parse(colorString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte r = byte.Parse(colorString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(colorString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(colorString.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            return Color.FromArgb(a, r, g, b); // 使用指定的 Alpha 值
        }
        else if (colorString.Length == 4)
        {
            byte a = byte.Parse(colorString.Substring(0, 1) + colorString.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
            byte r = byte.Parse(colorString.Substring(1, 1) + colorString.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(colorString.Substring(2, 1) + colorString.Substring(2, 1), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(colorString.Substring(3, 1) + colorString.Substring(3, 1), System.Globalization.NumberStyles.HexNumber);
            return Color.FromArgb(a, r, g, b); // 使用指定的 Alpha 值
        }
        else
        {
            throw new ArgumentException("Invalid color string length. Must be 3, 4, 6 or 8 characters.");
        }
    }
}