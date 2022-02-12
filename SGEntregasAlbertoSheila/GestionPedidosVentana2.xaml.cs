﻿using SGEntregasAlbertoSheila.ViewModel;
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
using System.Windows.Shapes;

namespace SGEntregasAlbertoSheila
{
    /// <summary>
    /// Lógica de interacción para GestionPedidosVentana2.xaml
    /// </summary>
    public partial class GestionPedidosVentana2 : Window
    {
        clientes cli;
        CollectionViewModel cvm;
        public GestionPedidosVentana2(clientes cliente)
        {
            InitializeComponent();

            //cvm = (CollectionViewModel)this.Resources["ColeccionVM"];

            cvm = (CollectionViewModel)((ObjectDataProvider)this.Resources["ColeccionVM"]).ObjectInstance;

            cli = cliente;

            //le metemos al constructor del view model to lo gordo
            ((ObjectDataProvider)this.FindResource("ColeccionVM")).ConstructorParameters.Clear();
            ((ObjectDataProvider)this.FindResource("ColeccionVM")).ConstructorParameters.Add(cli.dni);
            ((ObjectDataProvider)this.FindResource("ColeccionVM")).Refresh();

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tbCliente.Text = cli.nombre + " " + cli.apellidos;

        }


        private void ejecutaAnadir(object sender, ExecutedRoutedEventArgs e)
        {
            //Mostramos la ventana añadir pedido y le pasamos al cliente, y el collection view model
            AnadirPedido anadirPedido = new AnadirPedido(cli, cvm);
            anadirPedido.ShowDialog();
        }

        private void compruebaAnadir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ejecutaGuardarBD(object sender, ExecutedRoutedEventArgs e)
        {
            //Llamos al metodo guardarDatos de la clase cvm para guardar en la base de datos
            cvm.guardarDatos();
            System.Windows.MessageBox.Show("Se ha guardado en la BBDD", "Éxito");
        }

        private void compruebaGuardarBD(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void ejecutaModificar(object sender, ExecutedRoutedEventArgs e)
        {
            //Guardamos en pos la fila que hemos seleccionado en la lista
            int pos = dgPedidos.SelectedIndex;
            //Llamamos a la ventana modificar pasandole el cliente entero, y el pedido seleccionado
            ModificarPedido modificarPedido = new ModificarPedido(cli, cvm.ListaPedidos[pos]);
            modificarPedido.ShowDialog();
            
        }

        private void compruebaModificar(object sender, CanExecuteRoutedEventArgs e)
        {
            //Si hay alguna fila seleccionada, nos habilita el boton modificar
            if (dgPedidos.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }

        //Nos elimina el pedido seleccionado
        private void ejecutaEliminar(object sender, ExecutedRoutedEventArgs e)
        {
            //Mostramos un mensaje para que el usuario confirme la eliminacion del pedido
            DialogResult dr = (DialogResult)System.Windows.MessageBox.Show("¿Estás seguro?", "ÉXITO", MessageBoxButton.YesNo);
            //Obtenemos el idPedido de la lista pedidos que tenemos en cvm
            int idPedido = cvm.ListaPedidos[dgPedidos.SelectedIndex].id_pedido;
            //Hacemos una consulta en los pedidos, con el id que hemos recuperado antes
            pedidos objPedido = cvm.objBD.pedidos.Find(idPedido);

            //Si el usuario acepta a borrar el pedido
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                //Lo eliminamos de la bbdd
                cvm.objBD.pedidos.Remove(objPedido);
                //Lo eliminamos de la lista de pedidos y de la lista que mostramos en la ventana
                cvm.ListaPedidos.RemoveAt(dgPedidos.SelectedIndex);
                System.Windows.MessageBox.Show("Pedido borrado correctamente", "Exito");
            }

           
        }

        //Si hay algun pedido seleccionado, nos habilita el boton eliminar
        private void compruebaEliminar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgPedidos.SelectedIndex != -1)
            {
                e.CanExecute = true;
            }
        }


        private void ejecutaAtras(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            MenuOrdenadorVentana menuOrdenadorVentana = new MenuOrdenadorVentana();
            menuOrdenadorVentana.Show();

        }

        private void compruebaAtras(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
