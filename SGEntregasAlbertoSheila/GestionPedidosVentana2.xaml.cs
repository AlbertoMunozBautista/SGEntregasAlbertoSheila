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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila
{
    /// <summary>
    /// Lógica de interacción para GestionPedidosVentana2.xaml
    /// </summary>
    public partial class GestionPedidosVentana2 : Window
    {
        clientes cli;
        CollectionViewModel cvm;
        public GestionPedidosVentana2(clientes cliente)
        {
            InitializeComponent();
            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            cli = cliente;

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tbCliente.Text = cli.nombre + " " + cli.apellidos;
        }

        private void ejecutaAnadir(object sender, ExecutedRoutedEventArgs e)
        {

            AnadirPedido anadirPedido = new AnadirPedido(cli, cvm);
            anadirPedido.ShowDialog();
        }

        private void compruebaAnadir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ejecutaGuardarBD(object sender, ExecutedRoutedEventArgs e)
        {
            cvm.guardarDatos();
            System.Windows.MessageBox.Show("Se ha guardado en la BBDD", "Éxito");
        }

        private void compruebaGuardarBD(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void ejecutaModificar(object sender, ExecutedRoutedEventArgs e)
        {
            int pos = dgPedidos.SelectedIndex;
            ModificarPedido modificarPedido = new ModificarPedido(cli, cvm.ListaPedidos[pos]);
            modificarPedido.ShowDialog();
            
        }

        private void compruebaModificar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgPedidos.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }
        private void ejecutaEliminar(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult dr = (DialogResult)System.Windows.MessageBox.Show("¿Estás seguro?", "ÉXITO", MessageBoxButton.YesNo);
            int idPedido = cvm.ListaPedidos[dgPedidos.SelectedIndex].id_pedido;
            pedidos objPedido = cvm.objBD.pedidos.Find(idPedido);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                cvm.objBD.pedidos.Remove(objPedido);
                cvm.ListaPedidos.RemoveAt(dgPedidos.SelectedIndex);
                System.Windows.MessageBox.Show("Pedido borrado correctamente", "Exito");
            }

           
        }


        private void compruebaEliminar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgPedidos.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }
    }
}
