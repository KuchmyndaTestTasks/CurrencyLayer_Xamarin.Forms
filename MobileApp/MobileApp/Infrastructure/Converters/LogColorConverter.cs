using System;
using System.Globalization;
using MobileApp.Infrastructure.MainOperations;
using Xamarin.Forms;

namespace MobileApp.Infrastructure.Converters
{
    class LogColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            Color brush = Color.LightGray;
            switch ((Logger.Color) value)
            {
                case Logger.Color.Gray:
                    brush = Color.LightGray;
                    break;
                case Logger.Color.Green:
                    brush = Color.LightSeaGreen;
                    break;
                case Logger.Color.Red:
                    brush = Color.DarkRed;
                    break;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
