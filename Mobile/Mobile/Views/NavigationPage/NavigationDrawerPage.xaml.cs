using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.NavigationPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationDrawerPage : MasterDetailPage
    {
        public NavigationDrawerPage()
        {
            InitializeComponent();
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /*var item = e.SelectedItem as NavigationDrawerPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new Xamarin.Forms.NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;*/
        }
    }
}