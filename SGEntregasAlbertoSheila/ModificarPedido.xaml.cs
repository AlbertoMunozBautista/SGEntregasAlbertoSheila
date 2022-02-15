using SGEntregasAlbertoSheila.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila
{
    /// <summary>
    /// Lógica de interacción para ModificarPedido.xaml
    /// </summary>
    public partial class ModificarPedido : Window
    {
        CollectionViewModel cvm;
        clientes cli;
        //variables para moder modificar
        private pedidos pedido;
        private pedidos copPedido;

        //Constructor recibe el cliente al que pertenece el pedido, y el pedido (mismo) que vamos a modificar
        public ModificarPedido(clientes cli, pedidos pedido)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];

            //pedido y copia medico que asignamos al DataContext
            this.pedido = pedido;
            copPedido = (pedidos)pedido.Clone();
            this.DataContext = copPedido;

            this.cli = cli;

            cargarDatos();

        }

        private void cargarDatos()
        {
            txtCliente.Text = cli.nombre;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Si la descripcion no está vacía
            if(tbDescripcion.Text.Trim() != "")
            {
                actualizarProperties(copPedido, pedido);
                this.Close();
                MessageBox.Show("Pedido modificado correctamente");
            }
        }

        //Actualizamos los datos de la copia al pedido
        private void actualizarProperties(pedidos pedidoOrigen, pedidos pedidoDestino)
        {
            pedidoDestino.cliente = pedidoOrigen.cliente;
            pedidoDestino.fecha_pedido = pedidoOrigen.fecha_pedido;
            pedidoDestino.descripcion = pedidoOrigen.descripcion;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
