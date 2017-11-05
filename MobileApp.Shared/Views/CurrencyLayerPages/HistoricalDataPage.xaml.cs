using System.Threading.Tasks;
using MobileApp.Shared.ViewModels.MainViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.CurrencyLayerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoricalDataPage : ContentPage
    {
        public HistoricalDataPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() =>
            {
                var vm = (HistoricalDataViewModel) BindingContext;
                vm.Execute();
            });
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            MainLayout.Orientation = Device.Info.CurrentOrientation == DeviceOrientation.Landscape
                ? StackOrientation.Horizontal
                : StackOrientation.Vertical;
        }
    }
}