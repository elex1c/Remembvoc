using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Remembvoc.ApplicationCore.Common.BackgroundServices;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Mappings;
using Remembvoc.ApplicationCore.Common.Models;
using Remembvoc.ApplicationCore.Common.Models.ViewModels;
using Remembvoc.ApplicationCore.Common.Services;
using Remembvoc.ApplicationCore.Common.Validation.UserInputValidation.Validatiors;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;
using Remembvoc.Infrastructure;
using Remembvoc.UI.AdditionalUI.AdditionalWindows;

namespace Remembvoc.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureService)
            .Build();
    }

    private void ConfigureService(HostBuilderContext context, IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddTransient<Func<IWordService>>(sp => sp.GetRequiredService<IWordService>);
        services.AddTransient<Func<IPriorityService>>(sp => sp.GetRequiredService<IPriorityService>);
        services.AddTransient<IWordService, WordService>();
        services.AddTransient<IPriorityService, PriorityService>();
        services.AddScoped<ITranslationService<WordTranslationResponse>, TranslationService>();
        services.AddTransient<IWordValidator, WordValidator>();
        services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
        services.AddSingleton<INotificationIcon, NotifyIconBackground>();
        services.AddSingleton<IPaginationService, PaginationService>();
        services.AddSingleton<IBackgroundService<BackgroundServiceParameters>, WordRepeatingBackgroundService>();
        services.AddSingleton<Lazy<IBackgroundService<BackgroundServiceParameters>>>(sp => new Lazy<IBackgroundService<BackgroundServiceParameters>>(sp.GetRequiredService<IBackgroundService<BackgroundServiceParameters>>));
        services.AddSingleton<MainWindow>();
        services.AddSingleton<PagesData>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AddNewWordWindow>();
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