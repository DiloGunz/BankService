using ClientMgmt.Domain.Entities;
using ClientMgmt.Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClientMgmt.Infraestructure.Persistence;

public class ClientMgmtContext : DbContext
{
    public ClientMgmtContext(DbContextOptions options) : base(options) { }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new PersonaConfiguration());
    }
}
