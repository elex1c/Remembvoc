using Remembvoc.ApplicationCore.Common.Validation.Models;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IWordValidator : IValidator<WordInputModel, WordValidationResponse> { }