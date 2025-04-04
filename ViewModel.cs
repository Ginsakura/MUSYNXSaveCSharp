using MUSYNCSaveDecode.Model;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MUSYNCSaveDecode;

public partial class ViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// 命令属性
    /// </summary>
    public ICommand? AddUserCommand { get; set; }
    /// <summary>
    /// 命令属性
    /// </summary>
    public ICommand? TestCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 属性更改通知方法
    /// </summary>
    /// <param name="propertyName">发生变化的属性名</param>
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) { return; }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 静态实例对象变量
    /// </summary>
    /// <remarks>全局唯一</remarks>
    private static ViewModel? _instance;

    /// <summary>
    /// 锁对象
    /// </summary>
    private static readonly object _Lock = new();

    /// <summary>
    /// 公共的静态属性，用于访问单例实例
    /// </summary>
    public static ViewModel Instance
    {
        get
        {
            if (_instance == null)
            {
                // 同步锁定以确保线程安全
                lock (_Lock) { _instance ??= new ViewModel(); }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 私有构造函数，防止外部实例化
    /// </summary>
    private ViewModel()
    {
        // 获取当前程序集的版本号
        Assembly assembly = Assembly.GetExecutingAssembly();
        _Version = assembly.GetName().Version ?? new Version(0, 0, 0);
    }

    private readonly Version _Version;
    /// <summary>
    /// 版本号
    /// </summary>
    public String Version => $"{_Version.Major}.{_Version.Minor} build {_Version.Build}";

    private String _SavePath = StaticResource.SavePath;
    /// <summary>
    /// 存档路径字符串
    /// </summary>
    public String SavePath
    { get => _SavePath; set { _SavePath = value; OnPropertyChanged(nameof(_SavePath)); } }

    private String _RepoTip = StaticResource.RepoTip;
    /// <summary>
    /// 仓库提示
    /// </summary>
    public String RepoTip
    { get => _RepoTip; set { _RepoTip = value; OnPropertyChanged(nameof(_RepoTip)); } }

    private Int32 _MapCount = 0;
    /// <summary>
    /// 显示的谱面数量
    /// </summary>
    public Int32 MapCount
    { get => _MapCount; set { _MapCount = value; OnPropertyChanged(nameof(_MapCount)); } }

    private Double _SYNC_Rate = 0.0D;
    /// <summary>
    /// 综合同步率
    /// </summary>
    public Double SYNC_Rate
    { get => _SYNC_Rate; set { _SYNC_Rate = value; OnPropertyChanged(nameof(_SYNC_Rate)); } }
    /// <summary>
    /// 格式化的综合同步率
    /// </summary>
    public string SYNC_RateFormatted => $"{_SYNC_Rate * 100:F6}%";

    private Int32 _ResourceVersion = 20250319;
    /// <summary>
    /// 资源文件版本
    /// </summary>
    public Int32 ResourceVersion
    {
        get => _ResourceVersion;
        set { _ResourceVersion = value; OnPropertyChanged(nameof(_ResourceVersion)); }
    }

    private IsGameRunningLabelPropety _IsGameRunningLabelPropety = new IsGameRunningLabelPropety();
    public IsGameRunningLabelPropety IsGameRunningLabelPropety
    {
        get => _IsGameRunningLabelPropety;
        set { _IsGameRunningLabelPropety = value; OnPropertyChanged(nameof(_IsGameRunningLabelPropety)); }
    }
}