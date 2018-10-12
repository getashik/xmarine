using CrossplatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossplatApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetail : MasterDetailPage
    {
        public List<MasterMenu> MasterMenuList = new List<MasterMenu> { };

        public MasterDetail()
        {


            InitializeComponent();
            var settings = new ToolbarItem
            {
                Icon = "logout"

            };
            settings.Clicked += (sender, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            };

            this.ToolbarItems.Add(settings);

            MasterMenuList.Add(new MasterMenu()
            {
                Title = "Home",
                Icon = "home",
                TargetType = typeof(HomePage)
            });

            MasterMenuList.Add(new MasterMenu()
            {
                Title = "Add Book(Sqlite)",
                Icon = "book",
                TargetType = typeof(NewBook)
            });
            MasterMenuList.Add(new MasterMenu()
            {
                Title = "Car List(Object List)",
                Icon = "car",
                TargetType = typeof(NewList)
            });

            MasterMenuList.Add(new MasterMenu()
            {
                Title = "List From API",
                Icon = "www",
                TargetType = typeof(ApiList)
            });
            MasterMenuList.Add(new MasterMenu()
            {
                Title = "Camera",
                Icon = "camera3",
                TargetType = typeof(Campage)
            });
            MasterMenuList.Add(new MasterMenu()
            {
                Title = "Qr/Bar Code Scan",
                Icon = "qr",
                TargetType = typeof(QrBarPage)
            });

            MasterMenuListHolder.ItemsSource = MasterMenuList;


            Detail = new NavigationPage(new HomePage());
            IsPresented = false;


        }

        private void Btn_Books(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new NewBook());
            IsPresented = false;

        }

        private void Btn_ObjectLis(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new NewList());
            IsPresented = false;

        }

        private void Btn_Home(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void OnMenuSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterMenu)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}