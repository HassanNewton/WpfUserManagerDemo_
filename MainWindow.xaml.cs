using System.Windows;
using WpfUserManagerDemo.Managers;
using WpfUserManagerDemo.Views;

namespace WpfUserManagerDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Hämtar den globala UserManager-instansen från App.xaml
            var userManager = (UserManager)Application.Current.Resources["UserManager"];

            // Om ingen är inloggad, visa loginfönstret direkt
            if (!userManager.IsAuthenticated)
            {
                this.Hide(); // Dölj MainWindow tills någon loggat in
                var login = new LoginWindow();
                var result = login.ShowDialog();

                if (result == true)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Loggar ut användaren via UserManager
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            userManager.Logout();

            // Göm huvudfönstret och visa login igen
            this.Hide();
            var login = new LoginWindow();
            var result = login.ShowDialog();

            if (result == true)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }


    }
}
