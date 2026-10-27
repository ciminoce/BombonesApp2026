# Cierre por inactividad con panel inferior

## Comportamiento

Todos los 27 formularios secundarios tienen un panel inferior propio para el aviso.
El principal y el login no tienen cierre por inactividad.

- Plazo: 5 minutos por diálogo; aviso con cuenta regresiva los últimos 30 segundos.
- Teclado, clics y rueda reinician el plazo y ocultan el mensaje. Mover el mouse no cuenta.
- Se cancela el diálogo más interno y el anterior recibe un plazo completo.
- No se guardan cambios sin confirmar. No se deshacen operaciones ya guardadas.
- Al terminar la grilla, los `using` existentes liberan el formulario y su scope.
- Un MessageBox abierto pausa el cierre hasta que se responda.
- Las operaciones sincrónicas no se interrumpen mientras ocupan el hilo de interfaz.

## Diseño

Cada Designer contiene un TableLayoutPanel con dos filas: pnlContenido y
pnlAvisoInactividad. La segunda reserva 96 unidades de diseño de alto.
Su espacio permanece reservado aunque no haya aviso, evitando que se muevan
los controles al aparecer o desaparecer el mensaje. El controlador agrega una
etiqueta Dock.Fill dentro del panel; ya no hay una franja superpuesta.

Se conservaron las coordenadas originales de campos, botones y agrupaciones.
Se ampliaron las ventanas y sus límites cuando correspondía. En los formularios
altos de cajas el contenido tiene desplazamiento para acceder a todos los campos
y botones. La paginación de las grillas conserva un panel de altura fija.

Los formularios que aún no tienen operaciones implementadas también tienen el
panel preparado. Esto no agrega funcionalidades ni conexiones de menú nuevas:
el contador se activa al abrirlos mediante MostrarConInactividad(this).

## Incorporar sobre la versión anterior

Reemplazar `BombonesApp2026.Windows/Helpers/ControlInactividad.cs` y los 27
archivos Designer indicados al final. No copiar solamente el helper: requiere
el panel en cada formulario que se abra con MostrarConInactividad.
El ZIP incluye la solución completa y `Cambios_Inactividad.patch` con las
diferencias respecto al ZIP original.

Para futuros diálogos, preparar ambas filas y nombrar el panel inferior
`pnlAvisoInactividad`. Abrir con `MostrarConInactividad(this)` dentro del `using`
del formulario y continuar guardando solamente si devuelve DialogResult.OK.
No se requieren registros nuevos en DI.

## Prueba rápida en Windows

En ControlInactividad.cs cambiar temporalmente TiempoLimite a
TimeSpan.FromSeconds(40): el aviso aparecerá a los 10 segundos, con 30 restantes.
Restaurar TimeSpan.FromMinutes(5) al terminar.

1. Compilar la solución (conserva net10.0-windows).
2. Abrir cada grilla, alta/edición y detalle: verificar campos, botones, paginación
   y el aviso completo. Comprobar también escalas de pantalla de 125% y 150%.
3. Mover solamente el mouse: el aviso debe continuar y el formulario cancelarse.
4. Con el aviso visible, pulsar una tecla, hacer clic o usar la rueda: debe ocultarse
   y empezar otro plazo, sin mover los botones.
5. Dejar un alta/edición con datos sin confirmar: no debe persistirlos.
6. Probar Cajas -> Alta/Edición -> Agregar bombón: cierre de un diálogo por plazo.
7. Confirmar y cancelar operaciones normalmente; reabrirlas varias veces.
8. Dejar un MessageBox abierto: no debe cerrarse su propietario por detrás.
9. En Cajas, comprobar que el desplazamiento permite llegar a OK/Cancelar.

Verificación realizada: comparación estática de los 27 diseños contra el original,
conservación de los controles y sus coordenadas, y de las 23 aperturas existentes.
No se compiló ni ejecutó WinForms: el entorno de preparación no tiene SDK .NET
ni Windows. Las pruebas funcionales y visuales anteriores quedan pendientes.

## Designer actualizados

- frmBombonAe.Designer.cs
- frmBombones.Designer.cs
- frmCajaAe.Designer.cs
- frmCajas.Designer.cs
- frmCiudadAe.Designer.cs
- frmCiudades.Designer.cs
- frmClienteAe.Designer.cs
- frmClientes.Designer.cs
- frmDetalleBombon.Designer.cs
- frmDetalleRol.Designer.cs
- frmDetallesCaja.Designer.cs
- frmEmpleadoAe.Designer.cs
- frmEmpleados.Designer.cs
- frmFormaDePagoAe.Designer.cs
- frmFormasDePago.Designer.cs
- frmManejarBombonCajaAe.Designer.cs
- frmProvinciaAe.Designer.cs
- frmProvincias.Designer.cs
- frmRoles.Designer.cs
- frmRolesAe.Designer.cs
- frmTipoDeBombonesAe.Designer.cs
- frmTiposDeBombones.Designer.cs
- frmTransporteAe.Designer.cs
- frmTransportes.Designer.cs
- frmUsuarioAe.Designer.cs
- frmUsuarios.Designer.cs
- frmVentas.Designer.cs
