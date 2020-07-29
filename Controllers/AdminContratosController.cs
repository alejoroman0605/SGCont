using System;
using System.Linq;
using SGCont.Data;
using SGCont.Dtos;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SGCont.Controllers
{
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class AdminContratosController : Controller {
        private readonly SGContDbContext  context;
        public AdminContratosController (SGContDbContext  context) {
            this.context = context;
        }

        // GET contratos/AdminContratos
        [HttpGet]
        public IActionResult GetAll () {
            var trabajadores = context.Trabajadores.ToList ();

            var adminContratos = context.AdminContratos.Include (d => d.Departamento)
                .Select (d => new {
                    Administrador = trabajadores.FirstOrDefault (t => t.Id == d.AdminContratoId),
                        Departamento = d.Departamento,
                }).ToList ();

            return Ok (adminContratos);
        }

        // GET: contratos/AdminContratos/Id
        [HttpGet ("{id}", Name = "GetAdminContrato")]
        public IActionResult GetbyId (int id) {
         var trabajadores = context.Trabajadores.Select (t => new {
                Id = t.Id,
                    Nombre = t.Nombre,
                    Apellidos = t.Apellidos,
                    Nombre_Completo = t.Nombre + " " + t.Apellidos,
                    CI = t.CI,
                    Sexo = t.Sexo,
                    SexoName = t.Sexo.ToString (),
                    TelefonoFijo = t.TelefonoFijo,
                    TelefonoMovil = t.TelefonoMovil,
                    Direccion = t.Direccion,
                    ColorDePiel = t.CaracteristicasTrab.ColorDePiel,
                    ColorDePielName = t.CaracteristicasTrab.ColorDePiel.ToString (),
                    ColorDeOjos = t.CaracteristicasTrab.ColorDeOjos,
                    ColorDeOjosName = t.CaracteristicasTrab.ColorDeOjos.ToString (),
                    TallaPantalon = t.CaracteristicasTrab.TallaPantalon,
                    TallaCalzado = t.CaracteristicasTrab.TallaCalzado,
                    TallaCalzadoName = t.CaracteristicasTrab.TallaCalzado.ToString (),
                    TallaDeCamisa = t.CaracteristicasTrab.TallaDeCamisa,
                    TallaDeCamisaName = t.CaracteristicasTrab.TallaDeCamisa.ToString (),
                    OtrasCaracteristicas = t.CaracteristicasTrab.OtrasCaracteristicas,
                    Resumen = t.CaracteristicasTrab.Resumen,
                    Foto = t.CaracteristicasTrab.Foto,
                    Edad = (DateTime.Now - t.Fecha_Nac).Days / 365,
                    Departamento = ""
            });
            var adminContrato = trabajadores.FirstOrDefault (t => t.Id == id);
            if (adminContrato == null) {
                return NotFound ();
            }
            return Ok (adminContrato);
        }

        // POST contratos/AdminContratos
        [HttpPost]
        public IActionResult POST ([FromBody] AdminContratosDto adminContratosDto) {
            if (ModelState.IsValid) {
                foreach (var item in adminContratosDto.Administradores) {
                    if (context.AdminContratos.FirstOrDefault (s => s.AdminContratoId == item) == null) {
                        var administrador = new AdminContrato {
                        AdminContratoId = item,
                        DepartamentoId = adminContratosDto.DepartamentoId
                        };
                        context.AdminContratos.Add (administrador);
                        context.SaveChanges ();
                    }
                }
                return Ok ();
            }
            return BadRequest (ModelState);
        }

        // PUT contratos/adminContrato/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] AdminContratosDto adminContratosDto, int id) {
            var administrador = context.AdminContratos.FirstOrDefault (s => s.AdminContratoId == id);
            foreach (var item in adminContratosDto.Administradores) {
                administrador.AdminContratoId = item;
            }
            administrador.DepartamentoId = adminContratosDto.DepartamentoId;
            context.Entry (administrador).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE contratos/adminContrato/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var adminContrato = context.AdminContratos.FirstOrDefault (s => s.AdminContratoId == id);

            if (adminContrato == null) {
                return NotFound ();
            }
            context.AdminContratos.Remove (adminContrato);
            context.SaveChanges ();
            return Ok (adminContrato);
        }
         //Get :SGCont/DictContratos/TrabNoDictaminadores
        [HttpGet ("/SGCont/AdminContratos/TrabNoAdmin")]
        public IActionResult GetTrabNoAdmin () {
            var trabajadores = context.Trabajadores.ToList ();
            var admin = context.AdminContratos.ToList ();
            var administradores = new List<Trabajador> ();

            foreach (var item in admin) {
                administradores.Add (trabajadores.FirstOrDefault (s => s.Id == item.AdminContratoId));
            }
            return Ok (trabajadores.Except (administradores));
        }
    }
}