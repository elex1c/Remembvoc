using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Remembvoc.Helper;
using Remembvoc.Models.ApplicationModels;
using Remembvoc.SentencesLibraries;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class TranslateWordWindow : Window
{
    private App _app => (App)Application.Current;
    private Words? _word { get; set; }
    
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
            // ERROR
            MessageBox.Show("It is impossible to get your word from database. Try delete and add this word again.");
            
            Close();
            
            return;
        }
        
        string? sentence = await _app.SentenceGenerator.GenerateSentence(_word.Phrase, _word.Language.ShortForm);
        
        if (string.IsNullOrEmpty(sentence))
        {
            MessageBox.Show("Error while generating a text. Check you Wi-Fi connection and your API Key.");
            
            Close();
            
            return;
        }
        
        AddTextToTextBlock(sentence);
    }

    private void AddTextToTextBlock(string text)
    {
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