using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfUserManagerDemo.Models;

namespace WpfUserManagerDemo.Managers
{
    // UserManager ansvarar för hantering av inloggad användare och användarlista
    public class UserManager : INotifyPropertyChanged
    {
        private User _currentUser;          // Håller koll på nuvarande inloggad användare
        private List<User> _users;          // Lista över alla användare i systemet

        public UserManager()
        {
            _users = new List<User>();
            SeedDefaultUsers();            // Skapar några standardanvändare vid start
        }

        // Egenskap för att hämta nuvarande användare
        public User CurrentUser
        {
            get { return _currentUser; }
            private set
            {
                _currentUser = value;
                // Meddela UI om att CurrentUser har ändrats
                OnPropertyChanged(nameof(CurrentUser));
                // Meddela UI om att IsAuthenticated också har ändrats
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        // Returnerar true om någon är inloggad, annars false
        public bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }

        // Publik property för att läsa listan av användare
        public List<User> Users
        {
            get { return _users; }
        }

        // Skapar två standardanvändare (admin och user)
        private void SeedDefaultUsers()
        {
            _users.Add(new User
            {
                Username = "admin",
                Password = "password",
                DisplayName = "Administrator",
                Role = "Admin"
            });

            _users.Add(new User
            {
                Username = "user",
                Password = "password",
                DisplayName = "Regular User",
                Role = "User"
            });
        }

        // Försöker logga in en användare baserat på användarnamn och lösenord
        public bool Login(string username, string password)
        {
            foreach (var u in _users)
            {
                // Jämför användarnamn (ignorera versaler/gemener) och lösenord
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
                    && u.Password == password)
                {
                    CurrentUser = u;  // Sätt nuvarande användare
                    return true;      // Inloggning lyckades
                }
            }
            return false;             // Inloggning misslyckades
        }

        // Loggar ut den nuvarande användaren
        public void Logout()
        {
            CurrentUser = null;
        }

        // Registrerar en ny användare om användarnamnet inte redan finns
        public bool Register(string username, string password, string displayName, string role)
        {
            // Kontrollera om användarnamnet är upptaget
            foreach (var u in _users)
            {
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            // Skapa och lägg till ny användare
            var newUser = new User
            {
                Username = username,
                Password = password,
                DisplayName = displayName,
                Role = role
            };

            _users.Add(newUser);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
