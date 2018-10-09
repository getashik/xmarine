using CrossplatApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mEntry = Microcharts.Entry;

namespace CrossplatApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            DisplayWchart();
            ListsFamily();
        }
        public void DisplayWchart()
        {

            List<mEntry> entries = new List<mEntry>
            {
                //new mEntry(200) {
                //Label = "January",
                //ValueLabel = "200",
                //Color = SkiaSharp.SKColor.Parse("FF0000")
                //},
                ////new mEntry(400) {
                ////Label = "February",
                ////ValueLabel = "400",
                ////Color = SkiaSharp.SKColor.Parse("#00bfff")
                ////},
                //new mEntry(200) {
                //Label = "March",
                //ValueLabel = "-100",
                //Color = SkiaSharp.SKColor.Parse("#90D585")
                //}
            };

            var MinimumDate = new DateTime(2018, 7, 15);
            var MaximumDate = new DateTime(2019, 4, 10);
            // DisplayAlert("cc", (MaximumDate - MinimumDate).TotalDays.ToString() + "----" + (MaximumDate - DateTime.Today).TotalDays, "cc");

            int tw = System.Convert.ToInt32((MaximumDate - DateTime.Today).TotalDays.ToString());
            int wd = System.Convert.ToInt32((DateTime.Today - MinimumDate).TotalDays.ToString());

            entries.Add(new mEntry(tw)
            {
                Label = "Yet",
                ValueLabel = tw.ToString(),
                Color = SkiaSharp.SKColor.Parse("#00bfff")
            });

            entries.Add(new mEntry(wd)
            {
                Label = "Done",
                ValueLabel = wd.ToString(),
                Color = SkiaSharp.SKColor.Parse("#008000")
            });
            wChart.Chart = new Microcharts.DonutChart
            {
                Entries = entries,

            };



        }

        public void ListsFamily()
        {

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CrossplatApp.my_file.json");

            // Family[] Families;
            using (var reader = new System.IO.StreamReader(stream))
            {
                base.OnAppearing();

                var json = reader.ReadToEnd();
                var familyArr = JsonConvert.DeserializeObject<List<Family>>(json);
                FamilyViewList.ItemsSource = familyArr;
            }

        }

    }
}