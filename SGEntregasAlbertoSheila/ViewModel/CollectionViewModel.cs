using SGEntregasAlbertoSheila.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SGEntregasAlbertoSheila.ViewModel
{
    public class CollectionViewModel : INotifyPropertyChanged
    {
        //Evento que detecta los cambios en las propiedades
        public event PropertyChangedEventHandler PropertyChanged;

        //Listas de objetos
        private ClientesCollection _listaClientes = new ClientesCollection();
        private PedidosCollection _listaPedidos = new PedidosCollection();

        //Base de datos
        private entregasEntities _objBD = new entregasEntities();

        public CollectionViewModel()
        {
            cargarDatos();
        }

        //Recibe el dni de un cliente y carga sus pedidos
        public void cargarPedidos(String dniClientes)
        {
            ListaPedidos.Clear();

            //Consulta que obtiene los pedidos de un cliente en concreto (segun su dni)
            var qPedidos = from ped in objBD.pedidos where ped.cliente.Equals(dniClientes) select ped;

            //Almacena los pedidos en una lista
            foreach (var ped in qPedidos.ToList())
            {
                ListaPedidos.Add(ped);
            }
        }

        //Carga todos los clientes de la bbdd en una lista
        private void cargarDatos()
        {
            ListaClientes.Clear();
            
            //Consulta que obtiene todos los clientes de la bbdd
            var qClientes = from cli in objBD.clientes select cli;
            foreach (var clien in qClientes.ToList())
            {
                ListaClientes.Add(clien);
            }

           
        }

        //Lista de clientes
        public ClientesCollection ListaClientes
        {
            get { return _listaClientes; }
            set
            {
                _listaClientes = value;
                raisePropertyChanged();
            }
        }

        //Lista de pedidos
        public PedidosCollection ListaPedidos
        {
            get { return _listaPedidos; }
            set
            {
                _listaPedidos = value;
                raisePropertyChanged();
            }
        }

        //Base de datos
        public entregasEntities objBD
        {
            get { return _objBD; }
            set
            {
                _objBD = value;
                raisePropertyChanged();
            }


        }

        //Metodo que detecta los cambios en las propiedades y los lleva a cabo
        private void raisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //Metodo que accede a la bbdd para guardar.
        public void guardarDatos()
        {
            objBD.SaveChanges();
        }
    }
}
