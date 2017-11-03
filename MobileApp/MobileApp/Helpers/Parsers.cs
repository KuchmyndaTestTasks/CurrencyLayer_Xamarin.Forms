using System.IO;
using System.Linq;
using MobileApp.Global;
using MobileApp.Infrastructure;
using MobileApp.Models;

namespace MobileApp.Helpers
{
    public static class Parsers
    {
        /// <summary>
        /// Parses file 'Currencies.txt' into seperated models. (For Setting Tab)
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>collection of models</returns>
        public static CurrencyModel[] ParseCurrencyModels(Stream stream)
        {
            if (stream==null || stream.Length<1)
            {
                return null;
            }
            var lines= System.Text.Encoding.Default.GetString(stream.ToArray()).Split('\n');
            CurrencyModel[] models = new CurrencyModel[lines.Length];
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                var splited = line.Split('\t');
                models[index] = new CurrencyModel() {Code = splited.First(), Name = splited.Last()};
            }
            return models;
        }

        /// <summary>
        /// Gets currencies (all or selected)
        /// </summary>
        /// <param name="selected">all(false) or selected(true) currencies</param>
        /// <returns></returns>
       /* public static CurrencyModel[] GetStoredModels(bool selected = false)
        {
            var result = Parsers.ParseCurrencyModels();
            var uow = UnitOfWork.Instance;
            lock (uow)
            {
                //Checks selected currencies from local DB
                if (selected && uow.Any(typeof(Currency)))
                {
                    var models = uow.GetCurrencies();
                    foreach (var model in models)
                    {
                        //Get currency if it is found
                        if (result.Any(x => x.Code == model.Code))
                        {
                            var first = result.First(x => x.Code == model.Code);
                            var currencyModel = first;
                            currencyModel.IsSelected = true;
                            currencyModel.Rating = first.Rating;
                        }
                    }
                }
                if (selected)
                {
                    result = result.Where(x => x.IsSelected).ToArray();
                }
                return result;
            }
        }

        /// <summary>
        /// Gets history data for charting
        /// Select data for map-structure:each 'distoric date' has list of evsluated currencies
        /// 21.10.2017 : USD=1, EUR= 0.844 ....
        /// 20.10.2017 : USD=1, EUR=0.865 ....
        /// .....
        /// </summary>
        /// <returns></returns>
        public static Dictionary<DateTime, ApiCurrencyModel> GetStoredHistoryData()
        {
            var uow = UnitOfWork.Instance;
            var result = new Dictionary<DateTime, ApiCurrencyModel>();
            lock (uow)
            {
                //Checks selected currencies from local DB
                if (uow.Any(typeof(HistoricalData)))
                {
                    var models = uow.GetHistoricalData();
                    
                    foreach (var model in models)
                    {
                        var date = model.Date.Value;
                        var modelCurrency = model.Currency;
                        //If key exists in dictionary, has to rewrite/adding currency by concrete date.
                        //Else - just push to dictionary
                        if (result.ContainsKey(date))
                        {
                            //Just I took code "USD" from default API response from CurrentLayer.com
                            result[date].Code = "USD";
                            result[date].Currencies.Add(modelCurrency.Code, model.Rating);
                        }
                        else
                        {
                            result.Add(date,
                                new ApiCurrencyModel()
                                {
                                    Code = "USD",
                                    Currencies = {{modelCurrency.Code, model.Rating}}
                                });
                        }
                    }
                    return result;
                }
                return null;
            }
        }*/
    }
}
