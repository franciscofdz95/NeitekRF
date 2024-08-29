using Microsoft.EntityFrameworkCore;
using NeitekRF.API.Models;

namespace NeitekRF.API.Data;

public class RfContext(DbContextOptions<RfContext> options) : DbContext(options)
{
    public DbSet<Meta> Metas { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Meta>().HasQueryFilter(m => m.IsActive);
    }
}
