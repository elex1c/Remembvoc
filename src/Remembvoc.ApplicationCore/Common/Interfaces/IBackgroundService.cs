using Remembvoc.ApplicationCore.Common.Events;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IBackgroundService<in TParameters> 
    where TParameters : IBackgroundServiceParameters
{
    void Start(TParameters? parameters);
    void Stop();
}