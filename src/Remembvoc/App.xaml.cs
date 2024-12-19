using System.Windows;
using Remembvoc.RepetitionAlgorithm;
using Remembvoc.SentencesLibraries;

namespace Remembvoc;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public readonly NotifyIconBackground BackgroundIcon = new ();
    public readonly DatabaseContext DatabaseContext = new ();
    public readonly WordPopUpBackgroundProcess BackgroundProcess;
    public readonly ISentenceGen SentenceGenerator = new LIamaGen();

    public MainWindow? CurrentMainWindow => (MainWindow?)Current.MainWindow; 
    
    public App()
    {
        BackgroundProcess = new (this);
        BackgroundProcess.Start();
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        BackgroundIcon.Dispose();
        DatabaseContext.Dispose();
        BackgroundProcess.Stop();
        
        base.OnExit(e);
    }
}