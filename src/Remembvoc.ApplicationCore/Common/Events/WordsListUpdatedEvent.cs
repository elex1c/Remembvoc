using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.ApplicationCore.Common.Events;

public class WordsListUpdatedEvent : EventArgs
{
    public List<Word> CurrentPageWordList { get; set; }
    public List<Word> CurrentPageWordsForRevising { get; set; }
    public int VocabularyWordListTotalCount { get; set; }
    public int TranslationWordListTotalCount { get; set; }

    public WordsListUpdatedEvent(List<Word> currentPageWordList,
        List<Word> currentPageWordsForRevising,
        int vocabularyWordListTotalCount,
        int translationWordListTotalCount)
    {
        CurrentPageWordList = currentPageWordList;
        CurrentPageWordsForRevising = currentPageWordsForRevising;
        VocabularyWordListTotalCount = vocabularyWordListTotalCount;
        TranslationWordListTotalCount = translationWordListTotalCount;
    }
}