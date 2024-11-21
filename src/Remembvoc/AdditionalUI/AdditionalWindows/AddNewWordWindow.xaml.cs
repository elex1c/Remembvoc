using System.Windows;
using System.Windows.Input;
using Remembvoc.AdditionalUI.DialogHosts;
using Remembvoc.Models.ApplicationModels;
using Languages = Remembvoc.Models.Languages;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class AddNewWordWindow : Window, IDisposable
{
    private Action<DatabaseContext, string> ButtonAction { get; set; }
    public List<string> Languages { get; set; }
    public string ButtonText { get; set; }
    private DatabaseContext DbContext { get; }
    private const string DIALOG_HOST_IDENTIFIER = "AddNewWordDialogHost";
    
    public AddNewWordWindow(string btnText, DatabaseContext context)
    {
        ButtonText = btnText;
        DbContext = context;
        Languages = Enum.GetNames(typeof(Languages)).ToList();
        
        InitializeComponent();

        DataContext = this;
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        string phrase = tbUserInput.Text
            .Trim()
            .ToLower();
        string translation = tbTranslation.Text
            .Trim()
            .ToLower();
        
        int langId = DbContext.Languages
            .FirstOrDefault(x => x.ShortForm == cbLanguage.Text)?
            .Id ?? -1;

        bool isWordInDictionary = DbContext.Words
            .FirstOrDefault(x => x.Phrase == phrase) != null;

        if (isWordInDictionary)
        {
            ShowError("You already have this word in your dictionary");
            return;
        }
        if (string.IsNullOrEmpty(translation) || string.IsNullOrEmpty(phrase))
        {
            ShowError("You can't leave text boxes empty");
            return;
        }
        if (langId == -1)
        {
            ShowError("It is not possible to find your language.");
            return;
        }

        Words word = new Words { Phrase = phrase, LanguageId = langId, Translation = translation };
        DbContext.Words.Add(word);
        DbContext.SaveChanges();

        Priorities priority = new Priorities();
        RepetitionAlgorithm.Counting.DefaultSet(priority, 1440);
        priority.Id = word.Id;
        DbContext.Priorities.Add(priority);
        DbContext.SaveChanges();
        
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

    public void Dispose()
    {
        DbContext.Dispose();
    }
}