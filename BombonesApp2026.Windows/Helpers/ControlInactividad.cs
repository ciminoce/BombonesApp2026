using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BombonesApp2026.Windows.Helpers
{
    /// <summary>
    /// Abre un diálogo con cierre por inactividad. Usar siempre en el hilo de UI.
    /// El formulario y su scope siguen siendo responsabilidad del llamador.
    /// </summary>
    public static class ControlInactividad
    {
        // Cambiar solamente este valor para ajustar todos los formularios.
        public static TimeSpan TiempoLimite { get; set; } = TimeSpan.FromSeconds(15);

        public static DialogResult MostrarConInactividad(this Form formulario,
            IWin32Window propietario)
        {
            if (TiempoLimite <= TimeSpan.Zero)
                throw new InvalidOperationException("El tiempo de inactividad debe ser positivo.");

            using var control = new Vigilancia(formulario, TiempoLimite);
            return formulario.ShowDialog(propietario);
        }

        private sealed class Vigilancia : IMessageFilter, IDisposable
        {
            // Cada ShowDialog anidado suspende la vigilancia del anterior.
            [ThreadStatic]
            private static Vigilancia? _actual;

            private readonly Vigilancia? _anterior;
            private readonly Form _formulario;
            private readonly TimeSpan _limite;
            private readonly Stopwatch _reloj = Stopwatch.StartNew();
            private readonly System.Windows.Forms.Timer _timer;
            private readonly Label _aviso;
            private readonly Control _contenedorAviso;
            private static readonly TimeSpan AnticipacionAviso = TimeSpan.FromSeconds(30);
            private bool _bloqueado;
            private bool _disposed;

            public Vigilancia(Form formulario, TimeSpan limite)
            {
                _formulario = formulario;
                _limite = limite;
                // Cada diálogo reserva una fila propia para el aviso.
                _contenedorAviso = _formulario.Controls
                    .Find("pnlAvisoInactividad", true).OfType<Panel>().FirstOrDefault()
                    ?? throw new InvalidOperationException(
                        $"El formulario {formulario.Name} necesita el panel pnlAvisoInactividad.");
                _anterior = _actual;
                _actual = this;
                _aviso = new Label
                {
                    Name = "lblAvisoInactividad",
                    AutoSize = false,
                    BackColor = Color.FromArgb(255, 243, 184),
                    ForeColor = Color.FromArgb(75, 52, 0),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleLeft,
                    UseMnemonic = false,
                    TabStop = false,
                    Visible = false,
                    AccessibleName = "Aviso de cierre por inactividad"
                };
                _aviso.Dock = DockStyle.Fill;
                _contenedorAviso.Controls.Add(_aviso);
                _contenedorAviso.ClientSizeChanged += AjustarAviso;
                _anterior?.Reiniciar();
                _formulario.Shown += AlMostrar;
                Application.AddMessageFilter(this);
                _timer = new System.Windows.Forms.Timer { Interval = 500 };
                _timer.Tick += ComprobarInactividad;
                _timer.Start();
            }

            private void AlMostrar(object? sender, EventArgs e) => Reiniciar();

            private void Reiniciar()
            {
                if (_disposed) return;
                _reloj.Restart();
                _aviso.Visible = false;
            }

            private void MostrarAviso(TimeSpan restante)
            {
                int segundos = Math.Max(1, (int)Math.Ceiling(restante.TotalSeconds));
                string texto = $"Este formulario se cerrará automáticamente en {segundos} " +
                    (segundos == 1 ? "segundo" : "segundos") +
                    " por inactividad.\nLos cambios sin confirmar se descartarán. " +
                    "Presioná una tecla o hacé clic para continuar.";
                if (_aviso.Text != texto)
                {
                    _aviso.Text = texto;
                    AjustarAviso(this, EventArgs.Empty);
                }
                if (!_aviso.Visible)
                {
                    _aviso.BringToFront();
                    _aviso.Visible = true;
                }
            }

            private void AjustarAviso(object? sender, EventArgs e)
            {
                if (_aviso.IsDisposed || _formulario.IsDisposed) return;
                int margen = Math.Max(8, (int)(12 * _formulario.DeviceDpi / 96f));
                _aviso.Padding = new Padding(margen);
                // Dock.Fill limita el mensaje al panel reservado por el diseñador.
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (ReferenceEquals(_actual, this) && EsActividad(m.Msg))
                    Reiniciar();

                // Observar el mensaje sin impedir que llegue al control.
                return false;
            }

            private static bool EsActividad(int mensaje)
            {
                return mensaje switch
                {
                    0x0100 or 0x0101 => true, // WM_KEYDOWN / WM_KEYUP
                    0x0104 or 0x0105 => true, // WM_SYSKEYDOWN / WM_SYSKEYUP (Alt)
                    0x0201 or 0x0202 or 0x0203 => true, // Botón izquierdo
                    0x0204 or 0x0205 or 0x0206 => true, // Botón derecho
                    0x0207 or 0x0208 or 0x0209 => true, // Botón central
                    0x020B or 0x020C or 0x020D => true, // Botones adicionales
                    0x020A or 0x020E => true, // Rueda vertical / horizontal
                    0x00A1 or 0x00A2 or 0x00A3 => true, // Clic izquierdo en marco/título
                    0x00A4 or 0x00A5 or 0x00A6 => true, // Clic derecho en marco/título
                    0x00A7 or 0x00A8 or 0x00A9 => true, // Clic central en marco/título
                    0x00AB or 0x00AC or 0x00AD => true, // Botones adicionales en marco
                    // WM_MOUSEMOVE, WM_NCMOUSEMOVE, hover y paint NO cuentan.
                    _ => false
                };
            }

            private void ComprobarInactividad(object? sender, EventArgs e)
            {
                if (!ReferenceEquals(_actual, this) || _disposed ||
                    _formulario.IsDisposed || !_formulario.Visible ||
                    !_formulario.IsHandleCreated)
                    return;

                // Un MessageBox nativo puede deshabilitar la ventana sin cambiar
                // el estado administrado. No cerrar su propietario por detrás.
                if (!IsWindowEnabled(_formulario.Handle))
                {
                    _bloqueado = true;
                    Reiniciar();
                    return;
                }

                if (_bloqueado)
                {
                    _bloqueado = false;
                    Reiniciar();
                    return;
                }

                if (_formulario.DialogResult != DialogResult.None) return;

                TimeSpan restante = _limite - _reloj.Elapsed;
                if (restante > TimeSpan.Zero)
                {
                    if (restante <= AnticipacionAviso) MostrarAviso(restante);
                    return;
                }

                Reiniciar();
                // ShowDialog termina con Cancel. No ejecutar OK ni guardar.
                // Los using existentes disponen el formulario y, al terminar
                // la grilla, también el scope y su DbContext.
                _formulario.DialogResult = DialogResult.Cancel;
            }

            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                _timer.Stop();
                _timer.Tick -= ComprobarInactividad;
                _timer.Dispose();
                Application.RemoveMessageFilter(this);
                _formulario.Shown -= AlMostrar;
                _contenedorAviso.ClientSizeChanged -= AjustarAviso;
                if (!_contenedorAviso.IsDisposed) _contenedorAviso.Controls.Remove(_aviso);
                _aviso.Dispose();
                _reloj.Stop();

                _actual = _anterior;
                // El padre recibe un período completo, aunque ya llevara
                // varios minutos abierto antes de entrar al hijo.
                _anterior?.Reiniciar();
            }

            [DllImport("user32.dll", ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool IsWindowEnabled(IntPtr hWnd);
        }
    }
}
