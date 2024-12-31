using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IValidator<in TModel, TResponse>
    where TResponse : IValidationResponse
{
    public Task<TResponse> Validate(TModel model);
}