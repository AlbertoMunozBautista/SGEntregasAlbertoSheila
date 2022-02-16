using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SGEntregasAlbertoSheila.Converters
{
    public class GreaterThanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value.ToString(), out double ANCHO);
            double.TryParse(parameter.ToString(), out double ALTO);

            int anchooo = int.Parse(SystemParameters.PrimaryScreenWidth.ToString());
            int altooo = int.Parse(SystemParameters.PrimaryScreenHeight.ToString());

            return anchooo > altooo;


        }
        /*
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenhight = System.Windows.SystemParameters.PrimaryScreenHeight;
            return screenWidth < screenhight;1536
        }*/

     
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }
}
