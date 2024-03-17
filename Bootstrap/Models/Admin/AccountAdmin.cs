using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.Admin
{
    public class AccountAdmin
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AdminName { get; set; } = null!;
        [Required]
        public string AdminEmail { get; set; } = null!;
        [Required]
        public string AdminPassword { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
