using Mobile.Views.NavigationPage;

namespace Mobile.ViewModels.NavigationViewModels
{
    class NavigationDrawerViewModel:ViewModelBase
    {
        private NavigationDrawer _masterPage;
        private NavigationListViewModel _navigationListViewModel;
        public NavigationDrawerViewModel(NavigationDrawer masterPage)
        {
            _masterPage = masterPage;
            _navigationListViewModel=new NavigationListViewModel(masterPage);
            _masterPage.Master = new NavigationDrawerList {BindingContext = _navigationListViewModel};
        }

        public void InitializePage()
        {
            _navigationListViewModel.CurrencyDataRedirector.Execute(null);
        }
    }
}
