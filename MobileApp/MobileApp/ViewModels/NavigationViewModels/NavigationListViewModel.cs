﻿using System.Windows.Input;
using MobileApp.Views.CurrencyLayerPages;
using MobileApp.Views.NavigationPage;
using Xamarin.Forms;

namespace MobileApp.ViewModels.NavigationViewModels
{
    class NavigationListViewModel : ViewModelBase
    {
        #region <Constructors>

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

        #endregion

        #region <Fields>

        private readonly NavigationDrawer _drawer;
        private ICommand _currencyDataRedirector;
        private ICommand _historicalDataRedirector;
        private ICommand _exchangeDataRedirector;

        #endregion

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
            PutPageToNavDrawer(new ConvertingDataPage());
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