//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGEntregasAlbertoSheila
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class pedidos : INotifyPropertyChanged, ICloneable
    {

        public int id_pedido_;
        public int id_pedido {
            get { return id_pedido_; }
            set
            {
                id_pedido_ = value;
                notificarPropertyChanged();
            }
        }

        public string cliente_;
        public string cliente
        {
            get { return cliente_; }
            set
            {
                cliente_ = value;
                notificarPropertyChanged();
            }
        }

        public System.DateTime fecha_pedido_;
        public System.DateTime fecha_pedido
        {
            get { return fecha_pedido_; }
            set
            {
                fecha_pedido_ = value;
                notificarPropertyChanged();
            }
        }

        public string descripcion_;
        public string descripcion
        {
            get { return descripcion_; }
            set
            {
                descripcion_ = value;
                notificarPropertyChanged();
            }
        }

        public Nullable<System.DateTime> fecha_entrega_;
        public Nullable<System.DateTime> fecha_entrega {
            get { return fecha_entrega_; }
            set
            {
                fecha_entrega_ = value;
                notificarPropertyChanged();
            }
        }

        public byte[] firma_;
        public byte[] firma
        {
            get { return firma_; }
            set
            {
                firma_ = value;
                notificarPropertyChanged();
            }
        }

        public clientes clientes_;
        public virtual clientes clientes
        {
            get { return clientes_; }
            set
            {
                clientes_ = value;
                notificarPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notificarPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
