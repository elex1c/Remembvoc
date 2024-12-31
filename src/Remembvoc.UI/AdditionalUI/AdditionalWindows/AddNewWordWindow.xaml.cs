using System.Windows;
using System.Windows.Input;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.UI.AdditionalUI.DialogHosts;
using Remembvoc.UI.Models.ApplicationModels;
using Models_Languages = Remembvoc.UI.Models.Languages;

namespace Remembvoc.UI.AdditionalUI.AdditionalWindows;

public partial class AddNewWordWindow : Window
{
    private readonly IWordService _wordService;
    
    public List<string> Languages { get; set; }
    public string ButtonText { get; set; }
    private const string DIALOG_HOST_IDENTIFIER = "AddNewWordDialogHost";
    
    public AddNewWordWindow(string btnText, IWordService wordService)
    {
        _wordService = wordService;
        ButtonText = btnText;
        Languages = Enum.GetNames(typeof(Models_Languages)).ToList();
        
        InitializeComponent();

        DataContext = this;
    }

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        var response = await _wordService.AddWordAsync(tbUserInput.Text, cbLanguage.Text, tbTranslation.Text);

        if (!response.IsValid)
        {
            ShowError(response.ErrorMessage!);
            return;
        }
        
        Close();
    }

    private async void ShowError(string errorText)
    {
        var errorDialog = new ErrorDialogUserControl { ErrorText = errorText, DialogHostIdentifier = DIALOG_HOST_IDENTIFIER };
        
        await MaterialDesignThemes.Wpf.DialogHost.Show(errorDialog, DIALOG_HOST_IDENTIFIER);
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