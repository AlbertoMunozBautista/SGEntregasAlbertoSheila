using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SGEntregasAlbertoSheila
{
    class Comandos
    {
        public static RoutedUICommand Salir = new RoutedUICommand("Salir", "Salir",
         typeof(Comandos),
         new InputGestureCollection()
         {
                new KeyGesture(Key.Escape, ModifierKeys.None)
         }
         );

        public static RoutedUICommand Ordenador = new RoutedUICommand("Ordenador", "Ordenador",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.O, ModifierKeys.Control)
        }
        );

        public static RoutedUICommand Atras = new RoutedUICommand("Atras", "Atras",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.A, ModifierKeys.Control)
        }
        );

    }
}
