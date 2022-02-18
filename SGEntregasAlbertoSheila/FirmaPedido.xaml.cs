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
        DateTime fechaHoy = DateTime.Now;
        pedidos pedido;
        pedidos copiaPedido;
        CollectionViewModel cvm;

        clientes cli;

        byte[] firmaByte;
        public FirmaPedido(pedidos pedido, CollectionViewModel cvm)
        {
            InitializeComponent();

            this.pedido = pedido;

            copiaPedido = (pedidos)pedido.Clone();

            this.DataContext = copiaPedido;

            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            txtFechaEntrega.Text = fechaHoy.ToString("dd/MM/yyyy");


            this.cvm = cvm;

            this.cli = cargarCliente();
        }

        private clientes cargarCliente()
        {
            clientes objCliente = cvm.objBD.clientes.Find(this.pedido.cliente);
            txtCliente.Text = objCliente.apellidos + ", " + objCliente.nombre;

            return objCliente;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            firmaCanvas.Strokes.Clear();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //TODO COMPROBAR FIRMA
            if (firmaCanvas.Strokes.Count > 0) {

                firmaByte = InkCanvasToByte();

                copiaPedido.fecha_entrega = fechaHoy;
                copiaPedido.firma = firmaByte;

                actualizarProperties(copiaPedido, pedido);

                cvm.guardarDatos();

                MessageBox.Show("Guardado en la bbdd correctamente la entrega del pedido");

                //this.cvm.cargarPedidos(cli.dni);

                SegundaTabletVentana segundaTabletVentana = new SegundaTabletVentana(cli.dni);
                segundaTabletVentana.cargarTarjeta();

                this.Close();

                //PrimeraTabletVentana primeraTabletVentana = new PrimeraTabletVentana();
                //primeraTabletVentana.Show();
            } else
            {
                MessageBox.Show("La firma es obligatoria");
            }

            
            /*
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "isf files (*.isf)| *.isf";
            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                firmaCanvas.Strokes.Save(fs);
                fs.Close();
            }*/

          

        }

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
