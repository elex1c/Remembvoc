using Remembvoc.ApplicationCore.Common.Interfaces;

namespace Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

public class WordTranslationResponse : ITranslationResponse
{
    public bool IsSuccessRequest { get; set; }
    public string Sentence { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}