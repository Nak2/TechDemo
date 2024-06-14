using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Interface;

/// <summary>
/// Interaction logic for MainPanel.xaml
/// </summary>
public partial class MainPanel : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainPanel(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private void OnSalesUsers(object sender, RoutedEventArgs e)
    {
        // Go to SaleUsers page
        var saleUsersWindow = _serviceProvider.GetRequiredService<SalesUserPanel>();
        this.Close();
    }
    private void OnDistricts(object sender, RoutedEventArgs e)
    {

    }
    private void OnSecondaryRoles(object sender, RoutedEventArgs e)
    {
    }
}
