using System;
using System.Collections.Generic;
using System.Text;
using MobileApp.Shared.Models;

namespace MobileApp.Shared.Helpers
{
    static class ModelHelper
    {
        /// <summary>
        /// Converts App model into Exchange model (Converter of currencies)
        /// Example:
        /// 1 EUR = 1.2 USD
        /// 10 EUR = 1.2 USD * 10 = 12 USD
        /// ...
        /// </summary>
        /// <param name="model">App Model</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static ExchangeModel ToExchangeModel(this CurrencyModel model, double amount) =>
            new ExchangeModel()
            {
                Code = model.Code,
                Rating = model.Rating * amount
            };
    }
}
