using AdministratioSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdministratioSchool.Infraestructure.Persistence.Contex
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
