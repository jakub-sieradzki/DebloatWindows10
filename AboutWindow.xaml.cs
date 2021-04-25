using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace DebloatWindows10
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            versionTextBlock.Text = "Version " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);
            e.Handled = true;
        }
    }
}
