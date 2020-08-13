using System;
using System.ComponentModel.DataAnnotations.Schema;
using SGCont.Models;

namespace SGCont.Dtos
{
    public class TrabajadorDto {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string CI { get; set; }
    public string TelefonoFijo { get; set; }
    public string TelefonoMovil { get; set; }
    public virtual Sexo Sexo { get; set; }
    public byte[] Foto { get; set; }
    public string Direccion { get; set; }
    public string Correo { get; set; }
    public ColorDeOjos ColorDeOjos { get; set; }
    public ColorDePiel ColorDePiel { get; set; }
    public TallaDeCamisa TallaDeCamisa { get; set; }
    public string TallaPantalon { get; set; }
    public double TallaCalzado { get; set; }
    public string OtrasCaracteristicas { get; set; }
    public int? MunicipioId { get; set; }
    public string EstadoTrabajador { get; set; }
    public DateTime Fecha { get; set; }
    public int PerfilOcupacionalId { get; set; }
    public string Nombre_Referencia { get; set; }
    public string UserId { get; set; }

    [NotMapped]
    private string _nombreCompleto;

    [NotMapped]
    public string NombreCompleto {
      get { return Nombre + " " + Apellidos; }
      set { _nombreCompleto = value; }
    }
  }
}