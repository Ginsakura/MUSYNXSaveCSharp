using MUSYNCSaveDecode.Model;
using System.Diagnostics;
using System.Windows;

namespace MUSYNCSaveDecode.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ViewModel vm = ViewModel.Instance;
    private Process[]? process = null;

    public MainWindow()
    {
        InitializeComponent();
        Initialize();
        DataContext = vm;
    }

    private void Initialize()
    {
    }

    private void RefreshBtn_Click(object sender, RoutedEventArgs e)
    {
        //读取存档
    }

    private void OpenFileSelect_Click(object sender, RoutedEventArgs e)
    {
        //打开文件选择器
    }

    private void CheckGameStart()
    {
        process = Process.GetProcessesByName(StaticResource.ProcessName);
        if (process.Length == 0)
        {
            MessageBox.Show("请先启动游戏");
            return;
        }
    }

    private void IsGameRunningLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //通过Steam启动游戏
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlayedBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        new Decode(@"D:\Program Files\steam\steamapps\common\MUSYNX\SavesDir\savedata.sav").Function();
    }
}