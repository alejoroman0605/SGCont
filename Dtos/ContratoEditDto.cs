using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SGCont.Models;

namespace SGCont.Models {
    public class ContratoEditDto {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Tipo Tipo { get; set; }
        public int AdminContrato { get; set; }
        public Entidad Entidad { get; set; }
        public string ObjetoDeContrato { get; set; }
        public string Numero { get; set; }

        [Display (Name = "Monto CUP")]
        [DataType (DataType.Currency)]
        public decimal? MontoCup { get; set; }

        [Display (Name = "Monto CUC")]
        [DataType (DataType.Currency)]
        public decimal? MontoCuc { get; set; }

        [Display (Name = "Monto USD")]
        [DataType (DataType.Currency)]
        public decimal? MontoUsd { get; set; }
        public string[] monedas { get; set; }

        [DataType (DataType.Date)]
        [Display (Name = "Fecha de Llegada")]
        public DateTime FechaDeRecepcion { get; set; }

        [DataType (DataType.Date)]
        [Display (Name = "Fecha de Vencimiento")]
        public DateTime FechaVenContrato { get; set; }

        [DataType (DataType.Date)]
        [Required]
        public DateTime FechaDeVenOferta { get; set; }
        public DateTime FechaDeFirmado { get; set; }

        [Display (Name = "Formas de Pago")]
        public virtual List<FormaDePago> FormasDePago { get; set; }

        //Término de pago en días
        [Display (Name = "Término de Pago")]
        public int TerminoDePago { get; set; }
        public string Usuario { get; set; }
        public List<Departamento> Departamentos { get; set; }
        public List<EspecialistaExterno> EspecialistasExternos { get; set; }
        public bool AprobJurico { get; set; }
        public bool AprobEconomico { get; set; }
        public bool AprobComitContratacion { get; set; }
        public bool Cliente { get; set; }
    }
}