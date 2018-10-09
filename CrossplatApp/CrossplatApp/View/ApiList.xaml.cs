using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;
using CrossplatApp.Models;

namespace CrossplatApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApiList : ContentPage
    {

        public ApiList()
        {
            InitializeComponent();

            getJsonData();
        }

        public async void getJsonData()
        {

            //http://mysafeinfo.com/api/data?list=englishmonarchs&format=json
            if (CrossConnectivity.Current.IsConnected)
            {
                var httPConn = new HttpClient();
                var resp = await httPConn.GetAsync("http://mysafeinfo.com/api/data?list=englishmonarchs&format=json");
                string respData = await resp.Content.ReadAsStringAsync();
                var ObjModelArray = JsonConvert.DeserializeObject<List<ApiModel>>(respData);
                APiViewList.ItemsSource = ObjModelArray;
            }
            else
            {

                await DisplayAlert("Error", "No Internet", "Ok");

            }
            ProgressLoader.IsVisible = false;

        }
    }
}