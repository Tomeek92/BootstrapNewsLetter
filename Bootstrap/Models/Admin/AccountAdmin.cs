using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.Admin
{
    public class AccountAdmin
    {
        [Key]
        public Guid Id { get; set; }
        public string AdminEmail { get; set; } = null!;

    }
}
