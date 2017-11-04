using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Shared.ViewModels.MainViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.CurrencyLayerPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConvertingDataPage : ContentPage
	{
		public ConvertingDataPage ()
		{
			InitializeComponent ();
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        var vm = (ExchangeDataViewModel)BindingContext;
            vm.Execute();
	    }
	}
}