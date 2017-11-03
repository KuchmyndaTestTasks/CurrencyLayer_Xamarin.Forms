using System;
using System.Globalization;
using MobileApp.Models;
using Xamarin.Forms;

namespace MobileApp.Infrastructure.Converters
{
    class CurrentModelConverter : IValueConverter
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
