﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using MobileApp.Abstractions;
using MobileApp.Infrastructure;
using MobileApp.Infrastructure.MainOperations;
using MobileApp.Models;
using Xamarin.Forms;

namespace MobileApp.ViewModels.MainViewModels
{
    class CurrentDataViewModel : ViewModelBase, IInitializationViewModel
    {
        /// <summary>
        /// ViewModel for CurrentDataPage.xaml
        /// </summary>
        public CurrentDataViewModel(Grid grid) : base()
        {
            _grid = grid;
            BackgroundDownloader.UpdateEvent += Execute;
            Execute();
        }

        ~CurrentDataViewModel()
        {
            BackgroundDownloader.UpdateEvent -= Execute;
        }

        #region <Fields>

        /// <summary>
        /// Selected Currencies
        /// </summary>
        private ObservableCollection<CurrencyModel> _currencyModels;

        /// <summary>
        /// Ratios matrix with currencies by rating.
        /// </summary>
        private double[,] _rates;

        /// <summary>
        /// Grid for generating controls from code and presents _rates[,].
        /// </summary>
        private readonly Grid _grid;


        /// <summary>
        /// Property for blocking UI or etc
        /// </summary>
        private bool _isEnabled;

        /// <summary>
        /// Real-Time ratings of currencies from API/from local DB in Offline-mode
        /// </summary>
        ApiCurrencyModel _liveCurrencyModel;

        #endregion

        #region <Properties>

        public bool IsLoading
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
            InitializeModels();
            var apiCurrencyModels = CurrencyLayerApplication.HistoricalData;
            _liveCurrencyModel = apiCurrencyModels.First(x => x.Key.Ticks == apiCurrencyModels.Max(t => t.Key.Ticks))
                .Value;
        }

        /// <summary>
        /// Initializes currency models.
        /// </summary>
        private void InitializeModels()
        {
            _currencyModels = new ObservableCollection<CurrencyModel>(CurrencyLayerApplication.CurrencyModels);
        }

        /// <summary>
        /// Executes main task.
        /// </summary>
        protected void Execute()
        {
            Initialize();
            Calculation();
            Device.BeginInvokeOnMainThread(InitializeGrid);
        }

        /// <summary>
        /// Calculates ratios between currencies.
        /// </summary>
        private void Calculation()
        {
            var dictionary = _liveCurrencyModel.Currencies;
            var size = dictionary.Count;
            _rates = new double[size, size];
            int i = 0;
            //Expample: each code are rated to USD
            //         "USD": 1,
            //         "EUR": 0.84,
            //         "UAH": 25.6
            //         "ILS": 8.3
            //    In an each loop it сalculates ratios between currencies by 
            //    derivative rating from inner loop to rationg from outer loop
            //    
            //  code1[i]\code2[j]    USD         EUR         UAH         ILS     
            //          USD         USD/USD     EUR/USD     UAH/USD     ILS/USD
            //          EUR         USD/EUR     ......                  ILS/EUR
            //          UAH          ....       ......      ......             
            //          ILS         USD/ILS     ......      UAH/ILS     ......
            //
            // Formula:  _rates[j,i]= code2[j]/code[1];
            //
            foreach (var code1 in dictionary)
            {
                var j = 0;
                foreach (var code2 in dictionary)
                {
                    //If code1==0, set 1 for avoiding dividing on zero. Need for apllying changes in Settings in Offline/mode.
                    var number = code1.Value == 0 ? 1 : code1.Value;
                    _rates[i, j++] = Math.Round(code2.Value / number, 5);
                }
                i++;
            }
        }

        #endregion

        #region <Additional>

        /// <summary>
        /// Initializes Grid UI by currencies
        /// </summary>
        private void InitializeGrid()
        {
            //Matrix which each row is Grid.Row
            Tuple<CurrencyModel, ExchangeModel[]>[] rates =
                new Tuple<CurrencyModel, ExchangeModel[]>[_currencyModels.Count];
            ClearGridContent();
            CreateGridMatrix(rates);

            for (int i = 0; i < rates.Length; i++)
            {
                CreateHeaderCell(rates, i);
                CreateGridCell(rates, i);
            }
        }

