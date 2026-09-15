using BombonesApp2026.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BombonesApp2026.Windows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IConfiguration configuration =
                new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile(
                        "appsettings.json",
                        optional: false,
                        reloadOnChange: true)
                    .Build();

            string connectionString =
                configuration.GetConnectionString("Bombones")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión.");

            var options =
                new DbContextOptionsBuilder<BombonesDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            var context =
                new BombonesDbContext(options);
            //instanciar repositorios y servicios aquí si es necesario
            Application.Run(new frmPrincipal());
        }
    }
}