using System.Windows.Input;
using Mobile.Views.CurrencyLayerPages;
using Mobile.Views.NavigationPage;
using Xamarin.Forms;

namespace Mobile.ViewModels.NavigationViewModels
{
    class NavigationListViewModel : ViewModelBase
    {
        private readonly NavigationDrawer _drawer;
        private ICommand _currencyDataRedirector;
        private ICommand _historicalDataRedirector;
        private ICommand _exchangeDataRedirector;

        public NavigationListViewModel()
        {
            CurrencyDataRedirector = new Command(RedirectToCurrData);
            HistoricalDataRedirector = new Command(RedirectToHistData);
            ExchangeDataRedirector = new Command(RedirectToExchData);
            //SettingRedirector = new Command(RedirectToCurrData);
        }
        public NavigationListViewModel(NavigationDrawer drawer) : this()
        {
            _drawer = drawer;
        }

        #region Properties

        public ICommand CurrencyDataRedirector
        {
            get { return _currencyDataRedirector; }
            set
            {
                _currencyDataRedirector = value;
                OnPropertyChanged();
            }
        }

        public ICommand HistoricalDataRedirector
        {
            get { return _historicalDataRedirector; }
            set
            {
                _historicalDataRedirector = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExchangeDataRedirector
        {
            get { return _exchangeDataRedirector; }
            set
            {
                _exchangeDataRedirector = value;
                OnPropertyChanged();
            }
        }

        public ICommand SettingRedirector { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Goes to CurrentData page.
        /// </summary>
        private void RedirectToCurrData()
        {
            PutPageToNavDrawer(new CurrencyDataPage());
        }
        /// <summary>
        /// Goes to ExchangeData page.
        /// </summary>
        private void RedirectToExchData()
        {
            PutPageToNavDrawer(new ExchangeDataPage());
        }
        /// <summary>
        /// Goes to HistoricalData page.
        /// </summary>
        private void RedirectToHistData()
        {
            PutPageToNavDrawer(new HistoricalDataPage());
        }
        /// <summary>
        /// Putting a page into NavDrawer.
        /// </summary>
        /// <param name="page"></param>
        private void PutPageToNavDrawer(Page page)
        {
            _drawer.Detail = new NavigationPage(page);
            _drawer.IsPresented = false;
        }
        #endregion
    }
}
