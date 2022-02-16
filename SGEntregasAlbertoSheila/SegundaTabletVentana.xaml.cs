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

        bool apaisado = false;


        int anchooo = 0;
        int altooo = 0;
        public SegundaTabletVentana(String dni)
        {
            InitializeComponent();
            this.dni = dni;

            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            anchooo = int.Parse(SystemParameters.PrimaryScreenWidth.ToString());
            altooo = int.Parse(SystemParameters.PrimaryScreenHeight.ToString());

            SystemEvents.DisplaySettingsChanged += Current_SizeChanged;

            cargarTarjeta();


        }

        private void Current_SizeChanged(object sender, EventArgs eventArgs)
        {
            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                //MessageBox.Show("apaisada" +" alto:" +  SystemParameters.PrimaryScreenHeight + " ancho: " + SystemParameters.PrimaryScreenWidth);
                SPcontenedorTarjetas.Orientation = Orientation.Horizontal;
                //apaisado = true;
            }
            else
            {
                //MessageBox.Show("no apaisada " + " alto: " +  SystemParameters.PrimaryScreenHeight + " ancho: " + SystemParameters.PrimaryScreenWidth);
                //apaisado = false;             
                SPcontenedorTarjetas.Orientation = Orientation.Vertical;
            }


        }

        private void panel_Touch(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You've touched n°" + SPcontenedorTarjetas.Children.IndexOf(sender as UIElement));
        }




        private void cargarTarjeta()
        {
            ArrayList listaPedidos = new ArrayList();
            var pedi = from pe in cvm.objBD.pedidos
                       where pe.cliente.Equals(dni)
                       select pe;

            foreach (var item in pedi.ToList())
            {
                var tp = new PedidoCard();

                tp.idPedido = item.id_pedido;
                tp.fechaPedido = item.fecha_pedido;
                tp.descripcion = item.descripcion;
                listaPedidos.Add(tp);


                // this.SPcontenedorTarjetas.Children.Add(tp);
                
            }
            int i = 0;

            foreach (var item in listaPedidos)
            {
                listaPedidosVentana.Items.Add(listaPedidos[i]);
                i++;
            }
        }

    }



  
    
}
