using AutoMapper;
using Remembvoc.ApplicationCore.Common.BackgroundServices;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;
using Remembvoc.ApplicationCore.Common.Models.ViewModels;
using Remembvoc.ApplicationCore.Common.Validation;
using Remembvoc.ApplicationCore.Common.Validation.Models;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWordValidator _wordValidator;
    private readonly PagesData _pagesData;
    private readonly MainViewModel _mainViewModel;

    public WordService(IWordRepository repository,
        IMapper mapper,
        IWordValidator wordValidator,
        PagesData pagesData,
        MainViewModel mainViewModel)
    {
        _repository = repository;
        _mapper = mapper;
        _wordValidator = wordValidator;
        _pagesData = pagesData;
        _mainViewModel = mainViewModel;
    }

    public async Task<WordValidationResponse> AddWordAsync(string word,
        string language,
        string translation)
    {
        var validationResponse = await _wordValidator.Validate(
            new WordInputModel
            {
                Phrase = word,
                Language = language,
                Translation = translation
            });
        bool isWordInDictionary = await GetWordByNameAsync(word) != null;
        
        if (!validationResponse.IsValid) return validationResponse;
        if (isWordInDictionary) return new WordValidationResponse { IsValid = false, ErrorMessage = Errors.WORD_EXISTS };
        
        var wordEntity = _mapper.Map<WordEntity>(validationResponse.Word);
        wordEntity = await _repository.AddWordAsync(wordEntity);
        
        validationResponse.Word!.Id = wordEntity.Id;
        
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
        var args = new WordsListUpdatedEvent(
            await GetWordsForWordListAsync(_pagesData.MainPage.ElementsPerPage, _pagesData.MainPage.CurrentPageNumber),
            await GetWordsForRevisingAsync(_pagesData.TranslationPage.ElementsPerPage,
                _pagesData.TranslationPage.CurrentPageNumber),
            await CountWordsForWordList(),
            await CountWordsForRevisingAsync());

        _pagesData.MainPage.TotalWordsAmount = args.VocabularyWordListTotalCount;
        _pagesData.TranslationPage.TotalWordsAmount = args.TranslationWordListTotalCount;
        
        _mainViewModel.OnPagesUpdated(args);
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
}