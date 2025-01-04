using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models;
using Remembvoc.ApplicationCore.Common.Models.ViewModels;

namespace Remembvoc.ApplicationCore.Common.BackgroundServices;

public class WordRepeatingBackgroundService : IBackgroundService<BackgroundServiceParameters>
{
    private readonly Func<IWordService> _wordService;
    private readonly Func<IPriorityService> _priorityService;
    private readonly INotificationIcon _notificationIcon;
    private readonly PagesData _pagesData;

    private readonly CancellationTokenSource _cancellationToken = new();
    
    public WordRepeatingBackgroundService(Func<IWordService> wordService,
        Func<IPriorityService> priorityService,
        INotificationIcon notificationIcon,
        PagesData pagesData)
    {
        _wordService = wordService;
        _priorityService = priorityService;
        _notificationIcon = notificationIcon;
        _pagesData = pagesData;
    }

    public void Start(BackgroundServiceParameters? parameters = null)
    {
        var token = _cancellationToken.Token;

        Task.Run(UpdateAndCheck, token);
    }

    private async void UpdateAndCheck()
    {
        var wordService = _wordService();
        var priorityService = _priorityService();
        
        await priorityService.UpdatePrioritiesAsync();

        if (_pagesData.TranslationPage.TotalWordsAmount != await wordService.CountWordsForRevisingAsync())
        {
            await wordService.GetAndSendUpdatedDataAsync();
            
            _notificationIcon.ShowNotification(3000);
        }
        
        await Task.Delay(TimeSpan.FromMinutes(5));
    }

    public void Stop()
    {
        _cancellationToken.Cancel();
    }
}