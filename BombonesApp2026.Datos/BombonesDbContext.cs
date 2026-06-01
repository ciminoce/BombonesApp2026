using BombonesApp2026.Datos.EntityTypeConfigurations;
using BombonesApp2026.Entidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BombonesApp2026.Datos
{
    public class BombonesDbContext:DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoBombon> TipoBombones { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.; Initial Catalog=Bombones2026;
                    Integrated Security=true; TrustServerCertificate=true;");
            optionsBuilder.LogTo(msg=>Debug.WriteLine(msg),LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Rol>(new RolEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<TipoBombon>(new TipoBombonEntityTypeConfiguration());
        }
    }
}
