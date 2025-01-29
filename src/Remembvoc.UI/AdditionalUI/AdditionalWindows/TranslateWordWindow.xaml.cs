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
    private readonly Func<IWordService> _wordService;
    private Word _word { get; set; }
    private const string DIALOG_HOST_IDENTIFIER = "TranslateWordDialogHost";

    private bool _isClosed { get; set; }
    
    public TranslateWordWindow(ITranslationService<WordTranslationResponse> translationService, Func<IWordService> wordService)
    {
        _translationService = translationService;
        _wordService = wordService;

        _word = new Word();
        
        InitializeComponent();

        Loaded += GenerateTextAsync;
    }

    public void AddPhrase(string word)
    {
        _word = new Word
        {
            Phrase = word
        };
    }

    public void LoadNewWindow()
    {
        tbUserInput.Text = "";
        tbGeneratedSentence.Text = "Generating..";

        GenerateTextAsync(this, new RoutedEventArgs());
    }
    
    private async void GenerateTextAsync(object sender, RoutedEventArgs e)
    {
        var generatorResponse = await _translationService.GenerateSentenceAsync(_word.Phrase);
        
        if (!generatorResponse.IsSuccessRequest)
        {
            ShowError(generatorResponse.ErrorMessage);

            CloseWindow();
            
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
        
        var wordService = _wordService();
        
        switch (translationResponse.State)
        {
            case TranslationStates.NotTranslated:
                await ShowResult(false, translationResponse.Message);
                await wordService.GetAndSendUpdatedDataAsync();
                break;
            case TranslationStates.Translated:
                await ShowResult(true, translationResponse.Message);
                await wordService.GetAndSendUpdatedDataAsync();
                break;
            case TranslationStates.IncorrectInput:
                await ShowResult(false, translationResponse.Message);
                return;
        }
        
        CloseWindow();
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
        CloseWindow();
    }

    private void CloseWindow()
    {
        _isClosed = true;
        
        Hide();
    }
    
    private void TranslateWord_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    public bool IsClosed => _isClosed;
    
    public void Dispose()
    {
        Loaded -= GenerateTextAsync;
    }
}