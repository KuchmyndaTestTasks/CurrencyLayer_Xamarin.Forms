using MobileApp.Shared.Infrastructure;
using MobileApp.Shared.Infrastructure.MainOperations;
using Xamarin.Forms;

namespace MobileApp.Shared
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CurrencyLayerApplication.InitPage();
        }

        ~App()
        {
            BackgroundDownloader.Abort();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
        }
    }
}