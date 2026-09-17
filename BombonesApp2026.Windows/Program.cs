using Bombones2026.Servicios.Servicios;
using BombonesApp2026.Datos;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Servicios.Servicios;
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
            var tipoBombonRepositorio = new TipoBombonRepositorio(context);
            var formaDePagoRepositorio = new FormaDePagoRepositorio(context);
            var rolRepositorio = new RolRepositorio(context);
            var provinciaRepositorio = new ProvinciaRepositorio(context);
            var ciudadRepositorio = new CiudadRepositorio(context);
            var transporteRepositorio = new TransporteRepositorio(context);
            var bombonRepositorio = new BombonRepositorio(context);

            var tipoBombonServicio = new TipoBombonServicio(tipoBombonRepositorio);
            var formaDePagoServicio = new FormaDePagoServicio(formaDePagoRepositorio);
            var rolServicio = new RolServicio(rolRepositorio);
            var provinciaServicio = new ProvinciaServicio(provinciaRepositorio);
            var ciudadServicio = new CiudadServicio(ciudadRepositorio);
            var transporteServicio = new TransporteServicio(transporteRepositorio);
            var bombonServicio = new BombonServicio(bombonRepositorio);
            Application.Run(new frmPrincipal(tipoBombonRepositorio,
                formaDePagoRepositorio,
                rolRepositorio,
                provinciaRepositorio,
                ciudadRepositorio,
                transporteRepositorio,
                bombonRepositorio,
                tipoBombonServicio,
                formaDePagoServicio,
                rolServicio,
                provinciaServicio,
                ciudadServicio,
                transporteServicio,
                bombonServicio));
        }
    }
}