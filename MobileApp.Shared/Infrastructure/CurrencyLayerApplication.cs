using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MobileApp.Shared.Global;
using MobileApp.Shared.Infrastructure.MainOperations;
using MobileApp.Shared.Models;
using MobileApp.Shared.Views.MainViews;
using MobileApp.Shared.Views.NavigationPage;
using Xamarin.Forms;

namespace MobileApp.Shared.Infrastructure
{
    /// <summary>
    /// Application structure which store in RAM frequently used data
    /// </summary>
    public static class CurrencyLayerApplication
    {
        /// <summary>
        /// General selected currencies.
        /// </summary>
        public static CurrencyModel[] CurrencyModels
        {
            get
            {
                if (_currencyModels == null) RefreshModels();
                return _currencyModels;
            }
            set { _currencyModels = value; }
        }

        public static Dictionary<DateTime, ApiCurrencyModel> HistoricalData
        {
            get
            {
                if (_historicalData == null) RefreshModels();
                return _historicalData;
            }
            set { _historicalData = value; }
        }

        private static CurrencyModel[] _currencyModels;
        private static Dictionary<DateTime, ApiCurrencyModel> _historicalData;

        static CurrencyLayerApplication()
        {
            RefreshModels();
            Settings.Read();
            var instance = Settings.Instance;
            //Checks is application prepared for using
            var res = CurrencyModels != null && HistoricalData != null && CurrencyModels.Any() && HistoricalData.Any()
                      && !string.IsNullOrEmpty(instance.ApiKey);
            instance.IsConfigured = res;
        }



        /// <summary>
        /// Reread currencies from DB
        /// </summary>
        public static void RefreshModels()
        {
            CurrencyModels = new Serializator(CommonData.SelectedCurrenciesFile)
                .Deserialize<CurrencyModel[]>();
            HistoricalData = new Serializator(CommonData.HistoricalDataFile)
                .Deserialize<Dictionary<DateTime, ApiCurrencyModel>>();
        }

        /// <summary>
        /// Sets current thread in sleeping mode for time between calls.
        /// </summary>
        public static void ThreadSleep()
        {
            ThreadSleep(Settings.Instance.TimeBetweenCalls);
        }

        public static void ThreadSleep(int seconds)
        {
            Thread.Sleep(1000 * seconds);
        }

        public static void InitPage()
        {
            if (Settings.Instance.IsConfigured)
            {
                Application.Current.RedirectTo(new NavigationDrawer(), false);
            }
            else
            {
                Application.Current.RedirectTo(new InitPage(), false);
            }
        }
    }
}
