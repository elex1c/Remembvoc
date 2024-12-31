using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

public class WordValidationResponse : IValidationResponse
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public Word? Word { get; set; }
}