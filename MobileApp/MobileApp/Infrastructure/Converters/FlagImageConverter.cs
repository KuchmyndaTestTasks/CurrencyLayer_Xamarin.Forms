using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.Infrastructure.Converters
{
    class FlagImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str)) return value;
            return Helpers.UiHelpers.GetImage(new string(str.ToLower().Take(2).ToArray()),"png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
