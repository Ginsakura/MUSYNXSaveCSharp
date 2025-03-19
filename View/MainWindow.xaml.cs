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
        process = Process.GetProcessesByName(StaticObject.ProcessName);
        if (process.Length == 0)
        {
            MessageBox.Show("请先启动游戏");
            return;
        }
    }
}