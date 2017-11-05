using System.Threading.Tasks;
using MobileApp.Shared.ViewModels.MainViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.CurrencyLayerPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoricalDataPage : ContentPage
	{
		public HistoricalDataPage ()
		{
			InitializeComponent ();
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
    }
}