using System.Windows;
using System.Windows.Input;
using Remembvoc.Models.ApplicationModels;
using Languages = Remembvoc.Models.Languages;

namespace Remembvoc.AdditionalWindows;

public partial class AddNewWordWindow : Window
{
    private Action<DatabaseContext, string> ButtonAction { get; set; }
    public List<string> Languages { get; set; }
    public string ButtonText { get; set; }
    public DatabaseContext Context { get; }
    public string UserInput { get; set; }
    
    public AddNewWordWindow(string btnText, DatabaseContext context)
    {
        ButtonText = btnText;
        Context = context;
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
        
    }
}