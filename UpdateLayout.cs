using System;
using System.Windows;
using System.Windows.Navigation;

namespace DebloatWindows10
{
    class UpdateLayout
    {
        private ModifyApps modifyAppsPage;
        private int minimum;
        private int maximum;
        public UpdateLayout(int minimum, int maximum)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                NavigationService service = (Application.Current.MainWindow as MainWindow).mainFrame.NavigationService;
                if (service.Content == service.Content as ModifyApps)
                {
                    modifyAppsPage = service.Content as ModifyApps;
                }
            }));

            this.minimum = minimum;
            this.maximum = maximum;
        }

        public void SetupProgressBar()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.modifyProgressBar.Visibility = Visibility.Visible;
                    modifyAppsPage.modifyProgressBar.Minimum = minimum;
                    modifyAppsPage.modifyProgressBar.Maximum = maximum;
                    modifyAppsPage.modifyProgressBar.Value = 0;
                }
            }));
        }

        public void SetupPercentText()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.modifyPercent.Visibility = Visibility.Visible;
                    modifyAppsPage.modifyPercent.Text = "0%";
                }
            }));
        }

        public void SetEnabledModifyButton(bool isEnabled)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.modifyButton.IsEnabled = isEnabled;
                }
            }));
        }

        public void SetEnabledList(bool isEnabled)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.modifyAppsListView.IsEnabled = isEnabled;
                }
            }));
        }

        public void SetEnabledSelectAllButton(bool isEnabled)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.selectAllButton.IsEnabled = isEnabled;
                }
            }));
        }

        public void UpdateProgress(int value, string appName)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    modifyAppsPage.modifyProgressBar.Value = value;
                    modifyAppsPage.modifyPercent.Text = Math.Round(((float)value / maximum) * 100) + "%";

                    if (value == modifyAppsPage.modifyProgressBar.Maximum)
                    {
                        modifyAppsPage.modifyAppName.Text = "Completed";
                    }
                    else
                    {
                        modifyAppsPage.modifyAppName.Text = "Processing: " + appName;
                    }
                }
            }));
        }

        public void UpdateList(string mode)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (modifyAppsPage != null)
                {
                    if (mode == "uninstall")
                    {
                        modifyAppsPage.modifyAppsListView.ItemsSource = AppsInfo.GetInstalledAppsInfo(true);
                    }
                    else if (mode == "restore")
                    {
                        modifyAppsPage.modifyAppsListView.ItemsSource = AppsInfo.GetUninstalledAppsInfo(true);
                    }
                }
            }));
        }
    }
}
