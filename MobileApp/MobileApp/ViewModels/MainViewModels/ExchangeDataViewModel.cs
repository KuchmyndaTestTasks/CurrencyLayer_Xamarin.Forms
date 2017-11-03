﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using MobileApp.Abstractions;
using MobileApp.Helpers;
using MobileApp.Infrastructure;
using MobileApp.Infrastructure.MainOperations;
using MobileApp.Models;

namespace MobileApp.ViewModels.MainViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// ViewModel for CurrentDataPage.xaml
    /// </summary>
    internal class ExchangeDataViewModel : ViewModelBase, IInitializationViewModel
    {
        public ExchangeDataViewModel()
        {
            _currencyValue = 1;
            BackgroundDownloader.UpdateEvent += Execute;
            Execute();
        }

        ~ExchangeDataViewModel()
        {
            BackgroundDownloader.UpdateEvent -= Execute;
        }

        #region <Fields>

        /// <summary>
        /// Value which we want to convert to other currencies.
        /// </summary>
        private double _currencyValue;

        /// <summary>
        /// General selected currencies.
        /// </summary>
        private ObservableCollection<CurrencyModel> _currencyModels;

        /// <summary>
        /// Selected curency which we want to get converted values in other values.
        /// </summary>
        private CurrencyModel _selectedCurrencyModel;

        /// <summary>
        /// Converted currencies without _selectedCurrencyModel.
        /// </summary>
        private ObservableCollection<ExchangeModel> _exchangeModels;

        /// <summary>
        /// Property for blocking UI or etc
        /// </summary>
        private bool _isEnabled;

        private ApiCurrencyModel _liveCurrencyModel;

        #endregion

        #region <Properties>

        public double CurrencyValue
        {
            get { return _currencyValue; }
            set
            {
                _currencyValue = value;
                Calculation();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CurrencyModel> CurrencyModels
        {
            get { return _currencyModels; }
            set
            {
                _currencyModels = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ExchangeModel> ExchangeModels
        {
            get { return _exchangeModels; }
            set
            {
                _exchangeModels = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading { get; set; }

        public CurrencyModel SelectedCurrencyModel
        {
            get { return _selectedCurrencyModel; }
            set
            {
                _selectedCurrencyModel = value;
                Calculation();
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region <Methods>

        /// <summary>
        /// Initializes data & contexts by selected currencies.
        /// It need after apllying changes in Settings (changing currency selections)
        /// </summary>
        public void Initialize()
        {
            var apiCurrencyModels = CurrencyLayerApplication.HistoricalData;
            _liveCurrencyModel = apiCurrencyModels.First(x => x.Key.Ticks == apiCurrencyModels.Max(t => t.Key.Ticks))
                .Value;
            CurrencyModels = new ObservableCollection<CurrencyModel>(CurrencyLayerApplication.CurrencyModels);
            if (CurrencyModels != null && CurrencyModels.Any())
            {
                SelectedCurrencyModel = _currencyModels.First();
            }
        }

        /// <summary>
        /// Executes main task.
        /// </summary>
        protected void Execute()
        {
            Initialize();
            Calculation();
            CurrencyLayerApplication.ThreadSleep(1);
        }

        /// <summary>
        /// Converts value for other currencies.
        /// </summary>
        private void Calculation()
        {
            foreach (var quote in _liveCurrencyModel.Currencies)
            {
                if (CurrencyModels.Any(x => x.Code == quote.Key))
                {
                    CurrencyModels.First(x => x.Code == quote.Key).Rating = quote.Value;
                }
            }

            //Filter currencies without CurrencyModel
            var forCalculating = CurrencyModels.Where(x => x.Code != SelectedCurrencyModel.Code);
            SelectedCurrencyModel.Rating = CurrencyModels.First(x => x.Code == SelectedCurrencyModel.Code).Rating;

            //Converting. Formula: ConvertedCurrency[i] =  (CurrencyValue * CurrencyModels[i])/ SelectedCurrency.
            ExchangeModels = new ObservableCollection<ExchangeModel>(forCalculating.Select(x =>
                x.ToExchangeModel(Math.Round(CurrencyValue / SelectedCurrencyModel.Rating, 4))));
        }

        #endregion

    }
}