using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Remembvoc.ApplicationCore.Common.BackgroundServices;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Mappings;
using Remembvoc.ApplicationCore.Common.Services;
using Remembvoc.ApplicationCore.Common.Validation.UserInputValidation.Validatiors;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;
using Remembvoc.UI.RepetitionAlgorithm;
using Remembvoc.UI.SentencesLibraries;
using ISentenceGen = Remembvoc.UI.SentencesLibraries.ISentenceGen;

namespace Remembvoc.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public MainWindow? CurrentMainWindow => (MainWindow?)Current.MainWindow; 
    
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureService)
            .Build();
    }

    private void ConfigureService(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<INotificationIcon, NotifyIconBackground>();
        services.AddSingleton<IPaginationService, PaginationService>();
        services.AddSingleton<IBackgroundService<BackgroundServiceParameters>, WordRepeatingBackgroundService>();
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IPriorityService, PriorityService>();
        services.AddScoped<ITranslationService<WordTranslationResponse>, TranslationService>();
        services.AddScoped<IWordValidator, WordValidator>();
        services.AddScoped<ISentenceGenService, SentenceGenService>();
        services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
        services.AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();
        
        var bgService = AppHost.Services.GetRequiredService<IBackgroundService<BackgroundServiceParameters>>();
        bgService.Start(null);
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        var bgService = AppHost!.Services.GetRequiredService<IBackgroundService<BackgroundServiceParameters>>();
        bgService.Stop();
        
        await AppHost.StopAsync();
        
        base.OnExit(e);
    }
}