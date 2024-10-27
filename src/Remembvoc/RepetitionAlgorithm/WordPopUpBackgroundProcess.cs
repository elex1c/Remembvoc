using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.RepetitionAlgorithm
{
    public class WordPopUpBackgroundProcess(DatabaseContext _context)
    {
        private readonly CancellationTokenSource _cancellationToken = new();
        
        public void Start()
        {
            var token = _cancellationToken.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested) 
                {
                    Helper.DbMethods.UpdateTimeInPriorities(_context);

                    ProcessWordsForRevising(Helper.DbMethods.GetWordsForRevising(10, 1, _context));
                    
                    await Task.Delay(TimeSpan.FromMinutes(5), token);
                }
            }, token);

        }

        private void ProcessWordsForRevising(List<Words> wordsList)
        {
            if (wordsList.Count == 0) return;
            
        }
        
        public void Stop() 
        {
            _cancellationToken.Cancel();
        }
    }

}
