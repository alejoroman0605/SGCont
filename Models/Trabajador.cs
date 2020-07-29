using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models
{
    [Table ("trabajadores")]
    public class Trabajador {
        public int Id { get; set; }
        public string Codigo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }

        [Required]
        [RegularExpression (@"^[0-9]*$", ErrorMessage = "Inserte un número de identidad válido")]
        [StringLength (11, MinimumLength = 11, ErrorMessage = "Inserte un número de identidad válido")]
        public string CI { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string Correo { get; set; }
        public Sexo? Sexo { get; set; }
        public string Direccion { get; set; }
        public int? MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual CaracteristicasTrab CaracteristicasTrab { get; set; }
        public DateTime Fecha_Nac { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        private string _nombreCompleto;

        [NotMapped]
        public string NombreCompleto {
            get { return Nombre + " " + Apellidos; }
            set { _nombreCompleto = value; }
        }
    }
}