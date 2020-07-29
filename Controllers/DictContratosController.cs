using System;
using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using SGCont.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers
{
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class DictContratosController : Controller {
        private readonly SGContDbContext  context;

        public DictContratosController (SGContDbContext  context) {
            this.context = context;
        }

        // GET SGCont/DictContratos
        [HttpGet]
        public IActionResult GetAll () {
            var trabajadores = context.Trabajadores.ToList ();

            var dictaminadores = context.DictaminadoresContratos.Include (d => d.Departamento)
                .Select (d => new {
                    dictaminador = trabajadores.FirstOrDefault (t => t.Id == d.DictaminadorId),
                        Departamento = d.Departamento,
                }).ToList ();

            return Ok (dictaminadores);
        }

        // GET: SGCont/DictContratos/Id
        [HttpGet ("{id}", Name = "Getdictaminador")]
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
            var dictaminador = trabajadores.FirstOrDefault (t => t.Id == id);
            if (dictaminador == null) {
                return NotFound ();
            }
            return Ok (dictaminador);
        }

        // POST SGCont/DictContratos
        [HttpPost]
        public IActionResult POST ([FromBody] DictContratosDto dictContratosDto) {
            if (ModelState.IsValid) {
                foreach (var item in dictContratosDto.Dictaminadores) {
                    if (context.DictaminadoresContratos.FirstOrDefault (s => s.DictaminadorId == item) == null) {
                        var dictaminador = new DictaminadorContrato {
                        DictaminadorId = item,
                        DepartamentoId = dictContratosDto.DepartamentoId
                        };
                        context.DictaminadoresContratos.Add (dictaminador);
                        context.SaveChanges ();
                    }
                }
                return Ok ();
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/DictContratos/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] DictContratosDto dictContratosDto, int id) {
            var dictaminador = context.DictaminadoresContratos.FirstOrDefault (s => s.DictaminadorId == id);
            foreach (var item in dictContratosDto.Dictaminadores) {
                dictaminador.DictaminadorId = item;
            }
            dictaminador.DepartamentoId = dictContratosDto.DepartamentoId;
            context.Entry (dictaminador).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE SGCont/DictContratos/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var dictaminador = context.DictaminadoresContratos.FirstOrDefault (s => s.DictaminadorId == id);

            if (dictaminador == null) {
                return NotFound ();
            }
            context.DictaminadoresContratos.Remove (dictaminador);
            context.SaveChanges ();
            return Ok (dictaminador);
        }
        //Get :SGCont/DictContratos/TrabNoDictaminadores
        [HttpGet ("/SGCont/DictContratos/TrabNoDictaminadores")]
        public IActionResult GetTrabNoDictaminadores () {
            var trabajadores = context.Trabajadores.ToList ();
            var dictaminador = context.DictaminadoresContratos.ToList ();
            var dictaminadores = new List<Trabajador> ();

            foreach (var item in dictaminador) {
                dictaminadores.Add (trabajadores.FirstOrDefault (s => s.Id == item.DictaminadorId));
            }
            return Ok (trabajadores.Except (dictaminadores));
        }
    }
}