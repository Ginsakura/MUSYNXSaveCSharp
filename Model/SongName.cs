using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MUSYNCSaveDecode.Model;

public partial class SongName : INotifyPropertyChanged
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
    private static SongName? _instance;

    /// <summary>
    /// 锁对象
    /// </summary>
    private static readonly object _Lock = new();

    /// <summary>
    /// 公共的静态属性，用于访问单例实例
    /// </summary>
    public static SongName Instance
    {
        get
        {
            if (_instance == null)
            {
                // 同步锁定以确保线程安全
                lock (_Lock) { _instance ??= new SongName(); }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 私有构造函数，防止外部实例化
    /// </summary>
    private SongName()
    {
    }
}