using Bombones2026.Servicios.Servicios;
using BombonesApp2026.Servicios.DTOs.Bombon;

namespace BombonesApp2026.Windows
{
    public partial class frmBombonAe : Form
    {
        private readonly TipoBombonServicio _tipoServicio;
        public frmBombonAe(TipoBombonServicio tipoServicio)
        {
            InitializeComponent();
            _tipoServicio = tipoServicio;
        }

        internal BombonEditDto? GetBombon()
        {
            throw new NotImplementedException();
        }

        internal void SetBombon(BombonEditDto bombonEditDto)
        {
            throw new NotImplementedException();
        }
    }
}
