using System.Windows;
using System.Windows.Input;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class TranslateWord : Window
{
    public TranslateWord()
    {
        InitializeComponent();
    }

    private void BtnConfirmButton_OnClick(object sender, RoutedEventArgs e)
    {
        
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