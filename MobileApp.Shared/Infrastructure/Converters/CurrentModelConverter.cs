using System;
using System.Globalization;
using MobileApp.Shared.Models;
using Xamarin.Forms;

namespace MobileApp.Shared.Infrastructure.Converters
{
    public class CurrentModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var model = (CurrencyModel)value;
            return model.Code;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return new CurrencyModel(){Code = value.ToString(), IsSelected = true};
        }
    }
    
}
