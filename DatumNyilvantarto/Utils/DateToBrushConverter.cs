using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DatumNyilvantarto.Utils
{
    public class DateToBrushConverter : IValueConverter
    {
        private static readonly System.Windows.Media.Brush RedBrush = new SolidColorBrush(Colors.Red);
        private static readonly System.Windows.Media.Brush YellowBrush = new SolidColorBrush(Colors.Yellow);
        private static readonly System.Windows.Media.Brush GreenBrush = new SolidColorBrush(Colors.Green);
        private static readonly System.Windows.Media.Brush WhiteBrush = new SolidColorBrush(Colors.White);
        private static readonly System.Windows.Media.Brush BlackBrush = new SolidColorBrush(Colors.Black);
        private static readonly System.Windows.Media.Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return TransparentBrush;
            }

            if (value is DateTime dateTime)
            {
                var days = (dateTime - DateTime.Today).TotalDays;

                if (days <= 14)
                {
                    return parameter?.ToString() == "Foreground" ? WhiteBrush : RedBrush;
                }
                else if (days <= 30)
                {
                    return parameter?.ToString() == "Foreground" ? BlackBrush : YellowBrush;
                }
                else
                {
                    return parameter?.ToString() == "Foreground" ? WhiteBrush : GreenBrush;
                }
            }
            else
            {
                return TransparentBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
