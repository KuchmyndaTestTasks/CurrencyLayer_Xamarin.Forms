namespace MobileApp.Models
{
    public class CurrencyModel: ExchangeModel
    {
        /// <summary>
        /// Full name of Currency.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Selected currency in Setting Tab.
        /// </summary>
        public bool IsSelected { get; set; } = false;
    }
}
