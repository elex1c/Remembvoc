namespace Remembvoc.Models
{
    public class SentenceToTranslate
    {
        public Models.Word ChosenWord { get; set; }
        public string? GeneratedSentence { get; set; }
        public List<string>? AnswerChoices { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
