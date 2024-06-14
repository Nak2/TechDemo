using Interface.ApiService;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Interface;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IApiService, ApiService.ApiService>();
        services.AddSingleton<LoadingWindow>();
        services.AddSingleton<MainPanel>();
        services.AddSingleton<SalesUserPanel>();
    }

    protected void OnStartup(object sender, StartupEventArgs e)
    {
        var loadingWindow = _serviceProvider.GetRequiredService<LoadingWindow>();

        // Change window to new
        Application.Current.MainWindow = loadingWindow;
        Application.Current.MainWindow.Show();
    }
}

