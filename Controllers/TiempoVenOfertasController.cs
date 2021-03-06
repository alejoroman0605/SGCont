using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class TiempoVenOfertasController : Controller {
        private readonly SGContDbContext  context;
        public TiempoVenOfertasController (SGContDbContext  context) {
            this.context = context;
        }

        // GET SGCont/TiempoVenOfertas
        [HttpGet]
        public IEnumerable<TiempoVenOferta> GetAll () {
            return context.TiempoVenOfertas.ToList ();
        }

        // GET: SGCont/TiempoVenOfertas/Id
        [HttpGet ("{id}", Name = "GetTiempoVenOferta")]
        public IActionResult GetbyId (int id) {
            var tiempoVenOferta = context.TiempoVenOfertas.FirstOrDefault (s => s.Id == id);

            if (tiempoVenOferta == null) {
                return NotFound ();
            }
            return Ok (tiempoVenOferta);
        }

        // POST SGCont/TiempoVenOfertas
        [HttpPost]
        public IActionResult POST ([FromBody] TiempoVenOferta tiempoVenOferta) {
            if (ModelState.IsValid) {
                var t = context.TiempoVenOfertas.ToList ();
                if (t != null) {
                    foreach (var item in t) {
                        context.TiempoVenOfertas.Remove (item);
                    }
                }
                context.TiempoVenOfertas.Add (tiempoVenOferta);
                context.SaveChanges ();
                return new CreatedAtRouteResult ("GetTiempoVenOferta", new { id = tiempoVenOferta.Id });
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/TiempoVenOfertas/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] TiempoVenOferta tiempoVenOferta, int id) {
            if (tiempoVenOferta.Id != id) {
                return BadRequest (ModelState);

            }
            context.Entry (tiempoVenOferta).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE SGCont/TiempoVenOfertas/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var tiempoVenOferta = context.TiempoVenOfertas.FirstOrDefault (s => s.Id == id);

            if (tiempoVenOferta.Id != id) {
                return NotFound ();
            }
            context.TiempoVenOfertas.Remove (tiempoVenOferta);
            context.SaveChanges ();
            return Ok (tiempoVenOferta);
        }
    }
}