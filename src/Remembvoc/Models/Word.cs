using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.Models;

public class Word
{
    public string Phrase { get; set; }
    public Languages Language { get; set; }

    public static explicit operator Word(Words word)
    {
        Enum.TryParse(word.Language.ShortForm, out Languages language);
        return new Word { Phrase = word.Phrase, Language = language };
    }
}