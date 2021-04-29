using System;
using System.Windows;

namespace DebloatWindows10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            int version = Environment.OSVersion.Version.Build;
            if (version < 19041)
            {
                MessageBox.Show("This app works only on Windows 10 version 2004 or later. Please update your system to run this app.", "Error");
                Application.Current.Shutdown();
                return;
            }

            InitializeComponent();
            Application.Current.MainWindow = this;

            mainFrame.NavigationService.Navigate(new StartPage());
        }
    }
}
