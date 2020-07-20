using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models
{
    [Table("municipios")]
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProvinciaId { get; set; }
        public virtual Provincia Provincia { get; set; }

    }
}