using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGCont.Models;

namespace SGCont.Dtos {
    public class AdminContratosDto {
        public List<int> Administradores { get; set; }
        public int DepartamentoId { get; set; }
    }
}