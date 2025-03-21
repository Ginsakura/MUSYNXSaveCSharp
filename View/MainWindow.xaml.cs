using MUSYNCSaveDecode.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace MUSYNCSaveDecode.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ViewModel vm = ViewModel.Instance;
    private Process[]? process = null;
    private Thread gameRunningThread;
    private Boolean _onClosing = false;
    private readonly Logger _logger = Logger.Instance;

    public MainWindow()
    {
        InitializeComponent();
        Initialize();
        gameRunningThread = new Thread(CheckGameRunning) { Name = "CheckGameRunningThread" };
        DataContext = vm;
    }

    private void Initialize()
    {
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        _onClosing = true;
        gameRunningThread.Join();
    }

    private void CheckGameRunning()
    {
        _logger.Info("Created CheckGameRunning Thread", "CheckGameRunning");
        Int16 counter = 0;
        while (!_onClosing)
        {
            if (counter == 0)
            {
                // 创建并启动Stopwatch实例
                Stopwatch stopwatch = Stopwatch.StartNew();

                // 执行目标函数
                {
                    String content;
                    SolidColorBrush foreground;
                    SolidColorBrush background;
                    process = Process.GetProcessesByName(StaticResource.ProcessName);
                    if (process == null)
                    {
                        content = "游戏未启动";
                        foreground = new SolidColorBrush(Toolkit.StringToColor("#000000"));
                        background = new SolidColorBrush(Toolkit.StringToColor("#FF7F7F"));
                    }
                    else if (process.Length == 1)
                    {
                        content = $"PID:{process[0].Id}";
                        foreground = new SolidColorBrush(Toolkit.StringToColor("#000000"));
                        background = new SolidColorBrush(Toolkit.StringToColor("#98E22B"));
                    }
                    else
                    {
                        content = $"多个游戏实例";
                        foreground = new SolidColorBrush(Toolkit.StringToColor("#FF0505"));
                        background = new SolidColorBrush(Toolkit.StringToColor("#FFBF7F"));
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                        {
                            IsGameRunningLabel.Content = content;
                            IsGameRunningLabel.Foreground = foreground;
                            IsGameRunningLabel.Background = background;
                        });
                    counter = 50;
                }
                // 停止计时
                stopwatch.Stop();
                // 获取经过的时间，以纳秒为单位
                TimeSpan elapsed = (TimeSpan)stopwatch.Elapsed;
                _logger.Debug($"Function Running... [{elapsed.TotalMilliseconds:F3}ms]", "CheckGameRunning");
            }
            else
                counter--;
        }
        _logger.Warn("Stoped CheckGameRunning Thread", "CheckGameRunning");
    }

    private void RefreshBtn_Click(object sender, RoutedEventArgs e)
    {
        //读取存档
    }

    private void OpenFileSelect_Click(object sender, RoutedEventArgs e)
    {
        //打开文件选择器
    }

    private void IsGameRunningLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //通过Steam启动游戏
    }

    private void PlayedBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void UnPlayBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void FavoBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RankExBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void BlackExBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RedEx_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RankSBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RankABtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RankBBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RankCBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void KeysSelectorBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void DiffcutySelectorBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void SongsSelectorBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void AdvancedFeatures_Click(object sender, RoutedEventArgs e)
    {
    }

    private void ScorePlot_Click(object sender, RoutedEventArgs e)
    {
    }

    private void SettingBtn_Click(object sender, RoutedEventArgs e)
    {
        new Decode(@"D:\Program Files\steam\steamapps\common\MUSYNX\SavesDir\savedata.sav").DecodeSaveFile();
    }
}