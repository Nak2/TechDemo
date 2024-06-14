using Interface.ApiService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Interface;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoadingWindow : Window
{
    private readonly IApiService _apiService;
    private readonly DispatcherTimer _timer;
    private readonly IServiceProvider _serviceProvider;

    public LoadingWindow(IApiService apiService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _apiService = apiService;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2)
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private async void Timer_Tick(object sender, EventArgs e)
    {
        var result = await _apiService.GetDataAsync("HealthCheck");
        if (result.StatusCode == 200)
        {
            // Handle success
            var mainPanel = _serviceProvider.GetRequiredService<MainPanel>();

            // Change window to new
            this.Close();
            Application.Current.MainWindow = mainPanel;
            Application.Current.MainWindow.Show();
        }
        else
        {
            // Handle error
            StatusBlock.Text = "Failed to connect to server.\nRetrying...";
        }
    }
}