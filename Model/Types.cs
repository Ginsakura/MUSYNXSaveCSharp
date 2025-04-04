using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MUSYNCSaveDecode.Model;

public enum ColorFormat
{
    RGB = 0,
    RGBA,
    ARGB,
    BGR,
    BGRA,
    ABGR,
}

public class IsGameRunningLabelPropety : INotifyPropertyChanged
{
    private string _text = "游戏未运行";
    private SolidColorBrush _foregroundColor = new(Toolkit.StringToColor("#000000"));
    private SolidColorBrush _backgroundColor = new(Toolkit.StringToColor("#FF7F7F"));

    public string Text
    {
        get => _text;
        set { _text = value; OnPropertyChanged(nameof(Text)); }
    }
    public SolidColorBrush ForegroundColor
    {
        get => _foregroundColor;
        set { _foregroundColor = value; OnPropertyChanged(nameof(ForegroundColor)); }
    }
    public SolidColorBrush BackgroundColor
    {
        get => _backgroundColor;
        set { _backgroundColor = value; OnPropertyChanged(nameof(BackgroundColor)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}