using MobileApp.Abstractions;
using MobileApp.Models;

namespace CurrencyLayerApp.Infrastructure.DataManagers
{
    internal class CurrencyLocalDataManager : IDataManager<ApiCurrencyModel>
    {
        public void Save(ApiCurrencyModel data)
        {
            //ignored
        }
        public ApiCurrencyModel Upload()
        {
            var result = new ApiCurrencyModel();
            var uow = UnitOfWork.Instance;
            foreach (var currencyModel in uow.GetCurrencies())
            {
                result.Currencies.Add(currencyModel.Code, currencyModel.Rating);
            }
            return result;
        }
    }
}