namespace SafeScribe.API.Models
{
    public class Note
    {
        public int Id { get; set; } // Identificador único da nota
        public string Title { get; set; } = string.Empty; // Título da nota
        public string Content { get; set; } = string.Empty; // Conteúdo da nota
        public DateTime CreatedAt { get; set; } // Data de criação
        public int UserId { get; set; } // ID do usuário que criou a nota
    }
}