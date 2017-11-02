using System;
using System.Linq;
using System.Windows.Input;
using MobileApp.Abstractions;
using MobileApp.Helpers;
using MobileApp.Infrastructure;
using MobileApp.Models;
using Xamarin.Forms;

namespace MobileApp.ViewModels.Tabs_SettingViewModels
{
    //todo:need fix and ListView Template
    class CurrencySelectorViewModel : SavingViewModel, IModelChecker
    {
        public CurrencySelectorViewModel()
        {
            ClearCommand = new Command(ResetState);
            ResetState();
        }

        #region <Fields>

        private CurrencyModel[] _filteredCurrencyModels;
        private string _searchExpression;
        private CurrencyModel _selectedCurrency;
        private CurrencyModel[] _selectedCurrencies;
        private ICommand _clearCommand;

        #endregion

        #region <Properties>

        public Logger.MessageLog Message { get; set; }

        public string SearchExpression
        {
            get { return _searchExpression; }
            set
            {
                _searchExpression = value;
                FilterCurrencies();
                OnPropertyChanged();
            }
        }

        public CurrencyModel[] FilteredCurrencyModels
        {
            get { return _filteredCurrencyModels; }
            set
            {
                _filteredCurrencyModels = value;
                OnPropertyChanged();
            }
        }

        public CurrencyModel SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                AddCurrency();
                OnPropertyChanged();
            }
        }

        public CurrencyModel[] SelectedCurrencies
        {
            get { return _selectedCurrencies; }
            set
            {
                _selectedCurrencies = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClearCommand
        {
            get { return _clearCommand; }
            set
            {
                _clearCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region <Methods>

        private void AddCurrency()
        {
            if (SelectedCurrency == null) return;
            if (SelectedCurrencies == null)
                SelectedCurrencies = new CurrencyModel[0];
            else
            {
                var length = SelectedCurrencies.Length;
                var tmp = new CurrencyModel[length + 1];
                Array.Copy(SelectedCurrencies, tmp, length);
                SelectedCurrencies = tmp;
                SelectedCurrencies[SelectedCurrencies.Length - 1] =
                    FilteredCurrencyModels.First(x => x.Code == SelectedCurrency.Code);
            }
        }
        private void FilterCurrencies()
        {
            FilteredCurrencyModels =
                Parsers.ParseCurrencyModels(Application.Current.GetEmbeddedResource(CommonData.CurrenciesAssetFile));
            //1. If app isn't searching, just sorts first selected, later non-selected currencies.
            if (string.IsNullOrEmpty(SearchExpression))
            {
                var list = _filteredCurrencyModels.Where(x => x.IsSelected).ToList();
                foreach (var model in _filteredCurrencyModels)
                {
                    if (!list.Any(x => x.Code == model.Code))
                    {
                        list.Add(model);
                    }
                }
                FilteredCurrencyModels = list.ToArray();
            }
            else
            {
                //2. Filters by searching substring. Search is applying to Code and Name.
                FilteredCurrencyModels = _filteredCurrencyModels.Where(x =>
                        x.Name.ToLower().Contains(SearchExpression) || x.Code.ToLower().Contains(SearchExpression))
                    .ToArray();
            }
        }
        public bool IsValid()
        {
            return true;
        }
        public void ResetState()
        {
            FilterCurrencies();
            SelectedCurrencies = new CurrencyModel[0];
            SearchExpression = string.Empty;
        }
        #endregion
    }
}