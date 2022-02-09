using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SGEntregasAlbertoSheila
{
    //Clase comandos
    public class Comandos
    {
        public static RoutedUICommand Salir = new RoutedUICommand("Comando Salir", "Salir",
         typeof(Comandos),
         new InputGestureCollection()
         {
                new KeyGesture(Key.Escape, ModifierKeys.None)
         }
         );

        public static RoutedUICommand Ordenador = new RoutedUICommand("Comando Ordenador", "Ordenador",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.O, ModifierKeys.Control)
        }
        );

        public static RoutedUICommand Atras = new RoutedUICommand("Comando Atras", "Atras",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.E, ModifierKeys.Control)
        }
        );


        public static RoutedUICommand Añadir = new RoutedUICommand("Comando cuando...", "Añadir",
           typeof(Comandos),
           new InputGestureCollection()
           {
                new KeyGesture(Key.A, ModifierKeys.Control)
           }
           );

        public static RoutedUICommand Modificar = new RoutedUICommand("Comando cuando...", "Modificar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.M, ModifierKeys.Control)
          }
          );

        public static RoutedUICommand Eliminar = new RoutedUICommand("Comando cuando...", "Eliminar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.E, ModifierKeys.Control)
          }
          );

        public static RoutedUICommand Guardar = new RoutedUICommand("Comando cuando...", "Guardar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.S, ModifierKeys.Control)
          }
          );

        public static RoutedUICommand Aceptar = new RoutedUICommand("Comando cuando...", "Aceptar",
         typeof(Comandos),
         new InputGestureCollection()
         {
                new KeyGesture(Key.E, ModifierKeys.Control)
         }
         );

        public static RoutedUICommand Cancelar = new RoutedUICommand("Comando cuando...", "Cancelar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.S, ModifierKeys.Control)
          }
          );


    }
}
