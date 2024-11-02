using System.Collections.ObjectModel;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.RepetitionAlgorithm
{
    public class WordPopUpBackgroundProcess(App app)
    {
        private readonly CancellationTokenSource _cancellationToken = new();

        public ObservableCollection<Words> WordsToTranslate { get; set; } = new();
        
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
        
        public void ProcessWordsForRevising(List<Words> wordsList, bool notification)
        {
            WordsToTranslate = new ObservableCollection<Words>(wordsList);
            
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

}
