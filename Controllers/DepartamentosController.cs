﻿using System.Collections.Generic;
using System.Linq;
using SGCont.Data;
using SGCont.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class DepartamentosController : Controller {
        private readonly SGContDbContext  context;
        public DepartamentosController (SGContDbContext  context) {
            this.context = context;
        }
        // GET SGCont/Departamentos
        [HttpGet]
        public IEnumerable<Departamento> GetAll () {
            return context.Departamentos.ToList ();
        }

        // GET: SGCont/Departamentos/Id
        [HttpGet ("{id}", Name = "GetDeparatmentos")]
        public IActionResult GetbyId (int id) {
            var departamento = context.Departamentos.FirstOrDefault (s => s.Id == id);

            if (departamento == null) {
                return NotFound ();
            }
            return Ok (departamento);
        }

        // POST SGCont/Departamentos
        [HttpPost]
        public IActionResult POST ([FromBody] Departamento departamento) {
            if (ModelState.IsValid) {
                context.Departamentos.Add (departamento);
                context.SaveChanges ();
                return new CreatedAtRouteResult ("GetDeparatmentos", new { id = departamento.Id });
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/departamento/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] Departamento departamento, int id) {
            if (departamento.Id != id) {
                return BadRequest (ModelState);

            }
            context.Entry (departamento).State = EntityState.Modified;
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE SGCont/departamento/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var departamento = context.Departamentos.FirstOrDefault (s => s.Id == id);

            if (departamento.Id != id) {
                return NotFound ();
            }
            context.Departamentos.Remove (departamento);
            context.SaveChanges ();
            return Ok (departamento);
        }
    }
}