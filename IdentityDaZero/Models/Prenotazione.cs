using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityDaZero.Models {
    public class Prenotazione {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name ="Conferma Password")]
        [Compare("Password", ErrorMessage ="Le due password non coincidono.")]
        public string ConfermaPassword { get; set; }
        
        public string Telefono { get; set; }
        public bool? Partecipazione { get; set; }
    }
}