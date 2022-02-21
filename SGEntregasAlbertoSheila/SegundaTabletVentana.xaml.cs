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
        
            //Evento que se lanza cuando se detecta un cambio de orientacion de pantalla
            SystemEvents.DisplaySettingsChanged += Current_SizeChanged;

            izquierda = (int)this.Left;
            arriba = (int)this.Top;

   

            cargarTarjeta();


        }
        //Detecta si el ancho es mayor que el alto y al reves
        private void cambiarPantalla()
        {
            //Si la pntalla esta en horizontal
            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                //Cambiamos la orientacion del stakcpanel
                SPcontenedorTarjetas.Orientation = Orientation.Horizontal;
                //Hacemos que el alto y el ancho sea toda la primera pantalla
                this.Height = SystemParameters.FullPrimaryScreenHeight;
                this.Width = SystemParameters.FullPrimaryScreenWidth;
                //Mostramos el scroll horizontal y ocultamos el vertical
                scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            //Si la pantalla esta en vertical
            else
            {
                //Cambiamos la orientacion del stackpanel
                SPcontenedorTarjetas.Orientation = Orientation.Vertical;
                //Hacemos que el alto y el ancho sea toda la primera pantalla
                this.Height = SystemParameters.PrimaryScreenHeight;
                this.Width = SystemParameters.PrimaryScreenWidth;
                //Mostramos el scroll vertical y ocultamos el horizontal
                scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

            }
        }

        //Cuando se lanza este evento, llamamos al metodo cambiarPantalla()
        private void Current_SizeChanged(object sender, EventArgs eventArgs)
        {
            cambiarPantalla();
        }

        //Cargamos las tarjetas en el stackpanel
        public void cargarTarjeta()
        {
            this.SPcontenedorTarjetas.Children.Clear();

            //Hacemos una consulta para recoger los pedidos de un cliente en concreto y el cual no se haya entregado ya
            var pedi = from pe in cvm.objBD.pedidos
                       where pe.cliente.Equals(dni) && pe.fecha_entrega == null
                       select pe;

            //Recorremos la lista de los pedidos obtenidos en la consulta
            foreach (var item in pedi.ToList())
            {
                //Creamos una nueva tarjeta
                var tp = new PedidoCard(this);

                tp.idPedido = item.id_pedido;
                tp.fechaPedido = item.fecha_pedido;
                tp.descripcion = item.descripcion;
                listaPedidos.Add(tp);

                //Añadimos cada tarjeta al stack panel que las muestra
                this.SPcontenedorTarjetas.Children.Add(tp);

            }

            //Si el stack panel no es rellenado por ningun pedido, avisamos y nos vamos a la ventana anterior
            if (SPcontenedorTarjetas.Children.Count <= 0)
            {
                MessageBox.Show("El cliente no tiene tarjetas.");
                this.Close();
                PrimeraTabletVentana primeraTabletVentana = new PrimeraTabletVentana();
                primeraTabletVentana.Show();
            }
        }

        //Cierra esta ventana y abre primeraTabletVentana
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
