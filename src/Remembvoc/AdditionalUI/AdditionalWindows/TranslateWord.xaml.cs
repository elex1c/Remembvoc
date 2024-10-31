using System.Windows;
using System.Windows.Input;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class TranslateWord : Window
{
    public string Word { get; set; }

    public TranslateWord()
    {
        InitializeComponent();
    }

    private void BtnConfirmButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO: Complete this window
    }

    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TranslateWord_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}