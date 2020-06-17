using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models
 {
    [Table ("ContratoId_DictaminadorId")]
    public class ContratoId_DictaminadorId {
        public int Id { get; set; }
        [Required]
        public int ContratoId { get; set; }
        
        //trabajador en el Cargo de Jurídico o Económico
        public int DictaminadorContratoId { get; set; }
    }
}