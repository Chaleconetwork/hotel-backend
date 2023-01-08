using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Rol> Rols { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
