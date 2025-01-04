using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Validation.Models;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Validation.UserInputValidation.Validatiors;

public class WordValidator : IWordValidator
{
    public async Task<WordValidationResponse> Validate(WordInputModel model)
    {
        model.Phrase = model.Phrase
            .Trim()
            .ToLower();
        
        model.Translation = model.Translation
            .Trim()
            .ToLower();
        
        if (string.IsNullOrEmpty(model.Translation) 
            || string.IsNullOrEmpty(model.Language) 
            || string.IsNullOrEmpty(model.Phrase))
        {
            return new WordValidationResponse { IsValid = false, ErrorMessage = Errors.EMPTY_BOXES };
        }
        
        bool isLanguageParsed = Enum.TryParse(model.Language, true, out Languages language);
        
        if (!isLanguageParsed)
        {
            return new WordValidationResponse { IsValid = false, ErrorMessage = Errors.LANGUAGE_NOT_FOUND };
        }
        
        return new WordValidationResponse
        {
            IsValid = true,
            Word = new Word
            {
                Phrase = model.Phrase,
                Translation = model.Translation,
                Language = new Language { Name = language }
            }
        };
    }
}