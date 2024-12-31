using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;

namespace Remembvoc.ApplicationCore.Common.BackgroundServices;

public class WordRepeatingBackgroundService : IBackgroundService<BackgroundServiceParameters>
{
    private readonly IWordService _wordService;
    private readonly IPriorityService _priorityService;
    private readonly IPaginationService _paginationService;
    private readonly INotificationIcon _notificationIcon;

    private readonly CancellationTokenSource _cancellationToken = new();

    public WordRepeatingBackgroundService(IWordService _wordService,
        IPriorityService priorityService,
        IPaginationService paginationService,
        INotificationIcon notificationIcon)
    {
        this._wordService = _wordService;
        _priorityService = priorityService;
        _paginationService = paginationService;
        _notificationIcon = notificationIcon;
    }

    public void Start(BackgroundServiceParameters? parameters = null)
    {
        var token = _cancellationToken.Token;

        Task.Run(UpdateAndCheck, token);
    }

    private async void UpdateAndCheck()
    {
        // Updates priorities (Updates the time to the next word revising)
        await _priorityService.UpdatePrioritiesAsync();

        if (_paginationService.TranslationPage.TotalWordsAmount != await _wordService.CountWordsForRevisingAsync())
        {
            await _wordService.GetAndSendUpdatedDataAsync();
            
            _notificationIcon.ShowNotification(3000);
        }
        
        await Task.Delay(TimeSpan.FromMinutes(5));
    }

    public void Stop()
    {
        _cancellationToken.Cancel();
    }
}