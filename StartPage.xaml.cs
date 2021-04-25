using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DebloatWindows10
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void NavToUninstallApps_Click(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new ModifyApps("uninstall"));
        }

        private void NavToRestoreApps_Click(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new ModifyApps("restore"));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().Show();
        }
    }
}
