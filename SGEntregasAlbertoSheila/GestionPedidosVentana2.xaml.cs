﻿using System;
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
    /// Lógica de interacción para GestionPedidosVentana2.xaml
    /// </summary>
    public partial class GestionPedidosVentana2 : Window
    {
        clientes cli;
        public GestionPedidosVentana2(clientes cliente)
        {
            InitializeComponent();
            cli = cliente;

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

            AnadirPedido anadirPedido = new AnadirPedido();
            anadirPedido.ShowDialog();
        }

        private void compruebaAnadir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
