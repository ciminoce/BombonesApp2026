using BombonesApp2026.Entidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BombonesApp2026.Datos.EntityTypeConfigurations
{
    public class TipoBombonEntityTypeConfiguration : IEntityTypeConfiguration<TipoBombon>
    {
        public void Configure(EntityTypeBuilder<TipoBombon> builder)
        {
            builder.ToTable("TipoBombones");
            builder.HasKey(tb => tb.TipoBombonId);
            builder.Property(tb => tb.Nombre).HasMaxLength(50).IsRequired();
            builder.Property(tb => tb.Descripcion).HasMaxLength(150);
            builder.HasIndex(tb => tb.Nombre, "IX_TipoBombones_Nombre").IsUnique();
            builder.Property(tb => tb.RowVersion).IsRowVersion();
        }
    }
}
