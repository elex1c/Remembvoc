using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.ApplicationCore.Common.Models.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IDispatcher _dispatcher;
    
    private ObservableCollection<Word> _vocabularyWords = new();
    public ObservableCollection<Word> VocabularyWords
    {
        get => _vocabularyWords;
        set
        {
            _vocabularyWords = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<object> _phrasesForRevising = new();
    public ObservableCollection<object> PhrasesForRevising
    {
        get => _phrasesForRevising;
        set
        {
            _phrasesForRevising = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    public void OnPagesUpdated(WordsListUpdatedEvent e)
    {
        _dispatcher.Invoke(() =>
        {
            VocabularyWords.Clear();
            PhrasesForRevising.Clear();
            
            VocabularyWords = new ObservableCollection<Word>(e.CurrentPageWordList);
            PhrasesForRevising = new ObservableCollection<object>(e.CurrentPageWordsForRevising.Select(word => new { word.Phrase }));
        });
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}