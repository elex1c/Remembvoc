using System.Windows;
using System.Windows.Input;
using Remembvoc.Models.ApplicationModels;
using Languages = Remembvoc.Models.Languages;

namespace Remembvoc.AdditionalUI.AdditionalWindows;

public partial class AddNewWordWindow : Window
{
    private Action<DatabaseContext, string> ButtonAction { get; set; }
    public List<string> Languages { get; set; }
    public string ButtonText { get; set; }
    private DatabaseContext DbContext { get; }
    public string UserInput { get; set; }
    
    public AddNewWordWindow(string btnText, DatabaseContext context)
    {
        ButtonText = btnText;
        DbContext = context;
        Languages = Enum.GetNames(typeof(Languages)).ToList();
        
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
            // Error
            MessageBox.Show("You already have this word in your dictionary");
            return;
        }
        if (string.IsNullOrEmpty(translation) || string.IsNullOrEmpty(phrase))
        {
            // Error
            MessageBox.Show("You can't leave text boxes empty");
            return;
        }
        if (langId == -1)
        {
            // Error
            MessageBox.Show("It is not possible to find your language.");
            return;
        }

        Words word = new Words { Phrase = phrase, LanguageId = langId, Translation = translation };
        DbContext.Words.Add(word);
        DbContext.SaveChanges();

        Priorities priority = new Priorities();
        RepititionAlgorithm.Counting.DefaultSet(priority, 1440);
        priority.Id = word.Id;
        DbContext.Priorities.Add(priority);
        DbContext.SaveChanges();
    }
}