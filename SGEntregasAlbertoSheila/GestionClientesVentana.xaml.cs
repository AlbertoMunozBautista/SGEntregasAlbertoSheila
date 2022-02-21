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
        //Declaracion de variables
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

        //Si pulsamos 'Añadir' abrimos la ventana AñadirCliente (le pasamos el CollectionViewModel)
        private void ejecutaAnadir(object sender, ExecutedRoutedEventArgs e)
        {

            AnadirCliente anadirVentana = new AnadirCliente(cvm);
            anadirVentana.ShowDialog();
        }

        //Metodo que llama al ejecutaAñadir()
        private void compruebaAnadir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Llama a la bbdd para guardar los cambios que se hayan hecho
        private void ejecutaGuardarBD(object sender, ExecutedRoutedEventArgs e)
        {
            cvm.guardarDatos();
            System.Windows.MessageBox.Show("Se ha guardado en la BBDD", "Éxito");
        }

        //Llama al metodo ejecutaGuardarBD
        private void compruebaGuardarBD(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Abre la ventana ModificarCliente, le pasamos el cliente que hayamos seleccionado en esta ventana en la lista de clientes
        private void ejecutaModificar(object sender, ExecutedRoutedEventArgs e)
        {
            ModificarCliente modificarCliente = new ModificarCliente(cvm.ListaClientes[lstClientes.SelectedIndex]);
            modificarCliente.ShowDialog();
           
        }

        //Llama al metodo ejecutaModificar, comprobando antes que se haya seleccionado un cliente en la lista
        private void compruebaModificar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lstClientes.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }

        //Llama al metodo ejecutaEliminar, comprobando antes que hayamos seleccionado un cliente en la lista
        private void compruebaEliminar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lstClientes.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }

        //Metodo que elimina un clinete
        private void ejecutaEliminar(object sender, ExecutedRoutedEventArgs e)
        {
            //Mostramos una ventana de confirmacion
            DialogResult dr = (DialogResult)System.Windows.MessageBox.Show("¿Estás seguro?", "ÉXITO", MessageBoxButton.YesNo);

            //Rescatamos el id de cliente seleccionado
            String idCliente = cvm.ListaClientes[lstClientes.SelectedIndex].dni;

            //Consultamos la bbdd para buscar ese cliente (por el id)
            clientes objCliente = cvm.objBD.clientes.Find(idCliente);

            //Si confirmamos que queremos borrar ese cliente
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                //Miramos si ese cliente tiene pedidos
                while (objCliente.pedidos.Count > 0)
                {
                    //En caso de que tenga, borramos primero los pedidos que tenga
                    var pedido = (pedidos)objCliente.pedidos.First();
                    cvm.objBD.pedidos.Remove(pedido);
                }
                //Borramos de la lista y de la bbdd ese cliente
                cvm.ListaClientes.RemoveAt(lstClientes.SelectedIndex);
                cvm.objBD.clientes.Remove(objCliente);
            }
        }

        //Se lleva a cabo cuando pulsamos el boton 'Atras'
        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            //Cerramos la ventana en la que estamos y abrimos de nuevo la de MenuOrdenadorVentana (la anterior a esta)
            this.Close();
            MenuOrdenadorVentana menuOrdenadorVentana = new MenuOrdenadorVentana();
            menuOrdenadorVentana.Show();        

        }

        //Llama al metodo ejecutaAtras()
        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


    }
}
