using System.Windows;

namespace Remembvoc;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public readonly NotifyIconBackground BackgroundIcon = new ();
    public readonly DatabaseContext DatabaseContext = new ();
    
    protected override void OnExit(ExitEventArgs e)
    {
        BackgroundIcon.Dispose();
        DatabaseContext.Dispose();
        
        base.OnExit(e);
    }
}