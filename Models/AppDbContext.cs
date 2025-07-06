using Microsoft.EntityFrameworkCore;
using Inteia.Api.Core;
using Inteia.Api.Models;

namespace Inteia.Api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Vinculador> Vinculadores => Set<Vinculador>();
}
