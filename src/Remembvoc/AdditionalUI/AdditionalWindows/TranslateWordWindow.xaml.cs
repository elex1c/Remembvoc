using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Remembvoc.AdditionalUI.DialogHosts;
using Remembvoc.Helper;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class TranslateWordWindow : Window
{
    private App _app => (App)Application.Current;
    private Words? _word { get; set; }
    private const string DIALOG_HOST_IDENTIFIER = "TranslateWordDialogHost";

    public TranslateWordWindow(string word)
    {
        InitializeComponent();
        
        _word = DbMethods.GetWordElement(word);

        Loaded += GenerateTextAsync;
    }

    private async void GenerateTextAsync(object sender, RoutedEventArgs e)
    {
        if (_word is null)
        {
            ShowError("It is impossible to get your word from database. Try delete and add the word again.");
            
            Close();
            
            return;
        }
        
        string? sentence = await _app.SentenceGenerator.GenerateSentence(_word.Phrase, _word.Language.ShortForm);
        
        if (string.IsNullOrEmpty(sentence))
        {
            ShowError("Error while generating a text. Check you Wi-Fi connection and your API Key.");
            
            Close();
            
            return;
        }

        AddTextToTextBlock(sentence);
    }

    private void AddTextToTextBlock(string text)
    {
        tbGeneratedSentence.Text = "";
        
        string[] parts = text.Split([_word!.Phrase], StringSplitOptions.None);
            
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
        string userInput = tbUserInput.Text.ToLower();
        if (string.IsNullOrEmpty(userInput))
        {
            ShowError("You can't left the input empty");
            return;
        }
        
        if (_word!.Translation == userInput)
        {
            RepetitionAlgorithm.Counting.CountPoints(_word.Priorities,
                true);
            
            // Success message
            await ShowResult(true, "It's correct!");
        }
        else
        {
            RepetitionAlgorithm.Counting.CountPoints(_word.Priorities,
                false);
            
            // Fail message
            await ShowResult(false, "It isn't correct!");
        }

        await using var context = new DatabaseContext();
        context.Priorities.Update(_word.Priorities);
        await context.SaveChangesAsync();
        
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
}