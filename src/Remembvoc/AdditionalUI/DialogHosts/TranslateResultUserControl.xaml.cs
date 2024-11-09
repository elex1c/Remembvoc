using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace Remembvoc.AdditionalUI.DialogHosts;

public partial class TranslateResultUserControl : UserControl
{
    public string ResultText { get; set; }
    private bool _isCorrect { get; set; }
    
    private static readonly SolidColorBrush CorrectColour = new (Color.FromRgb(28, 122, 48));
    private static readonly SolidColorBrush IncorrectColour = new (Color.FromRgb(214, 32, 15));
    private static readonly PackIconKind CorrectIcon = PackIconKind.TickCircle;
    private static readonly PackIconKind IncorrectIcon = PackIconKind.CrossCircle;
    public string? DialogHostIdentifier { get; set; }

    public TranslateResultUserControl(bool isCorrect)
    {
        InitializeComponent();

        _isCorrect = isCorrect;
        SetFormAppearing();
        DataContext = this;
    }

    private void SetFormAppearing()
    {
        if (_isCorrect)
        {
            ChangeFormColour(CorrectColour);
            ResultIcon.Kind = CorrectIcon;
        }
        else
        {
            ChangeFormColour(IncorrectColour);
            ResultIcon.Kind = IncorrectIcon;
        }
    }
    
    private void ChangeFormColour(SolidColorBrush colour)
    {
        FormBorder.BorderBrush = colour;
        ResultLabel.Foreground = colour;
    }
    
    private void BtnOk_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(DialogHostIdentifier))
            DialogHost.Close(DialogHostIdentifier);
    }
}