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
    /// Lógica de interacción para ModificarCliente.xaml
    /// </summary>
    public partial class ModificarCliente : Window
    {
        //lista que contiene los id de las provincias
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
            //consulta para cargar el combo box con el nombre las provincias ( de la tabla provincias)
            using (entregasEntities objBD = new entregasEntities())
            {
                this.cmbProvincia.Items.Clear();
                var qProvincias = from pro in objBD.provincias select pro;

                foreach (var pro in qProvincias.ToList())
                {
                    //Vamos rellenando el combobox y la lista con sus id correspondientes en el mismo orden
                    cmbProvincia.Items.Add(pro.nombre_provincia);
                    listaProvincias.Add(pro.id_provincia);
                }
                //mostramos en el combobox la provincia que tenia elegida el cliente -1, para que sean la misma
                cmbProvincia.SelectedIndex = cliente.provincia-1;
            }
        }

        private void ejecutaAceptar(object sender, ExecutedRoutedEventArgs e)
        {
            //guardamos en copCliente.provincia el id de la provincia que el usuario ha seleccionado
            copCliente.provincia = int.Parse(listaProvincias[cmbProvincia.SelectedIndex].ToString());
            //Llamamos a este metodo para actualizar los datos
            actualizarProperties(copCliente, cliente);
            this.Close();
            MessageBox.Show("Se ha modificado el cliente correctamente", "Exito");
        }

        private void compruebaAceptar(object sender, CanExecuteRoutedEventArgs e)
        {
            //Si los campos no estan vacios, habilitamos el boton aceptar
            if (txtNombre.Text.Trim() != "" && txtApellidos.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtDni.Text.Trim() != "" 
                && txtLocalidad.Text.Trim() != "" && txtDomicilio.Text.Trim() != "")
            {
               
                e.CanExecute = true;
            }
        }

        //Actualizamos los datos que antes tenia el cliente en (clienteOrigen) a clienteDestino
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

