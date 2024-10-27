using System.Windows;
using Remembvoc.RepetitionAlgorithm;

namespace Remembvoc;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public readonly NotifyIconBackground BackgroundIcon = new ();
    public readonly DatabaseContext DatabaseContext = new ();
    
    private readonly WordPopUpBackgroundProcess backgroundProcess;

    public MainWindow? CurrentMainWindow => (MainWindow?)Current.MainWindow; 
    
    public App()
    {
        backgroundProcess = new (this);
        backgroundProcess.Start();
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        BackgroundIcon.Dispose();
        DatabaseContext.Dispose();
        backgroundProcess.Stop();
        
        base.OnExit(e);
    }
}