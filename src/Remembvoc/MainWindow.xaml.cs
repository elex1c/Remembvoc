using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Remembvoc.AdditionalUI.AdditionalWindows;
using Remembvoc.Helper;
using Remembvoc.Models;
using Remembvoc.Models.ApplicationModels;
using AddNewWordWindow = Remembvoc.AdditionalUI.AdditionalWindows.AddNewWordWindow;
using Application = System.Windows.Application;

namespace Remembvoc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DatabaseContext DbContext { get; set; }
    private App _app => (App)Application.Current;

    private int CurrentPageNumber { get; set; } = 1;
    private const int ElementsPerPage = 11;

    private ObservableCollection<Word> WordsList { get; set; } = [];
    private int TotalWordsAmount => DbContext.Words.Count();
    private int LastPage => (int)Math.Ceiling(TotalWordsAmount / (double)ElementsPerPage);
    private bool IsMovingOnNextPage => (TotalWordsAmount - 1) % ElementsPerPage == 0;
    
    public MainWindow()
    {
        InitializeComponent();

        _app.BackgroundIcon.SetWindow(this);
        
        DbContext = ((App)Application.Current).DatabaseContext;
        
        Loaded += (_, _) => BasicStartMethods();
    }

    private void BasicStartMethods()
    {
        // Updates time since last check to word revising
        DbMethods.UpdateTimeInPriorities();
        // Loads words from vocabulary
        LoadWordsToCurrentPage(true);
        // Loads words to translate
        LoadWordsToTranslate();
    }

    private void UpdateWordsList()
    {
        var wordsList = DbContext.Words
            .Include(w => w.Language)
            .OrderBy(x => x.Id)
            .Skip(CurrentPageNumber * ElementsPerPage - ElementsPerPage)
            .Take(ElementsPerPage)
            .ToList();
        
        WordsList.Clear();
        foreach (var word in wordsList) WordsList.Add((Word)word);
    }
    
    private void LoadPageButtons()
    {
        if (TotalWordsAmount <= ElementsPerPage)
        {
            gridPageButtons.Visibility = Visibility.Collapsed;
            CurrentPageNumber = 1;
            tbPageNumber.Text = CurrentPageNumber.ToString();
            LoadWordsToCurrentPage(false);
        }
        else
        {
            if (CurrentPageNumber == 1)
            {
                btnPlusPage.IsEnabled = true;
                btnMinusPage.IsEnabled = false;
            }
            else if (CurrentPageNumber == LastPage)
            {
                btnPlusPage.IsEnabled = false;
                btnMinusPage.IsEnabled = true;
            }
            else if (CurrentPageNumber > LastPage)
            {
                CurrentPageNumber -= 1;
                tbPageNumber.Text = CurrentPageNumber.ToString();
                LoadWordsToCurrentPage(true);
            }
            else
            {
                btnPlusPage.IsEnabled = true;
                btnMinusPage.IsEnabled = true;
            }
        }
    }

    private void LoadWordsToCurrentPage(bool reloadButtons)
    {
        UpdateWordsList();
        
        vocabularyDataGrid.ItemsSource = WordsList;
        vocabularyDataGrid.Items.Refresh();

        if (reloadButtons) LoadPageButtons();
    }

    private void LoadWordsToTranslate()
    {
        _app.BackgroundProcess
            .ProcessWordsForRevising(DbMethods.GetWordsForRevising(10, 1), false);
        
        translateDataGrid.ItemsSource = _app.BackgroundProcess.WordsToTranslate;
        translateDataGrid.Items.Refresh();
    }
    
    protected override void OnClosed(EventArgs e)
    {
        _app.BackgroundIcon.SetWindow(null);

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

    private void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;

        DbContext.Words
            .Remove(DbContext.Words
                .Single(word => word.Phrase == button!.Tag.ToString()));
        
        DbContext.SaveChanges();
        LoadWordsToCurrentPage(true);
        LoadWordsToTranslate();
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        AddNewWordWindow w = new ("Add new word", DbContext);
        w.ShowDialog();

        if (IsMovingOnNextPage)
        {
            CurrentPageNumber += 1;
            tbPageNumber.Text = CurrentPageNumber.ToString();
        }
        
        LoadPageButtons();
        LoadWordsToCurrentPage(true);
    }

    private void BtnTranslate_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;

        var window = new TranslateWordWindow(button?.Tag.ToString() ?? "");
        window.ShowDialog();
        
        LoadWordsToTranslate();
    }

    private void BtnMinusPage_OnClick(object sender, RoutedEventArgs e)
    {
        CurrentPageNumber -= 1;
        
        LoadWordsToCurrentPage(true);
        
        tbPageNumber.Text = CurrentPageNumber.ToString();
    }

    private void BtnPlusPage_OnClick(object sender, RoutedEventArgs e)
    {
        CurrentPageNumber += 1;
        
        LoadWordsToCurrentPage(true);
        
        tbPageNumber.Text = CurrentPageNumber.ToString();
    }
}