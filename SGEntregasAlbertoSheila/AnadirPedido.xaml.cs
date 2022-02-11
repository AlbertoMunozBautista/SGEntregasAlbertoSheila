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
    /// Lógica de interacción para AnadirPedido.xaml
    /// </summary>
    public partial class AnadirPedido : Window
    {
        clientes cli;
        DateTime fechaHoy = DateTime.Now;
        CollectionViewModel cvm;

        public AnadirPedido(clientes cli, CollectionViewModel cvm)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.cvm = cvm;
            this.cli = cli;
            //Obtener el nombre del cliente seleccionado
            txtCliente.Text = cli.nombre;
           
            txtFechaPedido.Text = fechaHoy.ToString("dd/MM/yyyy");


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(tbDescripcion.Text.Trim() == "")
            {
                MessageBox.Show("Debes rellenar la descripcion del pedido", "Atención");
            }
            else
            {
                pedidos objPedido = new pedidos()
                {
                    cliente = cli.dni,
                    fecha_pedido = DateTime.Parse(txtFechaPedido.Text),
                    descripcion = tbDescripcion.Text
                };

                cvm.objBD.pedidos.Add(objPedido);
                cvm.ListaPedidos.Add(objPedido);
                MessageBox.Show("Pedido realizado correctamente", "Éxito");
                this.Close();
            }
        }
    }
}
