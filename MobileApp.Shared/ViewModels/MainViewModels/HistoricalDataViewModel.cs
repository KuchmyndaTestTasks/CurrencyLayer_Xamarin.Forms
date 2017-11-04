﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MobileApp.Shared.Abstractions;
using MobileApp.Shared.Infrastructure;
using MobileApp.Shared.Infrastructure.MainOperations;
using MobileApp.Shared.Models;

namespace MobileApp.Shared.ViewModels.MainViewModels
{
    public class HistoricalDataViewModel : ViewModelBase, IInitializationViewModel
    {
        public HistoricalDataViewModel()
        {
            BackgroundDownloader.UpdateEvent += Execute;
            Execute();
        }

        ~HistoricalDataViewModel()
        {
            BackgroundDownloader.UpdateEvent -= Execute;
        }

        #region <Fields>

        /// <summary>
        /// Selected currencies
        /// </summary>
        private ObservableCollection<CurrencyModel> _currencyModels;

        /// <summary>
        /// Header for Y-axis
        /// </summary>
        private string _description;

        private string _message;

        /// <summary>
        /// Currency Points (datetime, rating)
        /// </summary>
        private ObservableCollection<DatePoint> _chart;

        private CurrencyModel _currencyModelFrom;
        private CurrencyModel _currencyModelTo;

        /// <summary>
        /// Colection of historical data (datetime, currencies[])
        /// </summary>
        private Dictionary<DateTime, ApiCurrencyModel> _historicalData;

        #endregion

        #region <Properties>

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DatePoint> Chart
        {
            get { return _chart; }
            set
            {
                _chart = value;
                OnPropertyChanged();
            }
        }

        public CurrencyModel CurrencyModelFrom
        {
            get { return _currencyModelFrom; }
            set
            {
                _currencyModelFrom = value;
                OnPropertyChanged();
            }
        }

        public CurrencyModel CurrencyModelTo
        {
            get { return _currencyModelTo; }
            set
            {
                _currencyModelTo = value;
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


        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region <Methods> 

        public bool IsLoading { get; set; }

        /// <summary>
        /// Initializes data & contexts by selected currencies.
        /// It need after apllying changes in Settings (changing currency selections)
        /// </summary>
        public void Initialize()
        {
            InitializeModels();
            CurrencyModelFrom = CurrencyModels.First();
            CurrencyModelTo = CurrencyModels.Last();
        }

        /// <summary>
        /// Initializes and draws chart
        /// </summary>
        private void InitializeChart()
        {
            //For avoiding bugs & null collections/values
            if (!CurrencyModels.Any() || CurrencyModelFrom == null || CurrencyModelTo == null ||
                _historicalData == null)
                return;
            Description = $"{_currencyModelFrom.Code}/{_currencyModelTo.Code}";
            _chart = new ObservableCollection<DatePoint>();
            foreach (var model in _historicalData)
            {
                _chart.Add(new DatePoint
                {
                    Date = model.Key.ToString("dd/MM/yyyy"),
                    Rating = model.Value.Currencies[_currencyModelFrom.Code] /
                             model.Value.Currencies[_currencyModelTo.Code]
                });
            }
            Chart = _chart;
        }

        /// <summary>
        /// Initializes models after applyying changes in Setting tab.
        /// </summary>
        private void InitializeModels()
        {
            CurrencyModels =
                new ObservableCollection<CurrencyModel>(CurrencyLayerApplication.CurrencyModels);
            _historicalData = CurrencyLayerApplication.HistoricalData;
        }

        /// <summary>
        /// Executes main task.
        /// </summary>
        protected void Execute()
        {
            if (Settings.Instance.IsConfigured)
            {
                Initialize();
                CheckSelectedModels();
                InitializeChart();
            }
        }

        /// <summary>
        /// Checks models are null after applying changes in Setting tab. 
        /// </summary>
        private void CheckSelectedModels()
        {
            var currencies = _historicalData.First().Value.Currencies;
            //For avoiding bugs & null collections/values. 
            //Also checking containing some curency in last updated historical data.
            if ((_currencyModelFrom == null || _currencyModelTo == null)
                && _currencyModels.Count == currencies.Values.Count
                && _currencyModels.Any(x => currencies.First(t => t.Key == x.Code).Key == x.Code)
                && _currencyModels.Any(x => currencies.Last(t => t.Key == x.Code).Key == x.Code))
            {
                CurrencyModelFrom =
                    _currencyModels.First(x => currencies.First(t => t.Key == x.Code).Key == x.Code);
                CurrencyModelTo =
                    _currencyModels.First(x => currencies.First(t => t.Key == x.Code).Key == x.Code);
            }

        }

        #endregion

        #region <Additional>

        public struct DatePoint
        {
            public string Date { get; set; }
            public double Rating { get; set; }
        }

        #endregion
    }
}