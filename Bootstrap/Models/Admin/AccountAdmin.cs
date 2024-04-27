using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.Admin
{

    public class AccountAdmin : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AdminName { get; set; } = null!;
        [Required]
        public string AdminEmail { get; set; } = null!;
       
    }
}
