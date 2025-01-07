namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IDispatcher
{
    void Invoke(Action action);
}