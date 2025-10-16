namespace WpfUserManagerDemo.Models
{
    // Enkel modellklass för att representera en användare
    public class User
    {
        // Användarnamn för inloggning
        public string Username { get; set; }

        // Visningsnamn som kan visas i UI
        public string DisplayName { get; set; }

        // Roll t.ex. "Admin" eller "User"
        public string Role { get; set; }

        // Lösenord - OBS: lagras i klartext i denna övning
        public string Password { get; set; }
    }
}
