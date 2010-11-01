using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoughtsAndCrosses.Converter
{
    public class NoughtsAndCrossesConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Mark? m = (Mark?) value;
            if (!m.HasValue)
                return null;
            if (m.Value == Mark.Cross)
            {
                return new Uri("/NoughtsAndCrosses;component/cross.png", UriKind.Relative);
            }
            return new Uri("/NoughtsAndCrosses;component/nought.png", UriKind.Relative);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
