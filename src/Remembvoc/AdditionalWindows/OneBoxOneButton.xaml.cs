using System.Windows;
using System.Windows.Input;

namespace Remembvoc.AdditionalWindows;

public partial class OneBoxOneButton : Window
{
    private Action<DatabaseContext, string> ButtonAction { get; set; }
    private DatabaseContext _dbContext { get; set; }
    
    public string ButtonText { get; set; }
    public string UserInput { get; set; }
    
    public OneBoxOneButton(string btnText, DatabaseContext context, Action<DatabaseContext, string> action)
    {
        ButtonText = btnText;
        ButtonAction = action;
        _dbContext = context;
        
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

    private void Button_OnClick(object sender, RoutedEventArgs e) => ButtonAction.Invoke(_dbContext, tbUserInput.Text);
}