using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;

namespace MUSYNCSaveDecode.Model;

public static class Toolkit
{
    /// <summary>
    /// 获取系统DPI
    /// </summary>
    /// <returns>double[]: [dpiX, dpiY]</returns>
    public static double[] GetSystemDPI()
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
        return [0.0, 0.0];
    }

    public static void ChangeConsoleStyle()
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
}