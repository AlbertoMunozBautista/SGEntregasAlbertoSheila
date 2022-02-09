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
    /// Lógica de interacción para GestionClientesVentana.xaml
    /// </summary>
    public partial class GestionClientesVentana : Window
    {
        CollectionViewModel cvm;
        public GestionClientesVentana()
        {
            InitializeComponent();

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];

            //ScrollBar, si en vertical no en horizontal
            lstClientes.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }


        private void ejecutaAnadir(object sender, ExecutedRoutedEventArgs e)
        {

            AnadirCliente anadirVentana = new AnadirCliente(cvm);
            anadirVentana.ShowDialog();
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
            ModificarCliente modificarCliente = new ModificarCliente(cvm.ListaClientes[lstClientes.SelectedIndex]);
            modificarCliente.Show();
           
        }

        private void compruebaModificar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lstClientes.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }

        private void compruebaEliminar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lstClientes.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }

        private void ejecutaEliminar(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult dr = (DialogResult)System.Windows.MessageBox.Show("¿Estás seguro?", "ÉXITO", MessageBoxButton.YesNo);

            String idCliente = cvm.ListaClientes[lstClientes.SelectedIndex].dni;

            clientes objCliente = cvm.objBD.clientes.Find(idCliente);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                while (objCliente.pedidos.Count > 0)
                {
                    var pedido = (pedidos)objCliente.pedidos.First();
                    cvm.objBD.pedidos.Remove(pedido);
                }

                cvm.ListaClientes.RemoveAt(lstClientes.SelectedIndex);
                cvm.objBD.clientes.Remove(objCliente);
            }
        }

    }
}
