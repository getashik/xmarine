using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CrossplatApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Campage : ContentPage
    {
        public Campage()
        {
            InitializeComponent();
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
    }
}