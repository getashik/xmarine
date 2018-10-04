using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using CrossplatApp.Camera;
using System.IO;
using System.Reflection;
using CrossplatApp.Models;
using Newtonsoft.Json;
using mEntry = Microcharts.Entry;




namespace CrossplatApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            Slider.Value = 0;
            DisplayWchart();
        }

        public void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Text = (btn.Text == "clicked") ? "click" : "clicked";
            var MinimumDate = new DateTime(2018, 7, 15);
            var MaximumDate = new DateTime(2019, 4, 10);
            //DisplayAlert("cc", (MaximumDate - MinimumDate).TotalDays.ToString() + "----" + (MaximumDate - DateTime.Today).TotalDays, "cc");
            DisplayAlert("dd", (MaximumDate - MinimumDate).TotalDays.ToString(), "ff");


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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListsFamily();

        }

        public void Sliding(Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            sLabel.Text = string.Format("{0:F6}", e.NewValue);
        }



        private void Btn_AddBook(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewBook());
        }

        private void Btn_CustList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewList());
        }



        private async void Btn_TakePhoto(object sender, EventArgs e)
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (cameraStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                cameraStatus = results[Permission.Camera];
                if (cameraStatus != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                    //CameraButton.IsEnabled = false;
                    return;
                }
            }

            // Assures user intends to take a photo
            await DisplayAlert("Take picture confirmation", "Would you like to take a picture?", "Yes");

            // Accesses Media Plugin's TakePhotoAsync method to take a the photo
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                Directory = "Sample",
                Name = "test.jpg",
            });

            await DisplayAlert("File Location", photo.Path, "OK");

            ImageHolder.Source = ImageSource.FromStream(() =>
            {
                var stream = photo.GetStream();
                return stream;
            });
        }

        private void Btn_CamPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Campage());
        }

        private void Btn_ApiList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ApiList.ApiList());
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
    }
}
