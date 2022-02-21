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

        private bool dni;
        private bool email;

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


        private bool validarCorreo(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool validarDni(string dni)
        {
            //Comprobamos si el DNI tiene 9 digitos
            if (dni.Length != 9)
            {
                //No es un DNI Valido
                return false;
            }

            //Extraemos los números y la letra
            string dniNumbers = dni.Substring(0, dni.Length - 1);
            string dniLeter = dni.Substring(dni.Length - 1, 1);
            //Intentamos convertir los números del DNI a integer
            var numbersValid = int.TryParse(dniNumbers, out int dniInteger);
            if (!numbersValid)
            {
                //No se pudo convertir los números a formato númerico
                return false;
            }
            if (CalculateDNILeter(dniInteger) != dniLeter)
            {
                //La letra del DNI es incorrecta
                return false;
            }
            //DNI Valido :)
            return true;
        }

        private string CalculateDNILeter(int dniNumbers)
        {
            //Cargamos los digitos de control
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }

        private void ejecutaAceptar(object sender, ExecutedRoutedEventArgs e)
        {

            dni = validarDni(this.txtDni.Text);
            email = validarCorreo(this.txtEmail.Text);

            if (dni && email)
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

            } else if (!dni && !email)
            {
                MessageBox.Show("Correo y DNI inválidos.");

            } else if (!dni)
            {
                MessageBox.Show("DNI inválido.");
            } else
            {
                MessageBox.Show("Correo inválido.");
            }

           

        }

        private void compruebaAceptar(object sender, CanExecuteRoutedEventArgs e)
        {
            
            if (txtNombre.Text.Trim() != "" && txtApellidos.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtDni.Text.Trim() != "" && txtLocalidad.Text.Trim() != "" && txtDomicilio.Text.Trim() != "")
            {
                e.CanExecute = true;
            }
           
            
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
