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
    /// Lógica de interacción para FirmaPedido.xaml
    /// </summary>
    public partial class FirmaPedido : Window
    {
        DateTime fechaHoy = DateTime.Now;
        pedidos pedido;
        public FirmaPedido(pedidos pedido)
        {
            InitializeComponent();

            this.pedido = pedido;

            this.DataContext = pedido;

            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            txtFechaEntrega.Text = fechaHoy.ToString("dd/MM/yyyy");
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
