using System;
using System.Globalization;
using System.Linq;
using MobileApp.Shared.Models;
using Xamarin.Forms;

namespace MobileApp.Shared.Infrastructure.Converters
{
    class CurrencyModelHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is CurrencyModel)) return value;
            var x = (CurrencyModel) value;
            return $"{x.Code},{x.Name}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            var splited = value.ToString().Split(',');
            return new CurrencyModel() {Code = splited.First(), Name = splited.Last()};
        }
    }
}
