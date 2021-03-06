using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGCont.Models
 {
    [Table ("EspecialistaExternoId_ContratoId")]
    public class EspExternoId_ContratoId {
        public int Id { get; set; }
        public int EspecialistaExternoId { get; set; }
        public EspecialistaExterno EspecialistaExterno { get; set; }
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
    }
}