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
	public partial class CurrencyDataPage : ContentPage
	{
		public CurrencyDataPage ()
		{
			InitializeComponent ();
		    BindingContext = new CurrentDataViewModel(CurrencyTable);
        }
	}
}