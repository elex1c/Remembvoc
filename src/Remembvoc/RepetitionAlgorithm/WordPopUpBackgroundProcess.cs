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
            
            app.BackgroundIcon.ShowNotification(3000);

            var currentMainWindow = app.CurrentMainWindow;
            
            if (currentMainWindow is not null)
            {
                // TODO: Process all words for revising in data grid
            }
        }
        
        public void Stop() 
        {
            _cancellationToken.Cancel();
        }
    }

}
