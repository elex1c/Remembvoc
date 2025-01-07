using System.Windows;
using Remembvoc.ApplicationCore.Common.Interfaces;

namespace Remembvoc.UI.Models;

public class WpfDispatcher : IDispatcher
{
    public void Invoke(Action action)
    {
        Application.Current.Dispatcher.Invoke(action); 
    }
}