using MobileApp.Shared.Infrastructure.MainOperations;
using MobileApp.Shared.ViewModels.NavigationViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.NavigationPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationDrawer : MasterDetailPage
    {NavigationDrawerViewModel context;
        public NavigationDrawer()
        {
            context = new NavigationDrawerViewModel(this);
            BindingContext = context;
            InitializeComponent();
            context.InitializePage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundDownloader.Init();
        }
    }
}