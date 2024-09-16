using System.Windows;

namespace Remembvoc;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public readonly NotifyIconBackground BackgroundIcon = new ();
    
    protected override void OnExit(ExitEventArgs e)
    {
        BackgroundIcon.Dispose();
        
        base.OnExit(e);
    }
}