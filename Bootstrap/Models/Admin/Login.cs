﻿using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Models.Admin
{
    public class Login
    {
        [Required]
        public string LoginName { get; set; } = null!;
        [EmailAddress]
        public string LoginEmail { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
