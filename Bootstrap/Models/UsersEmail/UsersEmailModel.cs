using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.UsersEmail
{
    public class UsersEmailModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
    }
}
