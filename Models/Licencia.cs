using System;

namespace SGCont.Models
{
    public class Licencia
    {
        public int Id { get; set; }
        public string Aplicacion { get; set; }
        public string Subscriptor { get; set; }
        public DateTime Vencimiento { get; set; }
        public byte[] Hash { get; set; }
    }
}