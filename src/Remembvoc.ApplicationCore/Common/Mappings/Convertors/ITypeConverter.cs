using AutoMapper;

namespace Remembvoc.ApplicationCore.Common.Mappings.Convertors;

public interface ITypeConverter<in TSource, TDestination>
{
    TDestination Convert(TSource source, TDestination destination, ResolutionContext context);
}