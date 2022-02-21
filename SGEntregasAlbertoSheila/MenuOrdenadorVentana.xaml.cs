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
    /// Lógica de interacción para MenuOrdenadorVentana.xaml
    /// </summary>
    public partial class MenuOrdenadorVentana : Window
    {
        public MenuOrdenadorVentana()
        {
            InitializeComponent();

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        //Abrimos la ventana principal y ocultamos esta
        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }

        //Llama al metodo ejecutaAtras
        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }
     
        //Metodo que se ejecuta si pulsamos una imagen.
        //Cierra esta ventana y abre la de GestionClientesVentana
        private void gestionClientesClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            GestionClientesVentana gestionClientesVentana = new GestionClientesVentana();
            gestionClientesVentana.ShowDialog();
        }

        //Metodo que se ejecuta si pulsamos una imagen.
        //Cierra esta ventana y abre la de GestionPedidosVentana
        private void gestionPedidosClick(object sender, MouseButtonEventArgs e)
        {
            GestionPedidosVentana gestionPedidosVentana = new GestionPedidosVentana(this);
            gestionPedidosVentana.ShowDialog();
        }
    }
}
