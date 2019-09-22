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
    public class AutoresController : ControllerBase

    {
        private readonly AplicationDbContext context;

        public AutoresController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        //action result entidad, devuelve un action result error 404, etc; o una entidad  
        {
            return context.Autores.ToList();
        }
        [HttpGet("{id}", Name = "ObtenerAutor")]//inidcamos que el id viene en la url
        public ActionResult<Autor> Get(int Id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.id == Id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        [HttpPost]
        public ActionResult Post([FromBody]Autor autor)
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { Id = autor.id }, autor);
        }
        //actualizar
        [HttpPut("{id}")]//inidcamos que el id viene en la url
        public ActionResult Put(int id, [FromBody]Autor value)
        {
            if (id != value.id) //si no lo encuentra
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }
        //borramos autor
        [HttpDelete("{id}")]//actualizar
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.id == id);//ubicamos por id en la BD
            if (autor == null) //si no lo encuentra
            {
                return NotFound();
            }

            context.Autores.Remove(autor);// si lo encuentra lo borra
            context.SaveChanges();
            return autor;

        }
    }
}