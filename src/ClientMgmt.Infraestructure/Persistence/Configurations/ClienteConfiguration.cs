using ClientMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMgmt.Infraestructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente", "client_mgmt");

        builder.HasOne(c => c.Persona)
            .WithOne(p => p.Cliente)
            .HasForeignKey<Cliente>(c => c.PersonaId);
    }
}