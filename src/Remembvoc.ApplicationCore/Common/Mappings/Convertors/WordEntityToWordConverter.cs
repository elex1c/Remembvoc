using AutoMapper;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Mappings.Convertors;

public class WordEntityToWordConverter : ITypeConverter<WordEntity, Word>, AutoMapper.ITypeConverter<WordEntity, Word>
{
    public Word Convert(WordEntity source, Word destination, ResolutionContext context)
    { 
        if (source is null) return null;
        
        destination ??= new Word();

        destination.Id = source.Id;
        destination.Phrase = source.Phrase;
        destination.Translation = source.Translation;
        destination.Language = new Language { Name = (Languages)source.LanguageId };
        
        return destination;
    }
}