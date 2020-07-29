using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class FormasDePagosController : Controller {
        private readonly SGContDbContext  context;
        public FormasDePagosController (SGContDbContext  context) {
            this.context = context;
        }
        // GET SGCont/FormasDePagos
        [HttpGet]
        public IActionResult GetAll () {
            var formasDePagos = new List<dynamic> () {
                new { Id = FormaDePago.Transferencia_Bancaria, Nombre = "Transferencia Bancaria" },
                new { Id = FormaDePago.Cheque_Bancario, Nombre = "Cheque Bancario" },
                new { Id = FormaDePago.Efectivo, Nombre = "Efectivo"},
            };
            return Ok (formasDePagos);
        }

        //     // GET SGCont/FormasDePagos
        //     [HttpGet]
        //     public IEnumerable<FormaDePago> GetAll()
        //     {
        //         return context.FormasDePagos.ToList();
        //     }

        //     // GET: SGCont/FormasDePagos/Id
        //     [HttpGet("{id}", Name = "GetformaDePago")]
        //     public IActionResult GetbyId(int id)
        //     {
        //         var formaDePago = context.FormasDePagos.FirstOrDefault(s => s.Id == id);

        //         if (formaDePago == null)
        //         {
        //             return NotFound();
        //         }
        //         return Ok(formaDePago);
        //     }

        //     // POST SGCont/FormasDePagos
        //     [HttpPost]
        //     public IActionResult POST([FromBody] FormaDePago formaDePago)
        //     {
        //         if (ModelState.IsValid)
        //         {
        //             context.FormasDePagos.Add(formaDePago);
        //             context.SaveChanges();
        //             return new CreatedAtRouteResult("GetformaDePago", new { id = formaDePago.Id });
        //         }
        //         return BadRequest(ModelState);
        //     }

        //     // PUT SGCont/formaDePago/id
        //     [HttpPut("{id}")]
        //     public IActionResult PUT([FromBody] FormaDePago formaDePago, int id)
        //     {
        //         if (formaDePago.Id != id)
        //         {
        //             return BadRequest(ModelState);

        //         }
        //         context.Entry(formaDePago).State = EntityState.Modified;
        //         context.SaveChanges();
        //         return Ok();
        //     }

        //     // DELETE SGCont/formaDePago/id
        //     [HttpDelete("{id}")]
        //     public IActionResult Delete(int id)
        //     {
        //         var formaDePago = context.FormasDePagos.FirstOrDefault(s => s.Id == id);

        //         if (formaDePago.Id != id)
        //         {
        //             return NotFound();
        //         }
        //         context.FormasDePagos.Remove(formaDePago);
        //         context.SaveChanges();
        //         return Ok(formaDePago);
        //     }
    }
}