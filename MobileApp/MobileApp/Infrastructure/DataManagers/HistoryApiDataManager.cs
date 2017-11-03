using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using MobileApp.Abstractions;
using MobileApp.Global;
using MobileApp.Infrastructure;
using MobileApp.Models;

namespace CurrencyLayerApp.Infrastructure.DataManagers
{
    /// <inheritdoc />
    /// <summary>
    /// More see in IDataProvider
    /// </summary>
    class HistoryApiDataManager : DataManager<Dictionary<DateTime, ApiCurrencyModel>>
    {
        private readonly CurrencyModel[] _currencyModels;

        public HistoryApiDataManager(CurrencyModel[] currencyModels) : base(CommonData.HistoricalDataFile)
        {
            _currencyModels = currencyModels;
        }

        public override void Save(Dictionary<DateTime, ApiCurrencyModel> data)
        {
            if (data == null || !data.Any()) return;
            //Rewrite historical data
            Serializator.Serialize(data);
        }
    

        public override Dictionary<DateTime, ApiCurrencyModel> Upload()
        {
            CurrencyLayerProvider provider =
                new CurrencyLayerProvider(new HttpClient() { Timeout = TimeSpan.FromSeconds(10) });
            return provider.GetHistoricalCurrencyModel(_currencyModels, DateTime.Now,
                    7);
        }
    }
}