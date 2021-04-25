using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DebloatWindows10
{
    /// <summary>
    /// Interaction logic for ModifyApps.xaml
    /// </summary>
    public partial class ModifyApps : Page
    {
        private string mode;
        public ModifyApps(string mode)
        {
            InitializeComponent();

            this.mode = mode;
            AppsInfo.SetOptions(false);

            if (mode == "uninstall")
            {
                modifyAppsListView.ItemsSource = AppsInfo.GetInstalledAppsInfo(true);
                titleTextBlock.Text = "Installed apps";
                modifyButton.Content = "Uninstall";
                modifyButton.Click += UninstallApps_Click;
            }
            else if (mode == "restore")
            {
                modifyAppsListView.ItemsSource = AppsInfo.GetUninstalledAppsInfo(true);
                titleTextBlock.Text = "Uninstalled apps";
                modifyButton.Content = "Restore";
                modifyButton.Click += RestoreApps_Click;
            }
        }

        private void UninstallApps_Click(object sender, RoutedEventArgs e)
        {
            List<AppItem> appItemToUninstall = AppsInfo.GetInstalledAppsInfo(false).FindAll(x => x.Checked == true);
            if(appItemToUninstall.Count > 0)
            {
                new ModifyAppsScript(appItemToUninstall, "uninstall");
            }
        }

        private void RestoreApps_Click(object sender, RoutedEventArgs e)
        {
            List<AppItem> appItemToRestore = AppsInfo.GetUninstalledAppsInfo(false).FindAll(x => x.Checked == true);
            if(appItemToRestore.Count > 0)
            {
                new ModifyAppsScript(appItemToRestore, "restore");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            if (service.CanGoBack)
            {
                service.GoBack();
            }
        }

        private void ModifyAppsListView_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (modifyAppsListView.SelectedIndex != -1)
            {
                if (mode == "uninstall")
                {
                    AppsInfo.GetInstalledAppsInfo(false)[modifyAppsListView.SelectedIndex].Checked =
                        !AppsInfo.GetInstalledAppsInfo(false)[modifyAppsListView.SelectedIndex].Checked;
                }
                else if (mode == "restore")
                {
                    AppsInfo.GetUninstalledAppsInfo(false)[modifyAppsListView.SelectedIndex].Checked =
                        !AppsInfo.GetUninstalledAppsInfo(false)[modifyAppsListView.SelectedIndex].Checked;
                }
                modifyAppsListView.Items.Refresh();
            }
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            if(mode == "uninstall")
            {
                AppsInfo.GetInstalledAppsInfo(false).ForEach(c => c.Checked = true);
            }
            else if(mode == "restore")
            {
                AppsInfo.GetUninstalledAppsInfo(false).ForEach(c => c.Checked = true);
            }
            modifyAppsListView.Items.Refresh();

            selectAllButton.Content = "Unselect all";
            selectAllButton.Click += UnselectAllButton_Click;
        }

        private void UnselectAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (mode == "uninstall")
            {
                AppsInfo.GetInstalledAppsInfo(false).ForEach(c => c.Checked = false);
            }
            else if (mode == "restore")
            {
                AppsInfo.GetUninstalledAppsInfo(false).ForEach(c => c.Checked = false);
            }
            modifyAppsListView.Items.Refresh();

            selectAllButton.Content = "Select all";
            selectAllButton.Click += SelectAllButton_Click;
        }

        private void ShowAllAppsCheck_Click(object sender, RoutedEventArgs e)
        {
            AppsInfo.SetOptions(showAllAppsCheck.IsChecked);
            if (mode == "uninstall")
            {
                modifyAppsListView.ItemsSource = AppsInfo.GetInstalledAppsInfo(true);
            }
            else if (mode == "restore")
            {
                modifyAppsListView.ItemsSource = AppsInfo.GetUninstalledAppsInfo(true);
            }
        }
    }
}
