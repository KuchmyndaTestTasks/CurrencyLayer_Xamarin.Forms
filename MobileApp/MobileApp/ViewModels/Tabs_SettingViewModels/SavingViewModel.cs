using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MobileApp.Global;
using MobileApp.Infrastructure;
using MobileApp.Views.NavigationPage;
using Xamarin.Forms;

namespace MobileApp.ViewModels.Tabs_SettingViewModels
{
    class SavingViewModel : ViewModelBase
    {
        public SavingViewModel()
        {
            InitCommand();
        }

        #region <Fields>

        private ICommand _saveCommand;
        protected Action Action;

        protected static readonly Dictionary<string, bool> FinishedMap = new Dictionary<string, bool>
        {
            {nameof(SettingViewModel), false},
            {nameof(CurrencySelectorViewModel), false}
        };

        #endregion

        #region <Properties>

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                _saveCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region <Methods>

        private void InitCommand()
        {
            SaveCommand = new Command(() =>
            {
                Action?.Invoke();
                if (FinishedMap.All(x=>x.Value))
                {
                    Application.Current.RedirectTo(new NavigationDrawer(), false);
                }
            });
        }

        #endregion
    }
}
