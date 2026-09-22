using Bombones2026.Servicios.Servicios;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmPrincipal : Form
    {
        private readonly TipoBombonRepositorio _tipoBombonRepositorio;
        private readonly FormaDePagoRepositorio _formaDePagoRepositorio;
        private readonly RolRepositorio _rolRepositorio;
        private readonly ProvinciaRepositorio _provinciaRepositorio;
        private readonly CiudadRepositorio _ciudadRepositorio;
        private readonly TransporteRepositorio _transporteRepositorio;
        private readonly BombonRepositorio _bombonRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly TipoBombonServicio _tipoBombonServicio;
        private readonly FormaDePagoServicio _formaDePagoServicio;
        private readonly RolServicio _rolServicio;
        private readonly ProvinciaServicio _provinciaServicio;
        private readonly CiudadServicio _ciudadServicio;
        private readonly TransporteServicio _transporteServicio;
        private readonly BombonServicio _bombonServicio;
        private readonly ClienteServicio _clienteServicio;
        public frmPrincipal(TipoBombonRepositorio tipoBombonRepositorio,
            FormaDePagoRepositorio formaDePagoRepositorio,
            RolRepositorio rolRepositorio,
            ProvinciaRepositorio provinciaRepositorio,
            CiudadRepositorio ciudadRepositorio,
            TransporteRepositorio transporteRepositorio,
            BombonRepositorio bombonRepositorio,
            ClienteRepositorio clienteRepositorio,
            TipoBombonServicio tipoBombonServicio,

            FormaDePagoServicio formaDePagoServicio,
            RolServicio rolServicio,
            ProvinciaServicio provinciaServicio,
            CiudadServicio ciudadServicio,
            TransporteServicio transporteServicio,
            BombonServicio bombonServicio,
            ClienteServicio clienteServicio)
        {
            InitializeComponent();
            _tipoBombonRepositorio = tipoBombonRepositorio;
            _formaDePagoRepositorio = formaDePagoRepositorio;
            _rolRepositorio = rolRepositorio;
            _provinciaRepositorio = provinciaRepositorio;
            _ciudadRepositorio = ciudadRepositorio;
            _transporteRepositorio = transporteRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _tipoBombonServicio = tipoBombonServicio;
            _formaDePagoServicio = formaDePagoServicio;
            _rolServicio = rolServicio;
            _provinciaServicio = provinciaServicio;
            _ciudadServicio = ciudadServicio;
            _transporteServicio = transporteServicio;
            _bombonServicio = bombonServicio;
            _clienteServicio = clienteServicio;
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            using (frmRoles frm = new frmRoles(_rolServicio) { Text = "Listado de Roles" })
            {
                frm.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTiposBombones_Click(object sender, EventArgs e)
        {
            using (frmTiposDeBombones frm = new frmTiposDeBombones(_tipoBombonServicio) { Text = "Listado de Tipos de Bombones" })
            {
                frm.ShowDialog();
            }

        }

        private void btnFormaDePago_Click(object sender, EventArgs e)
        {
            using (frmFormasDePago frm = new frmFormasDePago(_formaDePagoServicio) { Text = "Listado de Formas de Pago" })
            {
                frm.ShowDialog();
            }

        }

        private void btnProvincias_Click(object sender, EventArgs e)
        {
            using (frmProvincias frm = new frmProvincias(_provinciaServicio) { Text = "Listado de Provincias" })
            {
                frm.ShowDialog();
            }
        }

        private void btnCiudades_Click(object sender, EventArgs e)
        {
            using (frmCiudades frm = new frmCiudades(_ciudadServicio, _provinciaServicio) { Text = "Listado de Ciudades" })
            {
                frm.ShowDialog();
            }
        }

        private void btnTransportes_Click(object sender, EventArgs e)
        {
            using (frmTransportes frm = new frmTransportes(_transporteServicio, _provinciaServicio) { Text = "Listado de Transportes" })
            {
                frm.ShowDialog();
            }
        }

        private void btnBombones_Click(object sender, EventArgs e)
        {
            using (frmBombones frm = new frmBombones(_bombonServicio, _tipoBombonServicio) { Text = "Listado de Bombones" })
            {
                frm.ShowDialog();

            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            using (frmClientes frm = new frmClientes(_clienteServicio, _provinciaServicio, _ciudadServicio                                                                                                           ) { Text = "Listado de Clientes" })
            {
                frm.ShowDialog();
            }
        }
    }
}
