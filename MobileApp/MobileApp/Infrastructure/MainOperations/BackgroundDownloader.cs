using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyLayerApp.Infrastructure.DataManagers;
using MobileApp.Abstractions;
using MobileApp.Global;
using MobileApp.Models;

namespace MobileApp.Infrastructure.MainOperations
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

        public static void Download()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                DataManager<Dictionary<DateTime, ApiCurrencyModel>> dataManager = 
                    new HistoryLocalDataManager();
                var historicalData = dataManager.Upload();
                /*if (historicalData == null || !historicalData.Any())
                {
                    dataManager = new HistoryApiDataManager(CurrencyLayerApplication.CurrencyModels); 
                    historicalData = dataManager.Upload();
                    if (historicalData == null)
                        Logger.SetLogMessage(MainLogMessages.EmptyHistory, Logger.Color.Red);
                    else
                    {
                        Task.Run(() => dataManager.Save(historicalData));
                        OnUpdateEvent();
                        CurrencyLayerApplication.ThreadSleep(60*Settings.Instance.TimeBetweenCalls);
                    }
                }*/
                if (historicalData != null)
                {
                    CurrencyLayerApplication.HistoricalData = historicalData;
                    CurrencyLayerApplication.ThreadSleep(4);
                }
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
