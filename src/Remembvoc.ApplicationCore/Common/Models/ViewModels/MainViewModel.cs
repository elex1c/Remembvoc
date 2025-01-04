using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.ApplicationCore.Common.Models.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Word> VocabularyWords { get; set; } = new();
    public ObservableCollection<string> PhrasesForRevising { get; set; } = new();
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPagesUpdated(WordsListUpdatedEvent e)
    {
        VocabularyWords.Clear();
        PhrasesForRevising.Clear();

        foreach (var word in e.CurrentPageWordList) VocabularyWords.Add(word);
        foreach (var word in e.CurrentPageWordsForRevising) PhrasesForRevising.Add(word.Phrase);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}