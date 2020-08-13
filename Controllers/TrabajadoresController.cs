using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCont.Data;
using SGCont.Dtos;
using SGCont.Models;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class TrabajadoresController : Controller {
        private readonly SGContDbContext context;
        public TrabajadoresController (SGContDbContext context) {
            this.context = context;
        }
        // GET SGCont/trabajadores
        [HttpGet]
        public IActionResult GetAll () {
            var trabajadores = context.Trabajadores
                .Where (s =>
                    s.EstadoTrabajador != EstadoTrabajador.Baja && s.EstadoTrabajador != EstadoTrabajador.Descartado)
                .Select (t => new {
                    Id = t.Id,
                        Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase (t.Nombre),
                        Apellidos = t.Apellidos,
                        Nombre_Completo = CultureInfo.InvariantCulture.TextInfo.ToTitleCase (t.Nombre + " " + t.Apellidos),
                        CI = t.CI,
                        Sexo = t.Sexo,
                        SexoName = t.Sexo.ToString (),
                        TelefonoFijo = t.TelefonoFijo,
                        TelefonoMovil = t.TelefonoMovil,
                        Direccion = t.Direccion,
                        EstadoTrabajador = t.EstadoTrabajador,
                        EstadoTrabajadorName = t.EstadoTrabajador.ToString (),
                        Correo = t.Correo,
                        Municipio = t.Municipio.Nombre,
                        // ColorDePiel = t.CaracteristicasTrab.ColorDePiel,
                        // ColorDePielName = t.CaracteristicasTrab.ColorDePiel.ToString (),
                        // ColorDeOjos = t.CaracteristicasTrab.ColorDeOjos,
                        // ColorDeOjosName = t.CaracteristicasTrab.ColorDeOjos.ToString (),
                        // TallaPantalon = t.CaracteristicasTrab.TallaPantalon,
                        // TallaCalzado = t.CaracteristicasTrab.TallaCalzado,
                        // TallaCalzadoName = t.CaracteristicasTrab.TallaCalzado.ToString (),
                        // TallaDeCamisa = t.CaracteristicasTrab.TallaDeCamisa,
                        // TallaDeCamisaName = t.CaracteristicasTrab.TallaDeCamisa.ToString (),
                        OtrasCaracteristicas = t.CaracteristicasTrab.OtrasCaracteristicas,
                        Foto = t.CaracteristicasTrab.Foto,
                        Fecha_Nac = t.Fecha_Nac.ToString ("dd MMMM yyyy"),
                        Edad = (DateTime.Now - t.Fecha_Nac).Days / 365,
                });

            if (trabajadores == null) {
                return NotFound ();
            }
            return Ok (trabajadores);
        }

        // GET: SGCont/trabajadores/Id
        [HttpGet ("{id}", Name = "GetTrab")]
        public IActionResult GetbyId (int id) {

            var trabajador = context.Trabajadores.Where (s => s.Id == id)
                .Select (t => new {
                    Id = t.Id,
                        Nombre = t.Nombre,
                        Apellidos = t.Apellidos,
                        CI = t.CI,
                        Sexo = t.Sexo.ToString (),
                        TelefonoFijo = t.TelefonoFijo,
                        TelefonoMovil = t.TelefonoMovil,
                        Direccion = t.Direccion,
                        MunicipioProv = t.Municipio.Nombre + " " + t.Municipio.Provincia.Nombre,
                        Municipio = t.Municipio.Nombre,
                        Correo = t.Correo,
                        Nombre_Completo = CultureInfo.InvariantCulture.TextInfo.ToTitleCase (t.Nombre + " " + t.Apellidos),
                        ColorDePiel = t.CaracteristicasTrab.ColorDePiel.ToString (),
                        ColorDeOjos = t.CaracteristicasTrab.ColorDeOjos.ToString (),
                        TallaPantalon = t.CaracteristicasTrab.TallaPantalon,
                        TallaCalzado = t.CaracteristicasTrab.TallaCalzado.ToString (),
                        TallaDeCamisa = t.CaracteristicasTrab.TallaDeCamisa.ToString (),
                        OtrasCaracteristicas = t.CaracteristicasTrab.OtrasCaracteristicas,
                        Resumen = t.CaracteristicasTrab.Resumen,
                        Foto = t.CaracteristicasTrab.Foto,
                });

            if (trabajador == null) {
                return NotFound ();
            }
            return Ok (trabajador);
        }

        // POST SGCont/trabajadores
        [HttpPost]
        public IActionResult POST ([FromBody] TrabajadorDto trabajadorDto) {
            if (ModelState.IsValid) {
                var trab = context.Trabajadores.FirstOrDefault (s => s.CI == trabajadorDto.CI);
                if (trab != null) {
                    if (trab.EstadoTrabajador == EstadoTrabajador.Descartado) {
                        return BadRequest ($"El trabajador ya está en el sistema y fue descartado de la bolsa");
                    }
                    return BadRequest ($"El trabajador ya está en el sistema");
                } else {
                    // Saber sexo y fecha de cumpleaños por el CI
                    var sexo = new Sexo ();
                    string fecha = "";
                    DateTime fechaNac = new DateTime ();
                    IFormatProvider culture = new System.Globalization.CultureInfo ("en-US", true);
                    if (trabajadorDto.CI != null) {
                        var sexoCI = int.Parse (trabajadorDto.CI.Substring (9, 1));
                        if (sexoCI % 2 == 0) {
                            sexo = Sexo.M;
                        } else {
                            sexo = Sexo.F;
                        }
                        var siglo = int.Parse (trabajadorDto.CI.Substring (6, 1));
                        if (siglo >= 0 && siglo <= 5) {
                            fecha = "19" + trabajadorDto.CI.Substring (0, 6);
                        }
                        if (siglo >= 6 && siglo <= 8) {
                            fecha = "20" + trabajadorDto.CI.Substring (0, 6);
                        }
                        fechaNac = DateTime.ParseExact (fecha, "yyyyMMdd", culture);
                    }
                    //Crear Trabajadores
                    var trabajador = new Trabajador () {
                        Nombre = trabajadorDto.Nombre,
                        Apellidos = trabajadorDto.Apellidos,
                        CI = trabajadorDto.CI,
                        Sexo = sexo,
                        Direccion = trabajadorDto.Direccion,
                        Correo = trabajadorDto.Correo,
                        TelefonoMovil = trabajadorDto.TelefonoMovil,
                        TelefonoFijo = trabajadorDto.TelefonoFijo,
                        EstadoTrabajador = EstadoTrabajador.Bolsa,
                        Fecha_Nac = fechaNac,
                    };
                    context.Trabajadores.Add (trabajador);
                    context.SaveChanges ();
                }
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/trabajadores/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] TrabajadorDto trabajadorDto, int id) {
            var sexo = new Sexo ();
            string fecha = "";
            DateTime fechaNac = new DateTime ();
            IFormatProvider culture = new System.Globalization.CultureInfo ("en-US", true);
            if (ModelState.IsValid) {
                var t = context.Trabajadores.Find (id);
                if (t != null) {
                    t.Nombre = trabajadorDto.Nombre;
                    t.Apellidos = trabajadorDto.Apellidos;
                    t.Direccion = trabajadorDto.Direccion;
                    t.Correo = trabajadorDto.Correo;
                    t.MunicipioId = trabajadorDto.MunicipioId;
                    t.TelefonoMovil = trabajadorDto.TelefonoMovil;
                    t.TelefonoFijo = trabajadorDto.TelefonoFijo;

                    //Cambio del sexo y fecha Nacimiento en caso de que cambie el CI
                    if (t.CI != trabajadorDto.CI) {
                        if (trabajadorDto.CI != null) {
                            var sexoCI = int.Parse (trabajadorDto.CI.Substring (9, 1));
                            if (sexoCI % 2 == 0) {
                                sexo = Sexo.M;
                            } else {
                                sexo = Sexo.F;
                            }
                            var siglo = int.Parse (trabajadorDto.CI.Substring (6, 1));
                            if (siglo >= 0 && siglo <= 5) {
                                fecha = "19" + trabajadorDto.CI.Substring (0, 6);
                            }
                            if (siglo >= 6 && siglo <= 8) {
                                fecha = "20" + trabajadorDto.CI.Substring (0, 6);
                            }
                            t.Sexo = sexo;
                            fechaNac = DateTime.ParseExact (fecha, "yyyyMMdd", culture);
                            t.Fecha_Nac = fechaNac;
                        }
                    }
                    t.CI = trabajadorDto.CI;
                    context.Update (t);

                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        // Pasar a Descartado el Trabajadores en Bolsa
        // DELETE SGCont/trabajadores/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var trabajador = context.Trabajadores.FirstOrDefault (s => s.Id == id);

            if (!TrabajadorExists (id)) {
                return NotFound ();
            }
            trabajador.EstadoTrabajador = EstadoTrabajador.Descartado;
            context.SaveChanges ();
            return Ok (trabajador);
        }
        // GET SGCont/Trabajadores/Municipios
        [HttpGet ("/SGCont/Trabajadores/Municipios")]
        public IEnumerable<Municipio> GetAllMunicipios () {
            return context.Municipio.ToList ();
        }

        // GET SGCont/Trabajadores/ByUserId
        [HttpGet ("/SGCont/Trabajadores/ByUserId")]
        public async Task<IActionResult> GetAllTrabUserId (bool tieneUserId) {
            if (tieneUserId) {
                var trabajadores = context.Trabajadores.Where (t => t.UserId != null).Select (t => new {
                    Id = t.Id,
                        Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase (t.Nombre),
                        Apellidos = t.Apellidos,
                        NombreCompleto = CultureInfo.InvariantCulture.TextInfo.ToTitleCase (t.Nombre + " " + t.Apellidos),
                        CI = t.CI,
                        User = t.UserId
                }).ToList ();
                return Ok (trabajadores);
            }
            return Ok (context.Trabajadores.Where (t => t.UserId == null).ToList ());
        }
        // POST SGCont/Trabajadores/AddUserId
        [HttpPost ("/SGCont/Trabajadores/AddUserId")]
        public IActionResult AddUserId (List<UserTrabajador> userTrabajadorDto) {
            foreach (var item in userTrabajadorDto) {
                var t = context.Trabajadores.Find (item.TrabajadorId);
                t.UserId = item.UserId;
                context.Update (t);
            }
            context.SaveChanges ();
            return Ok ();
        }
        // PUT SGCont/Trabajadores/NullUserId
        [HttpPut ("/SGCont/Trabajadores/NullUserId/{id}")]
        public IActionResult NullUserId ([FromBody] TrabajadorDto trabajadorDto, int id) {
            var t = context.Trabajadores.Find (id);
            t.UserId = null;
            context.Update (t);
            context.SaveChanges ();
            return Ok ();
        }
        private bool TrabajadorExists (int id) {
            return context.Trabajadores.Any (e => e.Id == id);
        }
    }
}