using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Shared.Infrastructure;
using MobileApp.Shared.UI.Popups;
using MobileApp.Shared.ViewModels.MainViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.Views.CurrencyLayerPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrencyDataPage : ContentPage
	{
	    CurrencyDataViewModel context;
        public CurrencyDataPage ()
		{
			InitializeComponent ();
            context = new CurrencyDataViewModel(CurrencyTable);
		    BindingContext = context;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            context.Execute();
	    }
	}
}