using AutoresWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoresWebApi.Contexts
{
    public class AplicationDbContext : DbContext
    {
        //constructor de la clase al cual le paso un objeto aplicationdbcontext options que a su vez 
        //se lo paso al constructor de la clase base dbContext
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
