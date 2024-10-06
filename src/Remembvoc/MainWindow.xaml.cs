using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using Remembvoc.Models;
using Remembvoc.SentencesLibraries;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Remembvoc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
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
            new () { Phrase = "Bonjour", Language = Languages.FRA },
            new () { Phrase = "Ciao", Language = Languages.ITA },
            new () { Phrase = "Hallo", Language = Languages.DEU },
            new () { Phrase = "ПриветПриветПриветПриветПриветПривет", Language = Languages.RUS },
            new () { Phrase = "你好", Language = Languages.CHN },
            new () { Phrase = "こんにちは", Language = Languages.JPN },
            new () { Phrase = "안녕하세요", Language = Languages.KOR },
            new () { Phrase = "Merhaba", Language = Languages.TUR },
            new () { Phrase = "Salam", Language = Languages.ARA }
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
        ISentenceGen gen = new LIamaGen();
        gen.Generate("Počítač", Languages.CES);
        
        Close();
    }

    private void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;
        MessageBox.Show(button!.Tag.ToString());
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}