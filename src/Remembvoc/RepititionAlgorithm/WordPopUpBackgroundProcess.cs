﻿namespace Remembvoc.RepititionAlgorithm
{
    public class WordPopUpBackgroundProcess
    {
        private CancellationTokenSource _cancelationToken;

        public void Start()
        {
            _cancelationToken = new CancellationTokenSource();
            var token = _cancelationToken.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested) 
                {
                    // 

                    await Task.Delay(TimeSpan.FromMinutes(5), token);
                }
            }, token);

        }

        public void Stop() 
        {
            _cancelationToken.Cancel();
        }
    }

}