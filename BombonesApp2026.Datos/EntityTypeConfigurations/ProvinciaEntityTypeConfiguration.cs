using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BombonesApp2026.Datos.EntityTypeConfigurations
{
    public class ProvinciaEntityTypeConfiguration : IEntityTypeConfiguration<Provincia>
    {

        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.ToTable("Provincias");
            builder.HasKey(p => p.ProvinciaId);
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(p => p.Nombre, "IX_Provincias_Nombre")
                .IsUnique();
        }
    }
}
