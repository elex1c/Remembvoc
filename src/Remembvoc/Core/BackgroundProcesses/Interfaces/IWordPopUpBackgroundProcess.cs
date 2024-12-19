using Remembvoc.Core.Common.Models;

namespace Remembvoc.Core.BackgroundProcesses.Interfaces;

public interface IWordPopUpBackgroundProcess
{
    public void Start();
    public void ProcessWordsForRevising(List<Word> wordsList, bool notification);
    public void Stop();
}