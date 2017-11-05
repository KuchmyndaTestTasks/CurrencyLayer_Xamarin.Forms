using MobileApp.Shared.Abstractions;
using MobileApp.Shared.Infrastructure.MainOperations;
using MobileApp.Shared.Views.NavigationPage;

namespace MobileApp.Shared.ViewModels.NavigationViewModels
{
    class NavigationDrawerViewModel : ViewModelBase
    {
        public NavigationDrawerViewModel(NavigationDrawer masterPage)
        {
            _masterPage = masterPage;
            _navigationListViewModel = new NavigationListViewModel(masterPage);
            _masterPage.Master = new NavigationDrawerList { BindingContext = _navigationListViewModel };
            
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