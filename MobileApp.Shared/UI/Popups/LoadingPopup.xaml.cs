using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Shared.UI.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPopup : PopupPage
	{
		public LoadingPopup ()
		{
			InitializeComponent();
		}

	    protected override bool OnBackButtonPressed()
	    {
	        return false;
	    }
	}
}