namespace BombonesApp2026.Servicios.Excepciones
{
    public class ValidarVentaExcepcion : Exception
    {
        public List<string> Errores { get; }
        public ValidarVentaExcepcion(List<string> errores)
        {
            Errores = errores;
        }

    }
}
