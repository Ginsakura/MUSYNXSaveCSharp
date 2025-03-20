using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MUSYNCSaveDecode.View;

/// <summary>
/// NumericTextBox.xaml 的交互逻辑
/// </summary>
public partial class NumericTextBox : UserControl
{
    public decimal Maximum { get; set; } = 1.0m;
    public decimal Minimum { get; set; } = 0.0m;
    public decimal Step { get; set; } = 0.01m;

    private decimal _Value = 0.10m;

    public decimal Value
    {
        get => _Value;
        set { _Value = value; TextBox.Text = value.ToString(); }
    }

    public NumericTextBox()
    {
        InitializeComponent();
        DataContext = this;
        TextBox.Text = _Value.ToString();
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Allow only digits and negative sign and decimal point
        e.Handled = !decimal.TryParse(e.Text, out _) && !e.Text.Equals("-") && !e.Text.Equals(".");
        if (!decimal.TryParse(TextBox.Text, out _)) TextBox.Text = "0";
    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            // 当用户按下 Enter 键时，触发 TextChanged 事件
            if (sender is TextBox textBox)
            {
                OnTextChanged(new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
            }
            e.Handled = true; // 阻止进一步处理 Enter 键，例如提交表单
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        // 当 TextBox 失焦时，触发 TextChanged 事件
        if (sender is TextBox textBox && Value.ToString() != textBox.Text)
        {
            OnTextChanged(new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
        }
    }

    // 调用 TextChanged 事件的处理程序
    protected virtual void OnTextChanged(TextChangedEventArgs e)
    {
        if (decimal.TryParse(TextBox.Text, out decimal newValue))
        {
            if (newValue < Minimum)
            {
                Value = Minimum;
                DecreaseButton.IsEnabled = false;
                IncreaseButton.IsEnabled = true;
            }
            else if (newValue > Maximum)
            {
                Value = Maximum;
                DecreaseButton.IsEnabled = true;
                IncreaseButton.IsEnabled = false;
            }
            else
            {
                Value = newValue;
            }
        }
    }

    private void IncreaseButton_Click(object sender, RoutedEventArgs e)
    {
        Value += Step;
        if (Value >= Maximum)
        {
            Value = Maximum;
            IncreaseButton.IsEnabled = false;
        }
        if (!DecreaseButton.IsEnabled)
            DecreaseButton.IsEnabled = true;
    }

    private void DecreaseButton_Click(object sender, RoutedEventArgs e)
    {
        Value -= Step;
        if (Value <= Minimum)
        {
            Value = Minimum;
            DecreaseButton.IsEnabled = false;
        }
        if (!IncreaseButton.IsEnabled)
            IncreaseButton.IsEnabled = true;
    }

    private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        // 在这里处理鼠标滚轮事件
        // 'e.Delta' 表示滚轮的移动量，正值表示向上滚动，负值表示向下滚动

        if (e.Delta > 0)
        {
            // 处理向上滚动
            Value += Step;
            if (Value >= Maximum)
            {
                Value = Maximum;
                IncreaseButton.IsEnabled = false;
            }
            if (!DecreaseButton.IsEnabled)
                DecreaseButton.IsEnabled = true;
        }
        else
        {
            // 处理向下滚动
            Value -= Step;
            if (Value <= Minimum)
            {
                Value = Minimum;
                DecreaseButton.IsEnabled = false;
            }
            if (!IncreaseButton.IsEnabled)
                IncreaseButton.IsEnabled = true;
        }

        // 如果你不希望事件继续冒泡，可以设置 e.Handled = true;
        //e.Handled = true;
    }
}