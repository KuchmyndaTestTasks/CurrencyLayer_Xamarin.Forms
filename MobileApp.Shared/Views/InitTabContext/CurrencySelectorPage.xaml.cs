using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.InitTabContext
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencySelectorPage
    {
        public CurrencySelectorPage()
        {
            InitializeComponent();
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