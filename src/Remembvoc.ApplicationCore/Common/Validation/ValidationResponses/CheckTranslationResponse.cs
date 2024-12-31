using Remembvoc.ApplicationCore.Common.Enums;

namespace Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

public class CheckTranslationResponse
{
    public string Message { get; set; }
    public TranslationStates State { get; set; }
}