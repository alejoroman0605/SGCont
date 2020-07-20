using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGCont.Models;

namespace SGCont.Dtos {
    public class AproContratoDto
    {
        public bool AprobJuridico { get; set; }
        public bool AprobEconomico { get; set; }
        public bool AprobComitContratacion { get; set; }
    }
}