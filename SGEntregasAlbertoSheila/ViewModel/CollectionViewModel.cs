﻿using SGEntregasAlbertoSheila.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SGEntregasAlbertoSheila.ViewModel
{
    public class CollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ClientesCollection _listaClientes = new ClientesCollection();
        private PedidosCollection _listaPedidos = new PedidosCollection();


        private entregasEntities _objBD = new entregasEntities();

        public CollectionViewModel()
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            ListaClientes.Clear();
            ListaPedidos.Clear();

            var qClientes = from cli in objBD.clientes select cli;
            foreach (var clien in qClientes.ToList())
            {
                ListaClientes.Add(clien);
            }

            var qPedidos = from ped in objBD.pedidos select ped;

            foreach (var ped in qPedidos.ToList())
            {
                ListaPedidos.Add(ped);
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

        public PedidosCollection ListaPedidos
        {
            get { return _listaPedidos; }
            set
            {
                _listaPedidos = value;
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
