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
        //Si pulsamos el boton Ctrl y el espacio es como si pulsasemos el botón 'Salir'
        public static RoutedUICommand Salir = new RoutedUICommand("Comando Salir", "Salir",
         typeof(Comandos),
         new InputGestureCollection()
         {
                new KeyGesture(Key.Escape, ModifierKeys.None)
         }
         );

        //Si pulsamos el boton Ctrl y la O es como si pulsasemos la opción de 'Ordenador'
        public static RoutedUICommand Ordenador = new RoutedUICommand("Comando Ordenador", "Ordenador",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.O, ModifierKeys.Control)
        }
        );

        //Si pulsamos el boton Ctrl y la tecla T es como si pulsasemos la opción de 'Tablet'
        public static RoutedUICommand Tablet = new RoutedUICommand("Comando Tablet", "Tablet",
       typeof(Comandos),
       new InputGestureCollection()
       {
                new KeyGesture(Key.T, ModifierKeys.Control)
       }
       );

        //Si pulsamos el boton Ctrl y la tecla E es como si pulsasemos el botón 'Atrás'
        public static RoutedUICommand Atras = new RoutedUICommand("Comando Atras", "Atras",
        typeof(Comandos),
        new InputGestureCollection()
        {
                new KeyGesture(Key.E, ModifierKeys.Control)
        }
        );

        //Si pulsamos el boton Ctrl y la tecla A es como si pulsasemos el botón 'Añadir'
        public static RoutedUICommand Añadir = new RoutedUICommand("Comando cuando...", "Añadir",
           typeof(Comandos),
           new InputGestureCollection()
           {
                new KeyGesture(Key.A, ModifierKeys.Control)
           }
           );

        //Si pulsamos el boton Ctrl y la tecla M es como si pulsasemos el botón 'Modificar'
        public static RoutedUICommand Modificar = new RoutedUICommand("Comando cuando...", "Modificar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.M, ModifierKeys.Control)
          }
          );

        //Si pulsamos el boton Ctrl y la tecla D es como si pulsasemos el botón 'Eliminar'
        public static RoutedUICommand Eliminar = new RoutedUICommand("Comando cuando...", "Eliminar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.D, ModifierKeys.Control)
          }
          );

        //Si pulsamos el boton Ctrl y la tecla S es como si pulsasemos el botón 'Guardar'
        public static RoutedUICommand Guardar = new RoutedUICommand("Comando cuando...", "Guardar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.S, ModifierKeys.Control)
          }
          );
        //Si pulsamos el boton Ctrl y la tecla N es como si pulsasemos el botón 'Aceptar'
        public static RoutedUICommand Aceptar = new RoutedUICommand("Comando cuando...", "Aceptar",
         typeof(Comandos),
         new InputGestureCollection()
         {
                new KeyGesture(Key.N, ModifierKeys.Control)
         }
         );

        //Si pulsamos el boton Ctrl y la tecla C es como si pulsasemos el botón 'Cancelar'
        public static RoutedUICommand Cancelar = new RoutedUICommand("Comando cuando...", "Cancelar",
          typeof(Comandos),
          new InputGestureCollection()
          {
                new KeyGesture(Key.C, ModifierKeys.Control)
          }
          );


    }
}
