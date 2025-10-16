using System.Windows;
using WpfUserManagerDemo.Managers;

namespace WpfUserManagerDemo.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Hämtar UserManager-instansen från App.xaml
            var userManager = (UserManager)Application.Current.Resources["UserManager"];

            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            // Försök logga in med angivna uppgifter
            bool success = userManager.Login(username, password);

            if (success)
            {
                // Om inloggningen lyckades stäng loginfönstret
                DialogResult = true;
                Close();
            }
            else
            {
                // Visa felmeddelande om uppgifterna inte stämde
                ErrorText.Text = "Fel användarnamn eller lösenord.";
            }
        }
    }
}
