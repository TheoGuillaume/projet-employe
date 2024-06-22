using Crud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class RazorPageDbContext : DbContext
    {
        public RazorPageDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Poste> Poste {get; set;}
   
    }
}