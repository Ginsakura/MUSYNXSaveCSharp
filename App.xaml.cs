using MUSYNCSaveDecode.Model;
using System.Diagnostics;
using System.Windows;

namespace MUSYNCSaveDecode;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly Logger _logger = Logger.Instance;

    protected override void OnStartup(StartupEventArgs e)
    {
        _logger.Debug(">>>>  Software Startuping  <<<<", "OnStartup");
        RegisterEvents();
        Toolkit.CompressLogFile();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _logger.Debug(">>>>  Software Exiting  <<<<", "OnExit");
        base.OnExit(e);
        _logger.Close();
        // 这里可以放置清理代码
        Environment.Exit(0);
    }

    private void RegisterEvents()
    {
        //Task线程内未捕获异常处理事件
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException; //Task异常

        //UI线程未捕获异常处理事件（UI主线程）
        DispatcherUnhandledException += App_DispatcherUnhandledException;

        //非UI线程未捕获异常处理事件(子线程)
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        try
        {
            if (e.Exception is Exception exception)
            {
                HandleException(exception);
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            e.SetObserved();
        }
    }

    /// <summary>
    /// 非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            if (e.ExceptionObject is Exception exception)
            {
                HandleException(exception);
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            //ignore
        }
    }

    /// <summary>
    /// UI线程未捕获异常处理事件（UI主线程）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        try
        {
            HandleException(e.Exception);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            e.Handled = true;
        }
    }

    private static void HandleException(Exception ex)
    {
        _logger.Fatal(">>>>  Software Exception  <<<<", "HandleException");
        StackTrace stackTrace = new(ex, true); // true 表示捕获源信息
        StackFrame[] stackFrames = stackTrace.GetFrames(); // 获取堆栈帧
        string file = "\r\nfile：";
        //string lin = "line：";
        foreach (StackFrame frame in stackFrames)
        {
            string? fileName = frame.GetFileName(); // 获取文件名
            if (fileName is null) continue;
            int line = frame.GetFileLineNumber(); // 获取行号
            file += $"[{fileName}---{line}]\r\n";
            //Console.WriteLine($"Error in file: {fileName}, line: {line}");
        }
        _logger.Fatal(file, "HandleException");

        MessageBox.Show("系统出错，请与开发人员联系：\r\n" + ex.Message);
        MessageBox.Show($"Error in  {file}");
        _logger.Close();
        Environment.Exit(0);
    }
}