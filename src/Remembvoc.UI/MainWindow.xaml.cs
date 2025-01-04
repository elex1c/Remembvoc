using System.Windows;
using System.Windows.Input;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.ViewModels;
using Remembvoc.UI.AdditionalUI.AdditionalWindows;
using Page = Remembvoc.ApplicationCore.Common.Models.Page;

namespace Remembvoc.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Func<IWordService> _wordService;
    private readonly IPaginationService _paginationService;
    private readonly AddNewWordWindow _addNewWordWindow;

    public MainWindow(Func<IWordService> wordService,
        IPaginationService paginationService,
        AddNewWordWindow addNewWordWindow,
        MainViewModel mainViewModel)
    {
        _wordService = wordService;
        _paginationService = paginationService;
        _addNewWordWindow = addNewWordWindow;

        DataContext = mainViewModel;
        
        InitializeComponent();

        Loaded += StartUpConfigure;
    }
    
    private async void StartUpConfigure(object sender, RoutedEventArgs e)
    {
        var wordService = _wordService();
        
        await wordService.GetAndSendUpdatedDataAsync();
    }
    
    private void MWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private async void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var wordService = _wordService();
        var button = sender as System.Windows.Controls.Button;
        
        await wordService.DeleteWordAsync(button!.Tag.ToString()!);
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        _addNewWordWindow.ShowDialog();
    }

    private void BtnTranslate_OnClick(object sender, RoutedEventArgs e)
    {
       
    }

    private void BtnPlusPage_OnClick(object sender, RoutedEventArgs e)
    {
        var page = _paginationService.NextPage();
        UpdateButtonsGrid(page);
    }

    private void BtnMinusPage_OnClick(object sender, RoutedEventArgs e)
    {
        var page = _paginationService.PreviousPage();
        UpdateButtonsGrid(page);
    }

    private void TabItemVocabulary_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        => SwitchPage(Pages.Vocabulary);

    private void TabItemTranslate_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) 
        => SwitchPage(Pages.Translate);

    private void SwitchPage(Pages page)
    {
        var currentPage = _paginationService.SwitchPage(page);
        UpdateButtonsGrid(currentPage);
    }
    
    private void UpdateButtonsGrid(Page page)
    {
        gridPageButtons.Visibility = page.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        btnMinusPage.IsEnabled = page.IsMinusPageButtonEnabled;
        btnPlusPage.IsEnabled = page.IsPlusPageButtonEnabled;
        tbPageNumber.Text = page.CurrentPageNumber.ToString();
    }
}