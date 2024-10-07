using System.Windows;
using System.Windows.Input;

namespace Remembvoc.AdditionalWindows;

public partial class OneBoxOneButton : Window
{
    public string ButtonText { get; set; }
    public OneBoxOneButton(string btnText)
    {
        ButtonText = btnText;
        
        InitializeComponent();

        DataContext = this;
    }

    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OneBoxOneButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}