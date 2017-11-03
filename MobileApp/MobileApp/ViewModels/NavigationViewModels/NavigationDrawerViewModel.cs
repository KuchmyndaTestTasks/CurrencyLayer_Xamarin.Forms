using MobileApp.Infrastructure.MainOperations;
using MobileApp.Views.NavigationPage;

namespace MobileApp.ViewModels.NavigationViewModels
{
    class NavigationDrawerViewModel : ViewModelBase
    {
        public NavigationDrawerViewModel(NavigationDrawer masterPage)
        {
            _masterPage = masterPage;
            _navigationListViewModel = new NavigationListViewModel(masterPage);
            _masterPage.Master = new NavigationDrawerList { BindingContext = _navigationListViewModel };
            BackgroundDownloader.Start();
        }
        #region <Fields>
        
        private readonly NavigationDrawer _masterPage;
        private readonly NavigationListViewModel _navigationListViewModel;

        #endregion

        #region <Methods>

        public void InitializePage()
        {
            _navigationListViewModel.CurrencyDataRedirector.Execute(null);
        }

        #endregion
    }
}