using Microsoft.Win32;
using SGEntregasAlbertoSheila.Components;
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
    /// Lógica de interacción para SegundaTabletVentana.xaml
    /// </summary>
    public partial class SegundaTabletVentana : Window
    {
        String dni;
        CollectionViewModel cvm;

        int izquierda;
        int arriba;

        ArrayList listaPedidos = new ArrayList();

        public SegundaTabletVentana(String dni)
        {
            InitializeComponent();
            this.dni = dni;

            cambiarPantalla();

            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        

            SystemEvents.DisplaySettingsChanged += Current_SizeChanged;

            izquierda = (int)this.Left;
            arriba = (int)this.Top;

   

            cargarTarjeta();


        }

        private void cambiarPantalla()
        {

            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                SPcontenedorTarjetas.Orientation = Orientation.Horizontal;
                this.Height = SystemParameters.FullPrimaryScreenHeight;
                this.Width = SystemParameters.FullPrimaryScreenWidth;
                scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            else
            {
                SPcontenedorTarjetas.Orientation = Orientation.Vertical;
                this.Height = SystemParameters.PrimaryScreenHeight;
                this.Width = SystemParameters.PrimaryScreenWidth;
                scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

            }
        }

        private void Current_SizeChanged(object sender, EventArgs eventArgs)
        {
            cambiarPantalla();
        }


        public void cargarTarjeta()
        {
            this.SPcontenedorTarjetas.Children.Clear();

            var pedi = from pe in cvm.objBD.pedidos
                       where pe.cliente.Equals(dni) && pe.fecha_entrega == null
                       select pe;

            foreach (var item in pedi.ToList())
            {
                var tp = new PedidoCard(this);

                tp.idPedido = item.id_pedido;
                tp.fechaPedido = item.fecha_pedido;
                tp.descripcion = item.descripcion;
                listaPedidos.Add(tp);

                //this.SPcontenedorTarjetas.Children
                this.SPcontenedorTarjetas.Children.Add(tp);

            }

            if (SPcontenedorTarjetas.Children.Count <= 0)
            {
                MessageBox.Show("El cliente no tiene tarjetas.");
                this.Close();
                PrimeraTabletVentana primeraTabletVentana = new PrimeraTabletVentana();
                primeraTabletVentana.Show();
            }
        }

        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            PrimeraTabletVentana primeraTabletVentana = new PrimeraTabletVentana();
            primeraTabletVentana.Show();

        }

        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



    }

}
