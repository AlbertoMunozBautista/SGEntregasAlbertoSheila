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
    /// Lógica de interacción para AnadirCliente.xaml
    /// </summary>
    public partial class AnadirCliente : Window
    {

        CollectionViewModel cvm;

        ArrayList listaProvincias = new ArrayList();

        public AnadirCliente(CollectionViewModel cvm)
        {
            InitializeComponent();

            this.cvm = cvm;

            //ocultar botones
            this.WindowStyle = WindowStyle.None;

            //no redimensionable
            this.ResizeMode = ResizeMode.NoResize;

            //centrar pantalla
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            cargarProvincias();
        }

        private void cargarProvincias()
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
            }
        }

        private void ejecutaAceptar(object sender, ExecutedRoutedEventArgs e)
        {

            clientes objCliente = new clientes()
            {
                nombre = txtNombre.Text,
                apellidos = txtApellidos.Text,
                localidad = txtLocalidad.Text,
                dni = txtDni.Text,
                email = txtEmail.Text,
                domicilio = txtDomicilio.Text,
                provincia = int.Parse(listaProvincias[cmbProvincia.SelectedIndex].ToString())
            };

            
            cvm.objBD.clientes.Add(objCliente);
            cvm.ListaClientes.Add(objCliente);
            

            System.Windows.MessageBox.Show("Cliente insertado correctamente", "ÉXITO");
            this.Close();

        }

        private void compruebaAceptar(object sender, CanExecuteRoutedEventArgs e)
        {
            /*
            if (txtNombre.Text.Trim() != "" && txtApellidos.Text.Trim() != "" && txtLocalidad.Text.Trim() != "" && txtAlergias.Text.Trim() != "" && txtAdestacar.Text.Trim() != "")
            {
                e.CanExecute = true;
            }*/
            e.CanExecute = true;
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
