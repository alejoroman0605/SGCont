using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models

{
    public class ObjetoDeContrato
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [NotMapped]
        public virtual ICollection<Documento> Documentos { get; set; }

        public ObjetoDeContrato()
        {
            Documentos = new HashSet<Documento>();
        }
    }
}