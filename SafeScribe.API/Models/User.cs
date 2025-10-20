namespace SafeScribe.API.Models
{
    public class User
    {
        public int Id { get; set; } // Identificador único do usuário
        public string Username { get; set; } = string.Empty; // Nome de login
        public string PasswordHash { get; set; } = string.Empty; // Senha criptografada
        public string Role { get; set; } = string.Empty; // Perfil
    }
}