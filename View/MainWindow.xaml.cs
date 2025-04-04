using Microsoft.Win32;
using MUSYNCSaveDecode.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using static MUSYNCSaveDecode.Model.StaticResource;

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
        gameRunningThread = new Thread(CheckGameRunning) { Name = "CheckGameRunningThread" };
        InitializeComponent();
        Initialize();
        DataContext = vm;
    }

    private void Initialize()
    {
        // 设置窗口位置
        Left = (SystemParameters.WorkArea.Width - Width) / 2;
        Top = (SystemParameters.WorkArea.Height - Height) / 2;
        // 设置窗口样式
        //WindowStyle = WindowStyle.SingleBorderWindow;
        //ResizeMode = ResizeMode.CanResizeWithGrip;
        // 启动检查游戏运行状态的线程
        gameRunningThread.Start();
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
                    process = Process.GetProcessesByName(StaticResource.ProcessName);
                    if (process == null || process.Length == 0)
                    {
                        vm.IsGameRunningLabelPropety.Text = "游戏未启动";
                        vm.IsGameRunningLabelPropety.ForegroundColor = BlackColor;
                        vm.IsGameRunningLabelPropety.BackgroundColor = PinkColor;
                    }
                    else if (process.Length == 1)
                    {
                        vm.IsGameRunningLabelPropety.Text = $"PID:{process[0].Id}";
                        vm.IsGameRunningLabelPropety.ForegroundColor = BlackColor;
                        vm.IsGameRunningLabelPropety.BackgroundColor = GreenColor;
                    }
                    else
                    {
                        vm.IsGameRunningLabelPropety.Text = $"多个游戏实例";
                        vm.IsGameRunningLabelPropety.ForegroundColor = RedColor;
                        vm.IsGameRunningLabelPropety.BackgroundColor = OrangeColor;
                    }
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
            Thread.Sleep(100);
        }
        _logger.Warn("Stoped CheckGameRunning Thread", "CheckGameRunning");
    }

    private void RefreshBtn_Click(object sender, RoutedEventArgs e)
    {
        //读取存档
        (new Decode(vm.SavePath)).DecodeSaveFile();
    }

    private void OpenFileSelect_Click(object sender, RoutedEventArgs e)
    {
        //打开文件选择器
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "MUSYNX存档文件(*.sav)|*.sav",
            Title = "选择存档文件",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        };
        if (openFileDialog.ShowDialog() == true)
        {
            SavePathTBox.Text = vm.SavePath = openFileDialog.FileName;
        }
        else
        {
            vm.SavePath = StaticResource.SavePath;
        }
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