using SGEntregasAlbertoSheila.Components;
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

namespace SGEntregasAlbertoSheila.Views
{
    /// <summary>
    /// Lógica de interacción para UserCardStackPanelView.xaml
    /// </summary>
    public partial class UserCardStackPanelView : UserControl
    {
        CollectionViewModel cvm;
        public UserCardStackPanelView()
        {
            InitializeComponent();
            cvm = (CollectionViewModel)this.Resources["ColeccionVM"];
            cargarTarjeta();

        }
        private void cargarTarjeta()
        {
            var pedi = from pe in cvm.objBD.pedidos
                       select pe;

            foreach (var item in pedi.ToList())
            {
                var tp = new PedidoCard();

                tp.idPedido = item.id_pedido;
                tp.fechaPedido = item.fecha_pedido;
                tp.descripcion = item.descripcion;


                this.SPcontenedorTarjetas.Children.Add(tp);
            }


        }
    }
}
