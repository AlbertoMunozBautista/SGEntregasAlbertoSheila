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
    /// Lógica de interacción para PrimeraTabletVentana.xaml
    /// </summary>
    public partial class PrimeraTabletVentana : Window
    {
        CollectionViewModel cvm;
        public PrimeraTabletVentana()
        {
            InitializeComponent();

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //ScrollBar, si en vertical no en horizontal
            lstClientes.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);

            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
        }

        //Se ejecuta al pulsar el boton 'atras'
        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            //Cierra esta ventana y se abre la principal (la anterior)
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }

        //Llama a ejecutaAtras()
        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Metodo que se ejecuta al cambiar la seleccion de la lista de clientes
        private void lstClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Rescatamos el dni de cliente segun el item seleccionado
            String dniCliente = cvm.ListaClientes[lstClientes.SelectedIndex].dni;

            //Hacemos una consulta para obtener los pedidos de un cliente en concreto y que no se haya entregado
            var pedidosVacios = from pe in cvm.objBD.pedidos
                                where pe.cliente.Equals(dniCliente) && pe.fecha_entrega == null
                                select pe;
            //Si esta consulta nos da registros
            if (pedidosVacios.Count() > 0)
            {
                //Cerramos esta ventana y abrimos segundaTabletVentana (pasandole el dni del cliente)
                this.Close();
                SegundaTabletVentana segundaTabletVentana = new SegundaTabletVentana(dniCliente);
                segundaTabletVentana.ShowDialog();
            }
            else
            {
                MessageBox.Show("Este cliente no tiene ningun pedido para firmar");
            }
        }
    }
}
