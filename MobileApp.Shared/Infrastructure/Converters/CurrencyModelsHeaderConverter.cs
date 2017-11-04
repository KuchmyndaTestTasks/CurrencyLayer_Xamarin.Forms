using System;
using System.Globalization;
using System.Linq;
using MobileApp.Shared.Models;
using Xamarin.Forms;

namespace MobileApp.Shared.Infrastructure.Converters
{
    class CurrencyModelsHeaderConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is CurrencyModel[])) return value;
            var list = (CurrencyModel[]) value;
            return list.Select(x => $"{x.Code},{x.Name}").ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
