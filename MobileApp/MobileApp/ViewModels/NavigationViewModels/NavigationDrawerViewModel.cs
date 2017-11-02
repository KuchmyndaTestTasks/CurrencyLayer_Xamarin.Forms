using MobileApp.Views.NavigationPage;

namespace MobileApp.ViewModels.NavigationViewModels
{
    class NavigationDrawerViewModel : ViewModelBase
    {
        #region <Fields>

        private NavigationDrawer _masterPage;
        private NavigationListViewModel _navigationListViewModel;

        #endregion

        #region <Methods>

        public NavigationDrawerViewModel(NavigationDrawer masterPage)
        {
            _masterPage = masterPage;
            _navigationListViewModel = new NavigationListViewModel(masterPage);
            _masterPage.Master = new NavigationDrawerList {BindingContext = _navigationListViewModel};
        }

        public void InitializePage()
        {
            _navigationListViewModel.CurrencyDataRedirector.Execute(null);
        }

        #endregion
    }
}