using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models
 {
    public class AdminContrato {
        public int Id { get; set; }
        public int AdminContratoId { get; set; }
    }
}