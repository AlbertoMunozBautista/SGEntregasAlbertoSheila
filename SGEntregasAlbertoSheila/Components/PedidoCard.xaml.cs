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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila.Components
{
    /// <summary>
    /// Lógica de interacción para PedidoCard.xaml
    /// </summary>
    public partial class PedidoCard : UserControl
    {
        CollectionViewModel cvm;
        public PedidoCard()
        {
            InitializeComponent();
            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            this.MouseLeftButtonUp += ContenedorCards_MouseLeftButtonUp;
            this.TouchDown += ContenedorCards_TouchDown;
        }

        private void ContenedorCards_TouchDown(object sender, TouchEventArgs e)
        {
            seleccionTarjetas(sender);
       
        }

        private void ContenedorCards_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            seleccionTarjetas(sender);
            e.Handled = true;

        }

        private void seleccionTarjetas(object sender)
        {
            //MessageBox.Show(((PedidoCard)sender).idPedido.ToString());

            pedidos objPedido = cvm.objBD.pedidos.Find(int.Parse(((PedidoCard)sender).idPedido.ToString()));

            FirmaPedido firmaPedido = new FirmaPedido(objPedido, cvm);
            firmaPedido.ShowDialog();
        }

        public int idPedido
        {
            get { return (int)GetValue(idPedidoProperty); }
            set { SetValue(idPedidoProperty, value); }
        }

        //el último valor es el valor por defecto
        public static readonly DependencyProperty idPedidoProperty =
            DependencyProperty.Register("idPedido", typeof(int), typeof(PedidoCard), new PropertyMetadata(0));


        public DateTime fechaPedido
        {
            get { return (DateTime)GetValue(fechaPedidoProperty); }
            set { SetValue(fechaPedidoProperty, value); }
        }

        public static readonly DependencyProperty fechaPedidoProperty =
            DependencyProperty.Register("fechaPedido", typeof(DateTime), typeof(PedidoCard), new PropertyMetadata(default(DateTime)));


        public string descripcion
        {
            get { return (string)GetValue(descripcionProperty); }
            set { SetValue(descripcionProperty, value); }
        }

        public static readonly DependencyProperty descripcionProperty =
            DependencyProperty.Register("descripcion", typeof(string), typeof(PedidoCard), new PropertyMetadata(string.Empty));


      

    }
}
