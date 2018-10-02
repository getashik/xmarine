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



namespace CrossplatApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Slider.Value = 0;




        }
        public void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Text = (btn.Text == "clicked") ? "click" : "clicked";


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetListData();

        }

        public void Sliding(Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            sLabel.Text = string.Format("{0:F6}", e.NewValue);
        }

        public void GetListData()
        {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {

                conn.CreateTable<Book>();
                var books = conn.Table<Book>().ToList();
                bookList.ItemsSource = books;

            }

        }

        private void Btn_AddBook(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewBook());
        }

        private void Btn_CustList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewList());
        }

        private void Btn_ListDelete(object sender, EventArgs e)
        {
            var button = (Image)sender;
            var bc = button.BindingContext as Book;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {

                conn.Delete<Book>(bc.Id);
                DisplayAlert("Success", "Deleted " + bc.Name, "ok");
                GetListData();


            }


        }

        private void Btn_ItemEdit(object sender, EventArgs e)
        {
            DisplayAlert("Success", "Edit Pressed ", "ok");
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
    }
}
