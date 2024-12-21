namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IBackgroundService
{
    public void Start(IBackgroundServiceParameters parameters);
    public void Stop();
}