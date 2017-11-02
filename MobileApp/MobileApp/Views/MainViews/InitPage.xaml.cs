using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.ViewModels.Tabs_SettingViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.MainViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitPage : TabbedPage
    {
        public InitPage ()
        {
            InitializeComponent();
        }
    }
}