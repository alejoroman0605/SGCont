using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models {
    public class Departamento {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}