using MUSYNCSaveDecode.Model;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace MUSYNCSaveDecode.View;

/// <summary>
/// Config.xaml 的交互逻辑
/// </summary>
public partial class Config : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private String _Language = "zh-cn";
    private String _LoggerFilterString = "INFO";
    private Boolean _Acc_Sync = false;
    private Boolean _CheckUpdate = true;
    private Boolean _DLLInjection = false;
    private Int32 _SystemDPI = 100;
    private Boolean _DonutChartinHitDelay = true;
    private Boolean _DonutChartinAllHitAnalyze = true;
    private Boolean _NarrowDelayInterval = true;
    private Boolean _ChangeConsoleStyle = false;
    private Int32 _ConsoleAlpha = 75;
    private String _ConsoleFont = "霞鹜文楷等宽";
    private Int32 _ConsoleFontSize = 36;
    private String _MainExecPath = "";
    private Boolean _DefaultKeys = false;
    private Int32 _DefaultDiffcute = 0;

    public new String Language
    {
        get => _Language;
        set { _Language = value; OnPropertyChanged(nameof(Language)); }
    }

    public Int32 SystemDPI
    {
        get => _SystemDPI;
        set { _SystemDPI = value; OnPropertyChanged(nameof(SystemDPI)); }
    }

    public Boolean Acc_Sync
    {
        get => _Acc_Sync;
        set { _Acc_Sync = value; OnPropertyChanged(nameof(Acc_Sync)); }
    }

    public Boolean CheckUpdate
    {
        get => _CheckUpdate;
        set { _CheckUpdate = value; OnPropertyChanged(nameof(CheckUpdate)); }
    }

    public Boolean DLLInjection
    {
        get => _DLLInjection;
        set { _DLLInjection = value; OnPropertyChanged(nameof(DLLInjection)); }
    }

    public Boolean DonutChartinHitDelay
    {
        get => _DonutChartinHitDelay;
        set { _DonutChartinHitDelay = value; OnPropertyChanged(nameof(DonutChartinHitDelay)); }
    }

    public Boolean DonutChartinAllHitAnalyze
    {
        get => _DonutChartinAllHitAnalyze;
        set { _DonutChartinAllHitAnalyze = value; OnPropertyChanged(nameof(DonutChartinAllHitAnalyze)); }
    }

    public Boolean NarrowDelayInterval
    {
        get => _NarrowDelayInterval;
        set { _NarrowDelayInterval = value; OnPropertyChanged(nameof(NarrowDelayInterval)); }
    }

    public Boolean ChangeConsoleStyle
    {
        get => _ChangeConsoleStyle;
        set { _ChangeConsoleStyle = value; OnPropertyChanged(nameof(ChangeConsoleStyle)); }
    }

    public Int32 ConsoleAlpha
    {
        get => _ConsoleAlpha;
        set { _ConsoleAlpha = value; OnPropertyChanged(nameof(ConsoleAlpha)); }
    }

    public String ConsoleFont
    {
        get => _ConsoleFont;
        set { _ConsoleFont = value; OnPropertyChanged(nameof(ConsoleFont)); }
    }

    public Int32 ConsoleFontSize
    {
        get => _ConsoleFontSize;
        set { _ConsoleFontSize = value; OnPropertyChanged(nameof(ConsoleFontSize)); }
    }

    public String MainExecPath
    {
        get => _MainExecPath;
        set { _MainExecPath = value; OnPropertyChanged(nameof(MainExecPath)); }
    }

    public Int32 DefaultKeys
    {
        get => _DefaultKeys ? 1 : 0;
        set { _DefaultKeys = value == 1; OnPropertyChanged(nameof(DefaultKeys)); }
    }

    public Int32 DefaultDiffcute
    {
        get => _DefaultDiffcute;
        set { _DefaultDiffcute = value; OnPropertyChanged(nameof(DefaultDiffcute)); }
    }

    public Config()
    {
        InitializeComponent();
        LoadLanguages();
    }

    /// <summary>
    /// 加载语言文件夹中的所有语言文件，并将语言名称添加到ComboBox中。
    /// 语言文件应为JSON格式，文件名为语言代码（如"zh-cn.json"）。
    /// </summary>
    private void LoadLanguages()
    {
        string langFolder = "lang"; // 语言文件夹路径
        string[] files = Directory.GetFiles(langFolder, "*.json", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file); // 获取文件名（如"zh-cn.json"）
            string languageCode = Path.GetFileNameWithoutExtension(fileName); // 获取语言代码（如"zh-cn"）

            if (StaticResource.languageNames.TryGetValue(languageCode, out string languageName))
            {
                LanguageCfg.Items.Add(languageName); // 将语言名称添加到ComboBox中
            }
        }
    }

    private void LanguageCfg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }

    private void LoggerFilterCfg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }

    private void Acc_SyncCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void CheckUpdateCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void DLLInjectionCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void DonutChartinHitDelayCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void DonutChartinAllHitAnalyzeCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void NarrowDelayIntervalCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void ChangeConsoleStyleCfg_Checked(object sender, RoutedEventArgs e)
    {
    }

    private void ConsoleFontCfg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }

    private void MainExecPathCfg_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
    }

    private void DefaultKeysCfg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }

    private void DefaultDiffcuteCfg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }
}