using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.ViewModels;
using Remembvoc.ApplicationCore.Common.Services;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;
using Remembvoc.UI.AdditionalUI.AdditionalWindows;
using Page = Remembvoc.ApplicationCore.Common.Models.Page;

namespace Remembvoc.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public TranslateWordWindow TranslateWordWindow { get; }
    private readonly Func<IWordService> _wordService;
    private readonly IPaginationService _paginationService;
    private readonly AddNewWordWindow _addNewWordWindow;
    private readonly TranslateWordWindow _translateWordWindow;

    public MainWindow(Func<IWordService> wordService,
        IPaginationService paginationService,
        AddNewWordWindow addNewWordWindow,
        MainViewModel mainViewModel,
        TranslateWordWindow translateWordWindow)
    {
        TranslateWordWindow = translateWordWindow;
        _wordService = wordService;
        _paginationService = paginationService;
        _addNewWordWindow = addNewWordWindow;
        _translateWordWindow = translateWordWindow;

        DataContext = mainViewModel;
        
        InitializeComponent();

        Loaded += StartUpConfigure;
    }
    
    private void StartUpConfigure(object sender, RoutedEventArgs e)
    {
        UpdateButtonsGrid(_paginationService.GetCurrentPage());
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
        var button = sender as Button;
        
        await wordService.DeleteWordAsync(button!.Tag.ToString()!);
        UpdateButtonsGrid(_paginationService.GetCurrentPage());
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        _addNewWordWindow.ShowDialog();
        UpdateButtonsGrid(_paginationService.GetCurrentPage());
    }

    private void BtnTranslate_OnClick(object sender, RoutedEventArgs e)
    {
        _translateWordWindow.AddPhrase(((Button)sender).Tag.ToString()!);
        _translateWordWindow.LoadNewWindow();
        _translateWordWindow.ShowDialog();
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

    private void SwitchPage(Pages page)
    {
        if (_paginationService.CurrentPageType == page) return;
        
        var currentPage = _paginationService.SwitchPage(page);
        UpdateButtonsGrid(currentPage);
    }
    
    private async void UpdateButtonsGrid(Page page)
    {
        if (page.CurrentPageNumber > page.LastPage) _paginationService.LoadPageButtons();
        
        var wordService = _wordService();
        
        await wordService.GetAndSendUpdatedDataAsync();
        
        _paginationService.LoadPageButtons();
        
        gridPageButtons.Visibility = page.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        btnMinusPage.IsEnabled = page.IsMinusPageButtonEnabled;
        btnPlusPage.IsEnabled = page.IsPlusPageButtonEnabled;
        tbPageNumber.Text = page.CurrentPageNumber.ToString();
    }

    private void MainTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (mainTabControl.SelectedIndex)
        {
            case 0:
                SwitchPage(Pages.Vocabulary);
                break;
            case 2:
                SwitchPage(Pages.Translate);
                break;
        }
    }
}