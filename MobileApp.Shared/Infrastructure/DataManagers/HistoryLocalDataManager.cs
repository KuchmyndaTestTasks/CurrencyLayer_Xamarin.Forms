using System;
using System.Collections.Generic;
using MobileApp.Shared.Abstractions;
using MobileApp.Shared.Global;
using MobileApp.Shared.Models;

namespace CurrencyLayerApp.Infrastructure.DataManagers
{
    class HistoryLocalDataManager : DataManager<Dictionary<DateTime, ApiCurrencyModel>>
    {
        public HistoryLocalDataManager() : base(CommonData.HistoricalDataFile)
        {
        }
        public override Dictionary<DateTime, ApiCurrencyModel> Upload()
        {
            return Serializator.Deserialize<Dictionary<DateTime, ApiCurrencyModel>>();
        }
    }
}