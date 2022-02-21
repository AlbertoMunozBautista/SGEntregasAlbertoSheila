using SGEntregasAlbertoSheila.ViewModel;
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila
{
    /// <summary>
    /// Lógica de interacción para FirmaPedido.xaml
    /// </summary>
    public partial class FirmaPedido : Window
    {
        //Declaracion de variables
        DateTime fechaHoy = DateTime.Now;
        pedidos pedido;
        pedidos copiaPedido;
        CollectionViewModel cvm;

        SegundaTabletVentana ventanaPedidos;

        clientes cli;

        byte[] firmaByte;

        //Constructor. Recibimos un pedido, el collectionviewmodel y la ventana de pedidos
        public FirmaPedido(pedidos pedido, CollectionViewModel cvm, SegundaTabletVentana ventanaPedidos)
        {
            InitializeComponent();

            //Debemos tener otra variable con un pedido clonado y asignarlo al DataContext para hacer modificaciones
            this.pedido = pedido;
            copiaPedido = (pedidos)pedido.Clone();
            this.DataContext = copiaPedido;

            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Metemos la fecha de hoy automaticamente en el campo fecha de entrega de la vetana
            txtFechaEntrega.Text = fechaHoy.ToString("dd/MM/yyyy");

            this.ventanaPedidos = ventanaPedidos;
            this.cvm = cvm;
            //Usamos la clase 'clientes' para usar el metodo de cargarCliente()
            this.cli = cargarCliente();
        }

        private clientes cargarCliente()
        {
            //Hacemos una consulta en los clientes donde buscamos el cliente que este enlazado con el pedido que hemos seleccionado
            clientes objCliente = cvm.objBD.clientes.Find(this.pedido.cliente);
            //De este consulta obtenemos el apellido y el nombre, que es lo que vamos a mostrar en esta ventana como información
            txtCliente.Text = objCliente.apellidos + ", " + objCliente.nombre;

            return objCliente;
        }

        //Se cierra la ventana si pulsamos en el boton cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Metodo que limpia el cuadro de la firma si pulsamos el boton 'Limpiar'
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            firmaCanvas.Strokes.Clear();
        }

        //Metodo que cuando pulsamos el boton aceptar
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Comprueba que el cuadro de firma no este vacio
            if (firmaCanvas.Strokes.Count > 0) {

                //Llamamos al metodo InkCanvasToByte y lo recogemos en un array de bytes (firmaByte)
                firmaByte = InkCanvasToByte();

                //Metemos la fecha de hoy y la firma en el objeto clonado del pedido (copiaPedido)
                copiaPedido.fecha_entrega = fechaHoy;
                copiaPedido.firma = firmaByte;

                //Metodo dnd le pasamos la copia(lo que hemos modificado) y el pedido(que habiamos recibido en principio)
                actualizarProperties(copiaPedido, pedido);

                //Llamamos al metodo guardarDatos() del CollectionViewModel para guardar en la bbdd los cambios
                cvm.guardarDatos();

                MessageBox.Show("Guardado en la bbdd correctamente la entrega del pedido");

                //Volvemos a llamar a cargarTarjeta() de la ventana Pedidos. Ahi es vuelven a cargar los pedidos
                //y se muestran solo los pedidos que no estén firmados.
                ventanaPedidos.cargarTarjeta();

                this.Close();//Cerramos la ventana
            } 
            //Si la firma no está rellena mostramos un mensaje de informacion
            else
            {
                MessageBox.Show("La firma es obligatoria");
            }
        }

        //Metodo que pasa la firma del InkCanvas a un array de byres
        private byte[] InkCanvasToByte()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (firmaCanvas.Strokes.Count > 0)
                {
                    firmaCanvas.Strokes.Save(ms, true);
                    byte[] unencryptedSignature = ms.ToArray();
                    return unencryptedSignature;
                }
                else
                {
                    return null;
                }
            }
        }

        //Metodo que actualiza el pedido con los cambios realizados
        //Pasamos los datos de pedidoOrigen(copiaPedido, lo que hemos modificado) a pedidoDestino(pedido, que vamos a guardar en la bbbdd modificado)
        private void actualizarProperties(pedidos pedidoOrigen, pedidos pedidoDestino)
        {
            pedidoDestino.cliente = pedidoOrigen.cliente;
            pedidoDestino.fecha_pedido = pedidoOrigen.fecha_pedido;
            pedidoDestino.descripcion = pedidoOrigen.descripcion;
            pedidoDestino.fecha_entrega = pedidoOrigen.fecha_entrega;
            pedidoDestino.firma = pedidoOrigen.firma;
        }

    }
}
