using SGEntregasAlbertoSheila.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SGEntregasAlbertoSheila.ClienteViewModel
{
    public class ClienteCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ClientesCollection _listaClientes = new ClientesCollection();

        private entregasEntities _objBD = new entregasEntities();

        public ClienteCollectionViewModel()
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            ListaClientes.Clear();


            var qClientes = from cli in objBD.clientes select cli;
            foreach (var clien in qClientes.ToList())
            {
                ListaClientes.Add(clien);
            }


        }



        public ClientesCollection ListaClientes
        {
            get { return _listaClientes; }
            set
            {
                _listaClientes = value;
                raisePropertyChanged();
            }
        }


        public entregasEntities objBD
        {
            get { return _objBD; }
            set
            {
                _objBD = value;
                raisePropertyChanged();
            }


        }

        private void raisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void guardarDatos()
        {
            objBD.SaveChanges();
        }
    }
}
