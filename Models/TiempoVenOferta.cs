using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGCont.Models;

public class TiempoVenOferta {
    public int Id { get; set; }
    public int OfertaTiempo { get; set; }
    public  int OfertasProxVencDesde { get; set; }
    public  int OfertasProxVencHasta { get; set; }
    public  int OfertasCasiVencDesde { get; set; }
    public  int OfertasCasiVencHasta { get; set; }
    public  int OfertasVencidas { get; set; }
}