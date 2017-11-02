using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
        protected bool IsFinished;

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
                if (IsFinished)
                {
                    Application.Current.RedirectTo(new NavigationDrawer(), false);
                }
            });
        }

        #endregion
    }
}
