using System;
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
    /// Lógica de interacción para SegundaTabletVentana.xaml
    /// </summary>
    public partial class SegundaTabletVentana : Window
    {
        String dni;
        public SegundaTabletVentana(String dni)
        {
            InitializeComponent();
            this.dni = dni;
        }
    }
}
