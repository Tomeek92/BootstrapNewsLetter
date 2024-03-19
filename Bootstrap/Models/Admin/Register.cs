using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.Admin
{
    public class Register
    {

        public string UserName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
