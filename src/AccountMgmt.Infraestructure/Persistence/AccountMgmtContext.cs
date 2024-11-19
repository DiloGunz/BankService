using AccountMgmt.Domain.Entities;
using AccountMgmt.Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AccountMgmt.Infraestructure.Persistence;

public class AccountMgmtContext : DbContext
{
    public AccountMgmtContext(DbContextOptions options) : base(options) { }

    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CuentaConfiguration());
        modelBuilder.ApplyConfiguration(new MovimientoConfiguration());
    }
}