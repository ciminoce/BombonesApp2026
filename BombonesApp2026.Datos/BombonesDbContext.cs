using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BombonesApp2026.Datos
{
    public class BombonesDbContext:DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.; Initial Catalog=Bombones2026;
                    Integrated Security=true; TrustServerCertificate=true;");
            optionsBuilder.LogTo(msg=>Debug.WriteLine(msg),LogLevel.Information);
        }
    }
}
