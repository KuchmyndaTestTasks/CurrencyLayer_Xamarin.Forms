using Xamarin.Forms;

namespace Mobile
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new Mobile.Views.NavigationPage.NavigationDrawerPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
