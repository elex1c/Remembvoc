using System.Collections.ObjectModel;
using System.Windows;

namespace Remembvoc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<FileName> ItemsSource;

    public MainWindow()
    {
        InitializeComponent();

        ((App)Application.Current).BackgroundIcon.SetWindow(this);

        ItemsSource = new ObservableCollection<FileName> 
        {
            new () { Word = "Hello", Language = "ENG" },
            new () { Word = "Ahoj", Language = "CES" },
        };
        myList.ItemsSource = ItemsSource;
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