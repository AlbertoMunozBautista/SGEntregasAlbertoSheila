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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        //Preguntamos al usuario si esta seguro de salir, y cerramos la aplicacion
        private void ejecutaSalir(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult dr = (DialogResult)System.Windows.MessageBox.Show("¿Estás seguro?", "Salir", MessageBoxButton.YesNo);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }        

        }

        //Ejecuta el metodo ejecutaSalir
        private void compruebaSalir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Abrimos la ventana menuOrdenadorVentana
        private void ejecutaOrdenador(object sender, ExecutedRoutedEventArgs e)
        {
            this.Hide();//Ocultamos esta ventana

            MenuOrdenadorVentana menuOrdenadorVentana = new MenuOrdenadorVentana();
            menuOrdenadorVentana.Show();
        }

        //Llama al metodo ejecutaOrdenador()
        private void compruebaOrdenador(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Abrimos la ventana primeraTabletVentana
        private void ejecutaTablet(object sender, ExecutedRoutedEventArgs e)
        {
            this.Hide();//Ocultamos esta ventana

            PrimeraTabletVentana primeraTabletVentana = new PrimeraTabletVentana();
            primeraTabletVentana.Show();

        }
        //Llama al metodo de ejecutaTablet
        private void compruebaTablet(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
