using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Blazorcrud.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Noticia> Noticia => Set<Noticia>();
        public DbSet<Categoria> Categoria => Set<Categoria>();
        public DbSet<User> User => Set<User>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BlazorCRUDB; Trusted_Connection=True;");
        //        //optionsBuilder.UseSqlServer("Server=tcp:webapinewsdbserver.database.windows.net,1433;Initial Catalog=NotiBlazor;Persist Security Info=False;User ID=htd7;Password=Esf@cilit0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    }
        //}
    }
}