using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGCont.Models;

namespace SGCont.Dtos {
    public class AdminCont_Cant {
        public AdminContrato AdminContrato{get;set;}
        public int CantidadContratos{get;set;}
        public int CantidadOfertas{get;set;}
    }
}