using System.Collections.ObjectModel;
using System.Windows;
using Remembvoc.Models;

namespace Remembvoc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<FileName> ItemsSource;
    private ObservableCollection<Models.Word> ItemsSource;

    public MainWindow()
    {
        InitializeComponent();

        ((App)Application.Current).BackgroundIcon.SetWindow(this);

        #region Test data settings

        ItemsSource = new ObservableCollection<Models.Word> 
        {
            new () { Phrase = "Hello", Language = Languages.ENG },
            new () { Phrase = "Ahoj", Language = Languages.CES },
        };
        myDG.ItemsSource = ItemsSource;
        
        #endregion
    }

    protected override void OnClosed(EventArgs e)
    {
        ((App)Application.Current).BackgroundIcon.SetWindow(null);

        base.OnClosed(e);
    }

    private void MWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}