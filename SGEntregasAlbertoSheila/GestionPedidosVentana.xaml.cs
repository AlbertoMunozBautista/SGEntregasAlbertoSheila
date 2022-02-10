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
        ArrayList listaClientes = new ArrayList();
        public GestionPedidosVentana()
        {
            InitializeComponent();
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

        }
    }
}
