using Remembvoc.Core.Common.Models;
using Remembvoc.Models;

namespace Remembvoc.Core.Common.Interfaces;

public interface IWordService
{
    public void AddWord(Word word);
    public void DeleteWord();
    public Word? GetWord(string word);
    public List<Word> GetWordsForRevising(int elementsPerPage, int pageNumber);
}