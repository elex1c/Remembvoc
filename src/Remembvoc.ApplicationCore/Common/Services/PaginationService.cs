using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models;

namespace Remembvoc.ApplicationCore.Common.Services;

public class PaginationService : IPaginationService
{
    private PagesData _pagesData { get; set; }

    public PaginationService(PagesData pagesData)
    {
        _pagesData = pagesData;
        
        DefaultConfigure();
    }

    private void DefaultConfigure()
    {
        _pagesData.CurrentPage = _pagesData.MainPage;
    }

    public Pages CurrentPageType { get; set; } = Pages.Vocabulary;

    public Page SwitchPage(Pages pages)
    {
        switch (pages)
        {
            case Pages.Vocabulary:
                _pagesData.CurrentPage = _pagesData.MainPage;
                break;
            case Pages.Translate:
                _pagesData.CurrentPage = _pagesData.TranslationPage;
                break;
        }
        
        CurrentPageType = pages;
        
        return _pagesData.CurrentPage;
    }

    public Page NextPage()
    {
        _pagesData.CurrentPage.CurrentPageNumber += 1;
        LoadPageButtons();
        return _pagesData.CurrentPage;
    }

    public Page PreviousPage()
    {
        _pagesData.CurrentPage.CurrentPageNumber -= 1;
        LoadPageButtons();
        return _pagesData.CurrentPage;
    }

    public void LoadPageButtons()
    {
        if (_pagesData.CurrentPage.TotalWordsAmount <= _pagesData.CurrentPage.ElementsPerPage)
        {
            _pagesData.CurrentPage.IsVisible = false;
            _pagesData.CurrentPage.CurrentPageNumber = 1;
        }
        else
        {
            if (_pagesData.CurrentPage.CurrentPageNumber == 1)
            {
                _pagesData.CurrentPage.IsPlusPageButtonEnabled = true;
                _pagesData.CurrentPage.IsMinusPageButtonEnabled = false;
                _pagesData.CurrentPage.IsVisible = true;
            }
            else if (_pagesData.CurrentPage.CurrentPageNumber == _pagesData.CurrentPage.LastPage)
            {
                _pagesData.CurrentPage.IsPlusPageButtonEnabled = false;
                _pagesData.CurrentPage.IsMinusPageButtonEnabled = true;
                _pagesData.CurrentPage.IsVisible = true;
            }
            else if (_pagesData.CurrentPage.CurrentPageNumber > _pagesData.CurrentPage.LastPage)
            {
                _pagesData.CurrentPage.CurrentPageNumber -= 1;
                LoadPageButtons();
            }
            else
            {
                _pagesData.CurrentPage.IsPlusPageButtonEnabled = true;
                _pagesData.CurrentPage.IsMinusPageButtonEnabled = true;
                _pagesData.CurrentPage.IsVisible = true;
            }
        }
    }

    public Page GetCurrentPage() => _pagesData.CurrentPage;
}