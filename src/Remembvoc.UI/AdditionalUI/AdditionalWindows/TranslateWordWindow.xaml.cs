using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;
using Remembvoc.UI.AdditionalUI.DialogHosts;

namespace Remembvoc.UI.AdditionalUI.AdditionalWindows;

public partial class TranslateWordWindow : Window, IDisposable
{
    private readonly ITranslationService<WordTranslationResponse> _translationService;
    private Word _word { get; set; }
    private const string DIALOG_HOST_IDENTIFIER = "TranslateWordDialogHost";

    public TranslateWordWindow(ITranslationService<WordTranslationResponse> translationService)
    {
        _translationService = translationService;

        _word = new Word();
        
        InitializeComponent();

        Loaded += GenerateTextAsync;
    }

    public void AddPhrase(string word)
    {
        _word.Phrase = word;
    }
    
    private async void GenerateTextAsync(object sender, RoutedEventArgs e)
    {
        var generatorResponse = await _translationService.GenerateSentenceAsync(_word.Phrase);
        
        if (!generatorResponse.IsSuccessRequest)
        {
            ShowError(generatorResponse.ErrorMessage);
            
            Close();
            
            return;
        }

        AddTextToTextBlock(generatorResponse.Sentence);
    }

    private void AddTextToTextBlock(string text)
    {
        tbGeneratedSentence.Text = "";
        
        string[] parts = text.Split([_word.Phrase], StringSplitOptions.None);
            
        for (int i = 0; i < parts.Length; i++)
        {
            tbGeneratedSentence.Inlines.Add(new Run(parts[i]));

            if (i < parts.Length - 1)
            {
                var underlinedWord = new Run(_word.Phrase)
                {
                    TextDecorations = TextDecorations.Underline
                };
                tbGeneratedSentence.Inlines.Add(underlinedWord);
            }
        }
    }
    
    private async void BtnConfirmButton_OnClick(object sender, RoutedEventArgs e)
    {
        var translationResponse = await _translationService.CheckTranslationAsync(tbUserInput.Text);

        switch (translationResponse.State)
        {
            case TranslationStates.Translated:
                await ShowResult(true, translationResponse.Message);
                break;
            case TranslationStates.NotTranslated:
                await ShowResult(false, translationResponse.Message);
                break;
            case TranslationStates.IncorrectInput:
                await ShowResult(false, translationResponse.Message);
                return;
        }
        
        Close();
    }

    private async void ShowError(string errorText)
    {
        var errorDialog = new ErrorDialogUserControl { ErrorText = errorText, DialogHostIdentifier = DIALOG_HOST_IDENTIFIER };
        
        await MaterialDesignThemes.Wpf.DialogHost.Show(errorDialog, DIALOG_HOST_IDENTIFIER);
    }
    
    private async Task ShowResult(bool isCorrect, string resultText)
    {
        var errorDialog = new TranslateResultUserControl(isCorrect)
        {
            ResultText = resultText,
            DialogHostIdentifier = DIALOG_HOST_IDENTIFIER
        };
        
        await MaterialDesignThemes.Wpf.DialogHost.Show(errorDialog, DIALOG_HOST_IDENTIFIER);
    }
    
    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TranslateWord_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    public void Dispose()
    {
        Loaded -= GenerateTextAsync;
    }
}