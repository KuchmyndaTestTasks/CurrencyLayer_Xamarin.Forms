using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyLayerApp.Infrastructure.DataManagers;
using MobileApp.Shared.Abstractions;
using MobileApp.Shared.Global;
using MobileApp.Shared.Models;
using Plugin.Connectivity;

namespace MobileApp.Shared.Infrastructure.MainOperations
{
    static class BackgroundDownloader
    {
        static BackgroundDownloader()
        {
            _provider=new CurrencyLayerProvider(new HttpClient());
        }

        #region <Fields>
        private static Thread _thread;
        private static CurrencyLayerProvider _provider;
        internal delegate void Updater();
        public static event Updater UpdateEvent;
        #endregion

        #region <Properties>
        
        #endregion

        #region <Methods>

        public static void Init()
        {
            if (CrossConnectivity.IsSupported)
            {
                var instance = CrossConnectivity.Current;
                instance.ConnectivityChanged += (sender, args) =>
                {
                    if (args.IsConnected)
                        Start();
                    else
                        Abort();
                };
                if(instance.IsConnected)
                    Start();
            }
        }

        public static void Download()
        {
            while (true)
            {
                DataManager<Dictionary<DateTime, ApiCurrencyModel>> dataManager =
                    new HistoryApiDataManager(CurrencyLayerApplication.CurrencyModels);
                var historicalData = dataManager.Upload();
                if (historicalData == null)
                    Logger.SetLogMessage(MainLogMessages.EmptyHistory, Logger.Color.Red);
                else
                {
                    dataManager.Save(historicalData);
                }

                if (historicalData != null)
                {
                    CurrencyLayerApplication.HistoricalData = historicalData;
                    CurrencyLayerApplication.ThreadSleep(4);
                    OnUpdateEvent();
                }
                CurrencyLayerApplication.ThreadSleep(60 * Settings.Instance.TimeBetweenCalls);
            }
        }

        public static void Start()
        {
            _thread = new Thread(Download);
            _thread.Start();
        }
        public static void Abort()
        {
            _thread.Abort();
        }
        #endregion

        private static void OnUpdateEvent()
        {
            UpdateEvent?.Invoke();
        }
    }
}
