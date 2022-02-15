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
        }

        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }

        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void lstClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("hola");
        }
    }


}
