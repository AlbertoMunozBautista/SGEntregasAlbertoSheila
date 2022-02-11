﻿using SGEntregasAlbertoSheila.ViewModel;
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
    /// Lógica de interacción para ModificarCliente.xaml
    /// </summary>
    public partial class ModificarCliente : Window
    {
        ArrayList listaProvincias = new ArrayList();
        private clientes cliente;
        private clientes copCliente;
        public ModificarCliente(clientes cli)
        {
            InitializeComponent();
            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.cliente = cli;
            copCliente = (clientes)cliente.Clone();

            this.DataContext = copCliente;

            cargarComboProvincias();
          
        }

        private void cargarComboProvincias()
        {
            using (entregasEntities objBD = new entregasEntities())
            {
                this.cmbProvincia.Items.Clear();
                var qProvincias = from pro in objBD.provincias select pro;

                foreach (var pro in qProvincias.ToList())
                {
                    cmbProvincia.Items.Add(pro.nombre_provincia);
                    listaProvincias.Add(pro.id_provincia);
                }
                cmbProvincia.SelectedIndex = cliente.provincia-1;
            }
        }

        private void ejecutaAceptar(object sender, ExecutedRoutedEventArgs e)
        {
            copCliente.provincia = int.Parse(listaProvincias[cmbProvincia.SelectedIndex].ToString());
            actualizarProperties(copCliente, cliente);
            this.Close();
            MessageBox.Show("Se ha modificado el cliente correctamente", "Exito");
        }

        private void compruebaAceptar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (txtNombre.Text.Trim() != "" && txtApellidos.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtDni.Text.Trim() != "" 
                && txtLocalidad.Text.Trim() != "" && txtDomicilio.Text.Trim() != "")
            {
               
                e.CanExecute = true;
            }
        }

        private void actualizarProperties(clientes clienteOrigen, clientes clienteDestino)
        {

            clienteDestino.nombre = clienteOrigen.nombre;
            clienteDestino.apellidos = clienteOrigen.apellidos;
            clienteDestino.email = clienteOrigen.email;
            clienteDestino.dni = clienteOrigen.dni;
            clienteDestino.localidad = clienteOrigen.localidad;
            clienteDestino.domicilio = clienteOrigen.domicilio;
            clienteDestino.provincia = clienteOrigen.provincia;

        }

        private void ejecutaCancelar(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void compruebaCancelar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
