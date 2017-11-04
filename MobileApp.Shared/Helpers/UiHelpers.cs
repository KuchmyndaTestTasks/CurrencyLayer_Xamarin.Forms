using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileApp.Shared.Infrastructure;
using MobileApp.Shared.Models;
using Xamarin.Forms;

namespace MobileApp.Shared.Helpers
{
    static class UiHelpers
    {
        #region Searching flags for some codes

        internal static ImageSource GetImage(string filename, string format)
        {
            string name = $"{filename}.{format}";
            return Device.OnPlatform(iOS: ImageSource.FromFile($"Images/{name}"),
                Android: ImageSource.FromFile($"{name}"),
                WinPhone: ImageSource.FromFile($"Images/{name}"));
        }

        /// <summary>
        /// Fing flag icon by code
        /// Example:
        /// Code 'USD' -> 'US' -> 'us' -> 'us'.png: 
        /// take first 2 letters and convert to the lower case
        /// later concat '.png' to end of taken letters
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <returns></returns>
        public static ImageSource GetImage(ExchangeModel currency)
        {
            return GetImage(new string(currency.Code.ToLower().Take(2).ToArray()), "png");
        }

        #endregion
    }
}
