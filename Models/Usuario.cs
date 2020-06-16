using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SGCont.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        public bool Activo { get; set; }
    }
}