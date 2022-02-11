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
        CollectionViewModel cvm;
        ArrayList listaClientes = new ArrayList();
        public GestionPedidosVentana()
        {
            InitializeComponent();
            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cargarComboClientes();
        }

        private void cargarComboClientes()
        {
            using (entregasEntities objBD = new entregasEntities())
            {
                this.cmbClientes.Items.Clear();
                var qClientes = from cli in objBD.clientes orderby cli.apellidos, cli.nombre select cli;

                foreach (var cli in qClientes.ToList())
                {
                    cmbClientes.Items.Add(cli.apellidos + ", " + cli.nombre);
                    listaClientes.Add(cli.dni);
                }
                cmbClientes.SelectedIndex = 0;
            }
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            int pos = cmbClientes.SelectedIndex;
            string dniClienteSeleccionado = listaClientes[pos].ToString();
            clientes objCliente = cvm.objBD.clientes.Find(dniClienteSeleccionado);
            GestionPedidosVentana2 gestionPedidos2 = new GestionPedidosVentana2(objCliente);
            gestionPedidos2.ShowDialog();
        }
    }
}
