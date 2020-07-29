using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class TiempoVenContratosController : Controller {
        private readonly SGContDbContext  context;
        public TiempoVenContratosController (SGContDbContext  context) {
            this.context = context;
        }

        // GET SGCont/TiempoVenContratos
        [HttpGet]
        public IEnumerable<TiempoVenContrato> GetAll () {
            return context.TiempoVenContratos.ToList ();
        }

        // GET: SGCont/TiempoVenContratos/Id
        [HttpGet ("{id}", Name = "GetTiempoVenContrato")]
        public IActionResult GetbyId (int id) {
            var tiempoVenContrato = context.TiempoVenContratos.FirstOrDefault (s => s.Id == id);

            if (tiempoVenContrato == null) {
                return NotFound ();
            }
            return Ok (tiempoVenContrato);
        }

        // POST SGCont/TiempoVenContratos
        [HttpPost]
        public IActionResult POST ([FromBody] TiempoVenContrato tiempoVenContrato) {
            if (ModelState.IsValid) {
                var t = context.TiempoVenContratos.ToList ();
                foreach (var item in t) {
                    context.TiempoVenContratos.Remove (item);
                }
                context.TiempoVenContratos.Add (tiempoVenContrato);
                context.SaveChanges ();
                return new CreatedAtRouteResult ("GetTiempoVenContrato", new { id = tiempoVenContrato.Id });
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/TiempoVenContratos/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] TiempoVenContrato tiempoVenContrato, int id) {
            if (tiempoVenContrato.Id != id) {
                return BadRequest (ModelState);

            }
            context.Entry (tiempoVenContrato).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE SGCont/TiempoVenContratos/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var tiempoVenContrato = context.TiempoVenContratos.FirstOrDefault (s => s.Id == id);

            if (tiempoVenContrato.Id != id) {
                return NotFound ();
            }
            context.TiempoVenContratos.Remove (tiempoVenContrato);
            context.SaveChanges ();
            return Ok (tiempoVenContrato);
        }
    }
}