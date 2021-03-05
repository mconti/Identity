using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityDaZero.Models {
    public class Login {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ricordami")]
        public bool Ricordami { get; set; }
    }
}