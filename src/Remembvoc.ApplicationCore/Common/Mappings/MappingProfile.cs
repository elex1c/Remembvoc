using AutoMapper;
using Remembvoc.ApplicationCore.Common.Mappings.Convertors;
using Remembvoc.ApplicationCore.Common.Mappings.Resolvers;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LanguageEntity, Language>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom<LanguageNameResolver>());
        
        CreateMap<WordEntity, Word>()
            .ConvertUsing<WordEntityToWordConverter>();
        
        CreateMap<Word, WordEntity>()
            .ConvertUsing<WordToWordEntityConverter>();
        
        CreateMap<PriorityEntity, Priority>();
        
        CreateMap<Priority, PriorityEntity>();
    }
}