using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Remembvoc.AdditionalWindows;
using Remembvoc.Models;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Remembvoc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<Word> ItemsSource;
    public DatabaseContext DbContext { get; set; }

    private int CurrentPageNumber { get; set; } = 1;
    private const int ElementsPerPage = 15;

    public MainWindow()
    {
        InitializeComponent();

        ((App)Application.Current).BackgroundIcon.SetWindow(this);

        DbContext = ((App)Application.Current).DatabaseContext;

        ItemsSource = new ObservableCollection<Word>();
        LoadWordsToCurrentPage();
    }

    private void LoadWordsToCurrentPage()
    {
        var words = DbContext.Words
            .Include(w => w.Language)
            .OrderBy(x => x.Id)
            .Skip((CurrentPageNumber * ElementsPerPage) - ElementsPerPage)
            .Take(ElementsPerPage)
            .ToList();

        List<Word> list = new();
        foreach (var word in words)
            list.Add((Word)word);

        ItemsSource = new ObservableCollection<Word>(list);
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
        /*ISentenceGen gen = new LIamaGen();
        gen.Generate("ЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪЪ", Languages.Italian);*/
        
        Close();
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;
        MessageBox.Show(button!.Tag.ToString());
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        AddNewWordWindow w = new ("Add new word", DbContext);
        w.ShowDialog();
    }
}