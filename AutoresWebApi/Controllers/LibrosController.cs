using AutoresWebApi.Contexts;
using AutoresWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly AplicationDbContext context;

        public LibrosController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        //action result entidad, devuelve un action result error 404, etc; o una entidad  
        {
            return context.Libros.Include(x => x.Autor).ToList();
        }
        [HttpGet("{id}", Name = "ObtenerLibro")]//inidcamos que el id viene en la url
        public ActionResult<Libro> Get(int Id)
        {
            var Libro = context.Libros.Include(x=> x.Autor).FirstOrDefault(x => x.Id == Id);
            if (Libro == null)
            {
                return NotFound();
            }
            return Libro;
        }
        [HttpPost]
        public ActionResult Post([FromBody]Libro Libro)
        {
            context.Libros.Add(Libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { Id = Libro.Id }, Libro);
        }
        //actualizar
        [HttpPut("{id}")]//inidcamos que el id viene en la url
        public ActionResult Put(int id, [FromBody]Libro value)
        {
            if (id != value.Id) //si no lo encuentra
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]//actualizar
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.Id == id);//ubicamos por id en la BD
            if (libro == null) //si no lo encuentra
            {
                return NotFound();
            }

            context.Libros.Remove(libro);// si lo en|||cuentra lo borra
            context.SaveChanges();
            return libro;

        }
    }
}
