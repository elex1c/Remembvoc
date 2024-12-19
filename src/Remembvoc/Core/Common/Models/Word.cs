using Remembvoc.Core.Common.Enums;

namespace Remembvoc.Core.Common.Models;

public class Word
{
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public Languages Language { get; set; }

    public static explicit operator Word(WordEntity wordEntity)
    {
        Enum.TryParse(wordEntity.LanguageEntity.ShortForm, out Languages language);
        return new Word { Phrase = wordEntity.Phrase, Translation = wordEntity.Translation, Language = language };
    }
}