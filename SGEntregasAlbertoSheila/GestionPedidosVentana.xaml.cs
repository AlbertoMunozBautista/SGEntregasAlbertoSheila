using SGEntregasAlbertoSheila.ViewModel;
using System;
using System.Collections;
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
    /// Lógica de interacción para GestionPedidosVentana.xaml
    /// </summary>
    public partial class GestionPedidosVentana : Window
    {
        //Declaracion de variables
        CollectionViewModel cvm;
        ArrayList listaClientes = new ArrayList();

        MenuOrdenadorVentana menuOrdenador;
        public GestionPedidosVentana(MenuOrdenadorVentana menuOrdenador)
        {
         InitializeComponent();
            //Llamaos de nuevo al ColeectionViewModel, declarado en el codigo de xaml.
            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];

            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            this.menuOrdenador = menuOrdenador;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            cargarComboClientes();
        }

        //Carga el combo con los clientes que haya en la bbdd
        private void cargarComboClientes()
        {
            using (entregasEntities objBD = new entregasEntities())
            {
                this.cmbClientes.Items.Clear();
                //Consulta de los todos los clientes en la bbdd y sacamos el apellido y el nombre
                var qClientes = from cli in objBD.clientes orderby cli.apellidos, cli.nombre select cli;

                foreach (var cli in qClientes.ToList())
                {
                    cmbClientes.Items.Add(cli.apellidos + ", " + cli.nombre);
                    listaClientes.Add(cli.dni);//Lista que recoge todos los dni del cliente en el mismo orden que estan en el combobox
                }
                cmbClientes.SelectedIndex = 0;//por defecto mostramos el primer cliente en el combobox
            }
        }

        //Boton confirmar
        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            //Cogemos la posicion del combobox seleccionado
            int pos = cmbClientes.SelectedIndex;
            //Recuperamos el dni de ese cliente por la lista que obtiene los dni de los clientes
            string dniClienteSeleccionado = listaClientes[pos].ToString();
            //Busvamos el cliente en la bbdd por su dni
            clientes objCliente = cvm.objBD.clientes.Find(dniClienteSeleccionado);
            //Cerramos vetanas
            menuOrdenador.Close();
            this.Close();
            //Abrimos la ventana gestionPedidosVentan2 pasandole el cliente seleccionado
            GestionPedidosVentana2 gestionPedidos2 = new GestionPedidosVentana2(objCliente);
            gestionPedidos2.ShowDialog();
        }

        //Cierra esta ventana
        private void ejecutaCancelar(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //Llama al metodo ejecutaCancelar()
        private void compruebaCancelar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
