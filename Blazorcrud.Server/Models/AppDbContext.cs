using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Noticia> Noticia => Set<Noticia>();
        public DbSet<Categoria> Categoria => Set<Categoria>();
        public DbSet<User> Users => Set<User>();
    }
}