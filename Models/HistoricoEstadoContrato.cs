using System;

namespace SGCont.Models

{
    public class HistoricoEstadoContrato
    {
        public int Id { get; set; }

        public int ContratoId { get; set; }

        public virtual Contrato Contrato { get; set; }

        public Estado Estado { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }
    }
}