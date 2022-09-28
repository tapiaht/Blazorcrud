using System;
using System.Collections.Generic;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Blazorcrud.Server.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //public DbSet<Noticia> Noticia => Set<Noticia>();
        //public DbSet<Categoria> Categoria => Set<Categoria>();
        //public DbSet<User> User => Set<User>();

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Noticia> Noticia { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BlazorCRUDB; Trusted_Connection=True;");
        //        //optionsBuilder.UseSqlServer("Server=tcp:webapinewsdbserver.database.windows.net,1433;Initial Catalog=NotiBlazor;Persist Security Info=False;User ID=htd7;Password=Esf@cilit0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Foto).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => e.NoticiaId)
                    .HasName("PK_News");

                entity.Property(e => e.Body).HasMaxLength(700);

                entity.Property(e => e.Titulo).HasMaxLength(500);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Noticia_Categoria");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    }
}