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
            InitializeComponent();
            Application.Current.MainWindow = this;

            mainFrame.NavigationService.Navigate(new StartPage());
        }
    }
}
