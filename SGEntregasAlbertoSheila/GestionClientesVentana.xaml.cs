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
    /// Lógica de interacción para GestionClientesVentana.xaml
    /// </summary>
    public partial class GestionClientesVentana : Window
    {
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

            //lstClientes.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }


        private void ejecutaAnadir(object sender, ExecutedRoutedEventArgs e)
        {

            AnadirCliente anadirVentana = new AnadirCliente(cvm);
            anadirVentana.ShowDialog();
        }

        private void compruebaAnadir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
