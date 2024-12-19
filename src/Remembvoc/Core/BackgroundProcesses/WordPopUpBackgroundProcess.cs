using System.Collections.ObjectModel;
using Remembvoc.Core.BackgroundProcesses.Interfaces;
using Remembvoc.Core.Common.Models;

namespace Remembvoc.Core.BackgroundProcesses;

    public class WordPopUpBackgroundProcess(App app) : IWordPopUpBackgroundProcess
    {
        private readonly CancellationTokenSource _cancellationToken = new();

        public ObservableCollection<WordEntity> WordsToTranslate { get; set; } = new();
        
        public void Start()
        {
            var token = _cancellationToken.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested) 
                {
                    Helper.DbMethods.UpdateTimeInPriorities();
                    
                    ProcessWordsForRevising(Helper.DbMethods.GetWordsForRevising(10, 1), true);
                    
                    await Task.Delay(TimeSpan.FromMinutes(5), token);
                }
            }, token);

        }
        
        public void ProcessWordsForRevising(List<WordEntity> wordsList, bool notification)
        {
            WordsToTranslate.Clear();
            foreach (var word in wordsList) WordsToTranslate.Add(word);
            
            if (WordsToTranslate.Count == 0) return;
            
            app.Dispatcher.Invoke(() =>
            {
                if (notification) app.BackgroundIcon.ShowNotification(3000);

                var currentMainWindow = app.CurrentMainWindow;
                
                if (currentMainWindow is not null)
                {
                    currentMainWindow.translateDataGrid.ItemsSource = WordsToTranslate;
                    currentMainWindow.translateDataGrid.Items.Refresh();
                }
            });
        }
        
        public void Stop() 
        {
            _cancellationToken.Cancel();
        }
    }


