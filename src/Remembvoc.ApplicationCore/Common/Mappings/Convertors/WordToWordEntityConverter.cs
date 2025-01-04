using AutoMapper;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Mappings.Convertors;

public class WordToWordEntityConverter : ITypeConverter<Word, WordEntity>, AutoMapper.ITypeConverter<Word, WordEntity>
{
    public WordEntity Convert(Word source, WordEntity destination, ResolutionContext context)
    {
        if (source is null) return null;
        
        destination ??= new WordEntity();

        destination.Id = source.Id;
        destination.Phrase = source.Phrase;
        destination.Translation = source.Translation;
        destination.LanguageId = (int)source.Language.Name;
        
        return destination;
    }
}