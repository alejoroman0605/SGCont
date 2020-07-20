using System.Linq;
using SGCont.Data;
using SGCont.Dtos;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers
{
    [Route ("contratacion/[controller]")]
    [ApiController]
    public class ComiteContratacionController : Controller {
        private readonly SGContDbContext  context;

        public ComiteContratacionController (SGContDbContext  context) {
            this.context = context;
        }

        // GET contratos/AdminContratos
        [HttpGet]
        public IActionResult GetAll () {
            var trabajadores = context.Trabajadores.ToList ();

            var trabajadoresComCont = context.ComiteContratacion.Where (t => t.Activo)
                .Select (d => new {
                    trabComiteContratacion = trabajadores.FirstOrDefault (t => t.Id == d.TrabComiteContratacionId),
                }).ToList ();

            return Ok (trabajadoresComCont);
        }

        // GET: contratos/AdminContratos/Id
        [HttpGet ("{id}", Name = "GetTrabComiteCont")]
        public IActionResult GetbyId (int id) {

            return Ok ();
        }

        // POST contratos/AdminContratos
        [HttpPost]
        public IActionResult POST ([FromBody] ComiteContratacionDto comiteContratacionDto) {
            if (ModelState.IsValid) {
                foreach (var item in comiteContratacionDto.ComiteContratacion) {
                    var trab = context.ComiteContratacion.FirstOrDefault (c => c.TrabComiteContratacionId == item && c.Activo == false);
                    if (trab != null) {
                        trab.Activo = true;
                        context.Entry (trab).State = EntityState.Modified;
                        context.SaveChanges ();
                    } else {
                        var trabComiteContratacion = new ComiteContratacion () {
                            TrabComiteContratacionId = item,
                            Activo = true
                        };
                        context.ComiteContratacion.Add (trabComiteContratacion);
                        context.SaveChanges ();
                    }
                }
                return Ok ();
            }
            return BadRequest (ModelState);
        }

        // PUT contratos/trabComiteContratacion/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] ComiteContratacionDto comiteContratacionDto, int id) {
            // var trab = context.ComiteContratacion.FirstOrDefault (s => s.Id == id);
            // foreach (var item in comiteContratacionDto.ComiteContratacion) {
            // }
            // context.Entry (trab).State = EntityState.Modified;
            // context.SaveChanges ();
            return Ok ();
        }

        // DELETE contratos/trabComiteContratacion/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var trabComiteContratacion = context.ComiteContratacion.FirstOrDefault (s => s.TrabComiteContratacionId == id);

            if (trabComiteContratacion == null) {
                return NotFound ();
            }
            trabComiteContratacion.Activo = false;
            context.SaveChanges ();
            return Ok (trabComiteContratacion);
        }
    }
}