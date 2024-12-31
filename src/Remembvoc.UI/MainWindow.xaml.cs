using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.UI.ViewModels;
using Page = Remembvoc.ApplicationCore.Common.Models.Page;

namespace Remembvoc.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly INotificationIcon _notificationIcon;
    private readonly IWordService _wordService;
    private readonly IPaginationService _paginationService;

    public MainWindow(INotificationIcon notificationIcon, IWordService wordService, IPaginationService paginationService)
    {
        _notificationIcon = notificationIcon;
        _wordService = wordService;
        _paginationService = paginationService;

        DataContext = new MainViewModel(_wordService);
        
        InitializeComponent();

        Loaded += StartUpConfigure;
    }

    private async void StartUpConfigure(object sender, RoutedEventArgs e)
    {
        await _wordService.GetAndSendUpdatedDataAsync();
    }

    protected override void OnClosed(EventArgs e)
    {
        _notificationIcon.SetWindow(null);

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

    private async void BtnDelWord_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as System.Windows.Controls.Button;
        
        await _wordService.DeleteWordAsync(button!.Tag.ToString()!);
    }

    private void BtnAddNewWord_OnClick(object sender, RoutedEventArgs e)
    {
        
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