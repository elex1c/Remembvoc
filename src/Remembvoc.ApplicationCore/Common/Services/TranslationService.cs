using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Validation;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Services;

public class TranslationService : ITranslationService<WordTranslationResponse>
{
    private readonly ISentenceGenService _genService;
    private readonly IWordService _wordService;
    private readonly IPriorityService _priorityService;

    private Word? Word { get; set; }
    
    public TranslationService(ISentenceGenService genService, IWordService wordService, IPriorityService priorityService)
    {
        _genService = genService;
        _wordService = wordService;
        _priorityService = priorityService;
    }

    public async Task<WordTranslationResponse> GenerateSentenceAsync(string word)
    {
        var wordObj = await _wordService.GetWordByNameAsync(word);

        if (wordObj is null)
            return new WordTranslationResponse
            {
                IsSuccessRequest = false,
                ErrorMessage = Errors.WORD_NOT_FOUND
            };
        
        string? generatedSentence = await _genService.GenerateAsync(wordObj.Phrase, wordObj.Language.Name.ToString());

        if (string.IsNullOrWhiteSpace(generatedSentence))
            return new WordTranslationResponse
            {
                IsSuccessRequest = false,
                ErrorMessage = Errors.GENERATION_FAILED
            };
        
        Word = wordObj;
        
        return new WordTranslationResponse
        {
            IsSuccessRequest = true,
            Sentence = generatedSentence
        };
    }

    public async Task<CheckTranslationResponse> CheckTranslationAsync(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
            return new CheckTranslationResponse
            {
                State = TranslationStates.IncorrectInput,
                Message = Errors.INCORRECT_INPUT
            };

        if (Word!.Phrase.ToLower() == userInput.Trim().ToLower())
        {
            await _priorityService.UpdateSinglePriorityByIdAsync(Word!.Id, true);
            
            return new CheckTranslationResponse
            {
                State = TranslationStates.Translated,
                Message = Success.TRANSLATED_SUCCESSFULLY
            };
        }
        
        await _priorityService.UpdateSinglePriorityByIdAsync(Word!.Id, false);
        
        return new CheckTranslationResponse
        {
            State = TranslationStates.NotTranslated,
            Message = Errors.INCORRECT_TRANSLATION
        };
    }
}