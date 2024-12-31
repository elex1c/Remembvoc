using AutoMapper;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;
using Remembvoc.ApplicationCore.Common.Validation.Models;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPaginationService _pageService;
    private readonly IWordValidator _wordValidator;

    public event EventHandler<WordsListUpdatedEvent> WordListUpdated;

    public WordService(IWordRepository repository,
        IMapper mapper,
        IPaginationService pageService,
        IWordValidator wordValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _pageService = pageService;
        _wordValidator = wordValidator;
    }

    public async Task<WordValidationResponse> AddWordAsync(string word, string language, string translation)
    {
        var validationResponse = await _wordValidator.Validate(
            new WordInputModel
            {
                Phrase = word,
                Language = language,
                Translation = translation
            });
        
        if (!validationResponse.IsValid) return validationResponse;

        var wordEntity = _mapper.Map<WordEntity>(validationResponse.Word);
        await _repository.AddWordAsync(wordEntity);

        await GetAndSendUpdatedDataAsync();
        
        return validationResponse;
    }

    public async Task DeleteWordAsync(string word)
    {
        var wordEntity = await _repository.GetWordByNameAsync(word);
        if (wordEntity is null) return;
        await _repository.DeleteWordAsync(wordEntity);
        
        await GetAndSendUpdatedDataAsync();
    }

    public async Task<List<Word>> GetAllAsync()
    {
        var wordEntities = await _repository.GetAllAsync();
        return wordEntities.Select(w => _mapper.Map<Word>(w))
            .ToList();
    }

    public async Task<Word?> GetWordByNameAsync(string word)
    {
        var wordEntity = await _repository.GetWordByNameAsync(word);
        return wordEntity is null ? null : _mapper.Map<Word>(wordEntity);
    }

    private async Task<List<Word>> GetWordsForRevisingAsync(int elementsPerPage, int pageNumber)
    {
        var wordEntities = await _repository.GetAllWithPrioritiesAsync();
        return wordEntities.Where(w => w.Priority.MinutesToRepeat <= 0)
            .OrderBy(w => w.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .Select(w => _mapper.Map<Word>(w))
            .ToList();
    }

    private async Task<List<Word>> GetWordsForWordListAsync(int elementsPerPage, int pageNumber)
    {
        var wordEntities= await _repository.GetAllWithPrioritiesAsync();
        return wordEntities.Where(w => w.Priority.MinutesToRepeat > 0)
            .OrderBy(w => w.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .Select(_mapper.Map<Word>)
            .ToList();
    }

    public async Task GetAndSendUpdatedDataAsync()
    {
        int currentMainPage = _pageService.MainPage.CurrentPageNumber;
        int elementsPerMainPAge = _pageService.MainPage.ElementsPerPage;
        int currentTranslationPage = _pageService.TranslationPage.CurrentPageNumber;
        int elementsPerTranslationPage = _pageService.TranslationPage.ElementsPerPage;
        
        OnWordListUpdated(new WordsListUpdatedEvent(
            await GetWordsForWordListAsync(elementsPerMainPAge, currentMainPage), 
            await GetWordsForRevisingAsync(elementsPerTranslationPage, currentTranslationPage)));
    }
    
    public async Task<int> CountWordsForRevisingAsync()
    {
        var words = await _repository.GetAllWithPrioritiesAsync();
        return words.Count(word => word.Priority.MinutesToRepeat <= 0);
    }

    public async Task<int> CountWordsForWordList()
    {
        int wordsForRevising = await CountWordsForRevisingAsync();
        var words = await _repository.GetAllAsync();
        return words.Count - wordsForRevising;
    }

    protected virtual void OnWordListUpdated(WordsListUpdatedEvent e)
    {
        WordListUpdated?.Invoke(this, e);
    }
}