        /// <summary>
        /// Creates cell UI, and inseting into Grid.
        /// With more logics in pushing inside components.
        /// </summary>
        /// <param name="rates">matrix</param>
        /// <param name="index">row</param>
        private void CreateGridCell(Tuple<CurrencyModel, ExchangeModel[]>[] rates, int index)
        {
            for (int j = 0; j < rates[index].Item2.Length; j++)
            {
                var panel = GetStackPanel();
                panel.Children.Add(GetTextBlock(Math.Round(rates[index].Item2[j].Rating, 5).ToString(), Color.Black));
                if (index != j)
                {
                    panel.Children.Add(GetTextBlock(Math.Round(rates[j].Item2[index].Rating, 5).ToString(),
                        Color.DarkGray));
                }
                var panelmain = GetPanel(panel);
                //Sets background color for odd and even rows.
                panelmain.BackgroundColor = index % 2 != 0 ? Color.White : Color.LightGray;
                SetCellIndexes(panelmain, index + 1, j + 1);
                _grid.Children.Add(panelmain);
            }
        }

        /// <summary>
        /// Creates header cell UI, and inseting into Grid.
        /// With more logics in pushing inside components.
        /// </summary>
        /// <param name="rates">matrix</param>
        /// <param name="index">row</param>
        private void CreateHeaderCell(Tuple<CurrencyModel, ExchangeModel[]>[] rates, int index)
        {
            var rowheader = GetStackPanel();
            var rowpicturedPanel = GetStackPanel();
            rowpicturedPanel.Orientation = StackOrientation.Horizontal;
            rowpicturedPanel.Children.Add(new Image() {Source = Helpers.UiHelpers.GetImage(rates[index].Item1)});
            rowpicturedPanel.Children.Add(GetTextBlock($"1 {rates[index].Item1.Code}", Color.Gray));
            rowheader.Children.Add(rowpicturedPanel);
            rowheader.Children.Add(GetTextBlock("Inverse:", Color.Gray));
            var colheader = GetStackPanel();
            colheader.Orientation = StackOrientation.Horizontal;
            colheader.Children.Add(new Image() {Source = Helpers.UiHelpers.GetImage(rates[index].Item1)});
            colheader.Children.Add(GetTextBlock($"{rates[index].Item1.Code}", Color.Gray));

            var panRowHeader = GetPanel(rowheader);
            SetCellIndexes(panRowHeader, index + 1, 0);
            var panColHeader = GetPanel(colheader);
            SetCellIndexes(panColHeader, 0, index + 1);
            panColHeader.BackgroundColor = Color.LightGray;
            panRowHeader.BackgroundColor = Color.LightGray;
            _grid.Children.Add(panColHeader);
            _grid.Children.Add(panRowHeader);
        }

        /// <summary>
        /// Sets for element indexes of Grid Matrix. Grid.Row[i].Column[j]
        /// </summary>
        /// <param name="element">UI component</param>
        /// <param name="row">row number</param>
        /// <param name="col">column number</param>
        private static void SetCellIndexes(View element, int row, int col)
        {
            element.SetValue(Grid.RowProperty, row);
            element.SetValue(Grid.ColumnProperty, col);
        }

        /// <summary>
        /// Creates grid matrix : initializes Grid.RowDefinitions and Grid.ColumnDefinitions.
        /// </summary>
        /// <param name="rates"></param>
        private void CreateGridMatrix(Tuple<CurrencyModel, ExchangeModel[]>[] rates)
        {
            if (_rates == null) return;

            for (var i = 0; i <= _currencyModels.Count; i++)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)});
                _grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
                if (i < _currencyModels.Count)
                {
                    rates[i] = new Tuple<CurrencyModel, ExchangeModel[]>(_currencyModels[i],
                        new ExchangeModel[_currencyModels.Count]);
                    for (int j = 0; j < rates[i].Item2.Length; j++)
                    {
                        rates[i].Item2[j] = new ExchangeModel
                        {
                            Code = _currencyModels[j].Code,
                            Rating = _rates[i, j]
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Clears all childrens in Grid.
        /// </summary>
        private void ClearGridContent()
        {
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
        }

        #endregion

        #region <Template methods>

        private Label GetTextBlock(string text, Color brush)
        {
            return new Label()
            {
                Text = text,
                TextColor = brush
            };
        }

        private StackLayout GetPanel(params View[] children)
        {
            var panel = new StackLayout();
            foreach (var child in children)
            {
                panel.Children.Add(child);
            }
            return panel;
        }

        private StackLayout GetStackPanel()
        {
            return new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
        }

        #endregion
    }
}
