using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models;

namespace Remembvoc.ApplicationCore.Common.Services;

public class PaginationService : IPaginationService
{
    private readonly IWordService _wordService;

    public Page MainPage { get; set; }
    public Page TranslationPage { get; set; }
    public Page CurrentPage { get; set; }

    public PaginationService(IWordService wordService)
    {
        _wordService = wordService;

        DefaultConfigure();
        
        wordService.WordListUpdated += OnWordListUpdated;
    }

    private void DefaultConfigure()
    {
        CurrentPage = MainPage;
    }
    
    public Page SwitchPage(Pages pages)
    {
        CurrentPage = pages switch
        {
            Pages.Vocabulary => MainPage,
            Pages.Translate => TranslationPage
        };
        
        return CurrentPage;
    }

    public Page NextPage()
    {
        CurrentPage.CurrentPageNumber += 1;
        LoadPageButtons();
        return CurrentPage;
    }

    public Page PreviousPage()
    {
        CurrentPage.CurrentPageNumber -= 1;
        LoadPageButtons();
        return CurrentPage;
    }

    public void LoadPageButtons()
    {
        if (CurrentPage.TotalWordsAmount <= CurrentPage.ElementsPerPage)
        {
            CurrentPage.IsVisible = false;
            CurrentPage.CurrentPageNumber = 1;
        }
        else
        {
            if (CurrentPage.CurrentPageNumber == 1)
            {
                CurrentPage.IsPlusPageButtonEnabled = true;
                CurrentPage.IsMinusPageButtonEnabled = false;
            }
            else if (CurrentPage.CurrentPageNumber == CurrentPage.LastPage)
            {
                CurrentPage.IsPlusPageButtonEnabled = false;
                CurrentPage.IsMinusPageButtonEnabled = true;
            }
            else if (CurrentPage.CurrentPageNumber > CurrentPage.LastPage)
            {
                CurrentPage.CurrentPageNumber -= 1;
            }
            else
            {
                CurrentPage.IsPlusPageButtonEnabled = true;
                CurrentPage.IsMinusPageButtonEnabled = true;
            }
        }
    }

    private async void OnWordListUpdated(object? sender, WordsListUpdatedEvent e)
    {
        MainPage.TotalWordsAmount = await _wordService.CountWordsForWordList();
        TranslationPage.TotalWordsAmount = await _wordService.CountWordsForRevisingAsync();
    }
}