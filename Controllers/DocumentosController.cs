using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers
{
    [Route("SGCont/[controller]")]
    [ApiController]
    public class DocumentosController : Controller
    {
        private readonly SGContDbContext  context;
        public DocumentosController(SGContDbContext  context)
        {
            this.context = context;
        }

        // GET SGCont/Documentos
        [HttpGet]
        public IEnumerable<Documento> GetAll()
        {
            return context.Documentos.ToList();
        }

        // GET: SGCont/Documentos/Id
        [HttpGet("{id}", Name = "GetDocumento")]
        public IActionResult GetbyId(int id)
        {
            var documento = context.Documentos.FirstOrDefault(s => s.Id == id);

            if (documento == null)
            {
                return NotFound();
            }
            return Ok(documento);
        }

        // POST SGCont/Documentos
        [HttpPost]
        public IActionResult POST([FromBody] Documento documento)
        {
            if (ModelState.IsValid)
            {
                context.Documentos.Add(documento);
                context.SaveChanges();
                return new CreatedAtRouteResult("GetDocumento", new { id = documento.Id });
            }
            return BadRequest(ModelState);
        }

        // PUT SGCont/Documentos/id
        [HttpPut("{id}")]
        public IActionResult PUT([FromBody] Documento documento, int id)
        {
            if (documento.Id != id)
            {
                return BadRequest(ModelState);

            }
            context.Entry(documento).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE SGCont/Documentos/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var documento = context.Documentos.FirstOrDefault(s => s.Id == id);

            if (documento.Id != id)
            {
                return NotFound();
            }
            context.Documentos.Remove(documento);
            context.SaveChanges();
            return Ok(documento);
        }
    }
}