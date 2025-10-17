using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfUserManagerDemo.Managers;

namespace WpfUserManagerDemo.Views
{
    public partial class LoginWindow : Window
    {
        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set { _error = value; OnPropertyChanged(); }
        }

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this; // 👈 databinding till fönstret självt
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var success = userManager.Login(Username, Password);

            if (success)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                Error = "Fel användarnamn eller lösenord.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }
    }
}
