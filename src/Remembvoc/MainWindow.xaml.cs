using System.Collections.ObjectModel;
using System.Transactions;
using System.Windows;
using Microsoft.EntityFrameworkCore;
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

    private int CurrentPageNumber { get; set; } = 1;
    private const int ElementsPerPage = 5;

    private int TotalWordsAmount => DbContext.Words.Count();
    private int LastPage => (int)Math.Ceiling(TotalWordsAmount / (double)ElementsPerPage);
    
    public MainWindow()
    {
        InitializeComponent();

        ((App)Application.Current).BackgroundIcon.SetWindow(this);

        DbContext = ((App)Application.Current).DatabaseContext;
        
        #region TempData

        translateDataGrid.ItemsSource = new ObservableCollection<Translation>([new Translation { Word = "Hello"}, new Translation { Word = "Goodbye"} ]);
        
        #endregion

        BasicStartMethods();
    }

    private void BasicStartMethods()
    {
        // Updates time since last check to word revising
        DbMethods.UpdateTimeInPriorities(DbContext);
        // Loads words from vocabulary
        LoadWordsToCurrentPage();
    }

    private void LoadPageButtons()
    {
        if (TotalWordsAmount <= ElementsPerPage)
        {
            gridPageButtons.Visibility = Visibility.Collapsed;
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
                LoadWordsToCurrentPage();
            }
            else
            {
                btnPlusPage.IsEnabled = true;
                btnMinusPage.IsEnabled = true;
            }
        }
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

        vocabularyDataGrid.ItemsSource = new ObservableCollection<Word>(list);
        vocabularyDataGrid.Items.Refresh();

        LoadPageButtons();
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

    private void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;

        DbContext.Words
            .Remove(DbContext.Words
                .Single(word => word.Phrase == button!.Tag.ToString()));
        
        DbContext.SaveChanges();
        LoadWordsToCurrentPage();
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        AddNewWordWindow w = new ("Add new word", DbContext);
        w.ShowDialog();
        
        LoadWordsToCurrentPage();
    }

    private void BtnMinusPage_OnClick(object sender, RoutedEventArgs e)
    {
        CurrentPageNumber -= 1;
        
        LoadWordsToCurrentPage();
        
        tbPageNumber.Text = CurrentPageNumber.ToString();
    }

    private void BtnPlusPage_OnClick(object sender, RoutedEventArgs e)
    {
        CurrentPageNumber += 1;
        
        LoadWordsToCurrentPage();
        
        tbPageNumber.Text = CurrentPageNumber.ToString();
    }
}