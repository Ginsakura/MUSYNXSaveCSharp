using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
    private void OnPropertyChanged(string propertyName)
    {
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
    }

    private String _SavePath = "Input SaveFile or AnalyzeFile Path (savedata.sav)";
    /// <summary>
    /// 存档路径字符串
    /// </summary>
    public String SavePath
    { get => _SavePath; set { _SavePath = value; OnPropertyChanged(nameof(_SavePath)); } }

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

    private String _ResourceVersion = "20250319";
    /// <summary>
    /// 资源文件版本
    /// </summary>
    public Int32 ResourceVersion
    {
        get => Int32.Parse(_ResourceVersion);
        set
        {
            _ResourceVersion = value.ToString();
            OnPropertyChanged(nameof(_ResourceVersion));
        }
    }
}