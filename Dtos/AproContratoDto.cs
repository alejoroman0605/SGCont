using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGCont.Models;

namespace SGCont.Dtos {
    public class AproContratoDto
    {
       public List<string> roles { get; set; }
        public int ContratoId { get; set; }
        public DateTime FechaDeFirmado { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
    }
}