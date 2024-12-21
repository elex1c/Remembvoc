using Remembvoc.ApplicationCore.Common.Interfaces;

namespace Remembvoc.ApplicationCore.Common.BackgroundServices;

public class WordRepeatingBackgroundService : IBackgroundService
{
    private readonly CancellationTokenSource _cancellationToken = new();
    
    public void Start(IBackgroundServiceParameters parameters)
    {
        var token = _cancellationToken.Token;

        Task.Run(async () =>
        {
            // TODO: Complete the service
        }, token);
    }

    public void Stop()
    {
        _cancellationToken.Cancel();
    }
}