using System.Collections.ObjectModel;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.RepetitionAlgorithm
{
    public class WordPopUpBackgroundProcess(App app)
    {
        private readonly CancellationTokenSource _cancellationToken = new();

        public void Start()
        {
            var token = _cancellationToken.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested) 
                {
                    Helper.DbMethods.UpdateTimeInPriorities();
                    
                    ProcessWordsForRevising(Helper.DbMethods.GetWordsForRevising(10, 1));
                    
                    await Task.Delay(TimeSpan.FromMinutes(5), token);
                }
            }, token);

        }

        private void ProcessWordsForRevising(List<Words> wordsList)
        {
            if (wordsList.Count == 0) return;
            
            app.Dispatcher.Invoke(() =>
            {
                app.BackgroundIcon.ShowNotification(3000);

                var currentMainWindow = app.CurrentMainWindow;
        
                if (currentMainWindow is not null)
                {
                    currentMainWindow.translateDataGrid.ItemsSource = new ObservableCollection<Words>(wordsList);
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
