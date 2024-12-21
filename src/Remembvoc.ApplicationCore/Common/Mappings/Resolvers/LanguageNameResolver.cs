using AutoMapper;
using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Models.DTOs;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Mappings.Resolvers;

public class LanguageNameResolver : IValueResolver<LanguageEntity, Language, Languages>
{
    public Languages Resolve(LanguageEntity source, Language destination, Languages destMember, ResolutionContext context)
    {
        // Default return: English
        return Enum.TryParse<Languages>(source.Name, out var language) ? language : Languages.English;
    }
}