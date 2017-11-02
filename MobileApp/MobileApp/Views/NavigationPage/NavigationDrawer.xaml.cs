using MobileApp.ViewModels.NavigationViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.NavigationPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationDrawer : MasterDetailPage
    {
        public NavigationDrawer()
        {
            var context = new NavigationDrawerViewModel(this);
            BindingContext = context;
            InitializeComponent();
            context.InitializePage();
        }
    }
}