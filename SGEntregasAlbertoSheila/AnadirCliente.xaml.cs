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
        //Variables
        CollectionViewModel cvm;

        ArrayList listaProvincias = new ArrayList();

        private bool dni;
        private bool email;

        //Esta ventana recibe el CollectionViewModel de GestionClinetesVentana
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

            //Llamamos al metodo cargar provincias
            cargarProvincias();
             
        }

        //En este metodo rellenamos el combobox de provincias con las que existen en la base de datos
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

        //Metodo para validar correo
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

        //Metodo que comprueba que el dni tenga un formato correcto
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

        //Metodo que nos calcula que el dni sea real, calcula su letra
        private string CalculateDNILeter(int dniNumbers)
        {
            //Cargamos los digitos de control
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }

        //Boton aceptar y sus comprobaciones
        private void ejecutaAceptar(object sender, ExecutedRoutedEventArgs e)
        {
            //Llamamos a los metodos para comprobar que el dni y el correo estén correctos
            dni = validarDni(this.txtDni.Text);
            email = validarCorreo(this.txtEmail.Text);

            //Si todo esta bien, creamos el nuevo cliente con los datos recogidos
            if (dni && email)
            {
                //Creamos un objeto del tipo clientes(bbdd)
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

                /*Añadimos el nuevo cliente a la lista y a la base de datos
                (aunque no lo guarda en la base de datos hasta que no pulsemos el boton correspondiene para ello)*/
                cvm.objBD.clientes.Add(objCliente);
                cvm.ListaClientes.Add(objCliente);

                //Mensaje de que todo ha salido correctamente
                System.Windows.MessageBox.Show("Cliente insertado correctamente", "ÉXITO");
                this.Close();//Cerramos la ventana

            }
            //Si el dni y el email no son correctos mostramos un mensaje
            else if (!dni && !email)
            {
                MessageBox.Show("Correo y DNI inválidos.");
            }
            //Si el dni no es correcto mostramos un mensaje
            else if (!dni)
            {
                MessageBox.Show("DNI inválido.");
            }
            //Si el email no es correctos mostramos un mensaje
            else
            {
                MessageBox.Show("Correo inválido.");
            }
        }


        //Metodo que comprueba que cuando le demos a 'Aceptar' no hay ningun campo vacio del formulario. Esto llama automaticamente al ejecutaAceptar()
        private void compruebaAceptar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (txtNombre.Text.Trim() != "" && txtApellidos.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtDni.Text.Trim() != "" && txtLocalidad.Text.Trim() != "" && txtDomicilio.Text.Trim() != "")
            {
                e.CanExecute = true;
            }
        }

        //Cierra la ventana en la que estamos
        private void ejecutaCancelar(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //Metodo que se ejecuta cuando pulsemos el boton 'Cancelar' y llama al metodo ejecutarCancelar()
        private void compruebaCancelar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
