using Microsoft.EntityFrameworkCore;
using Inteia.Api.Core;
using Inteia.Api.Models;

namespace Inteia.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        // Entidades existentes
        public DbSet<Evento> Eventos => Set<Evento>();
        public DbSet<Vinculador> Vinculadores => Set<Vinculador>();

        // 🔹 Nuevas entidades para Portfolio y relaciones
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<TipoDeApoyo> TiposDeApoyo => Set<TipoDeApoyo>();
        public DbSet<Objetivo> Objetivos => Set<Objetivo>();
        public DbSet<ObjPorcentaje> ObjPorcentajes => Set<ObjPorcentaje>();
        public DbSet<Recurso> Recursos => Set<Recurso>();
        public DbSet<Instrumento> Instrumentos => Set<Instrumento>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔒 Restricción de unicidad
            modelBuilder.Entity<Portfolio>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

            // 🔁 Relaciones entre entidades
            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.Objetivos)
                .WithOne(o => o.Portfolio)
                .HasForeignKey(o => o.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.Recursos)
                .WithOne(r => r.Portfolio)
                .HasForeignKey(r => r.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.Instrumentos)
                .WithOne(i => i.Portfolio)
                .HasForeignKey(i => i.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Objetivo>()
                .HasOne(o => o.ObjPorcentaje)
                .WithOne(p => p.Objetivo)
                .HasForeignKey<ObjPorcentaje>(p => p.ObjetivoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}