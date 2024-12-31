namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface INotificationIcon
{
    public void ShowNotification(int interval);
    public void SetWindow(object? window);
}