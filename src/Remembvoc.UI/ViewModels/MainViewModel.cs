using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.UI.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IWordService _wordService;
    
    public ObservableCollection<Word> VocabularyWords { get; set; } = new();
    public ObservableCollection<string> PhrasesForRevising { get; set; } = new();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public MainViewModel(IWordService wordService)
    {
        _wordService = wordService;
        _wordService.WordListUpdated += OnPagesUpdated;
    }

    private void OnPagesUpdated(object? sender, WordsListUpdatedEvent e)
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