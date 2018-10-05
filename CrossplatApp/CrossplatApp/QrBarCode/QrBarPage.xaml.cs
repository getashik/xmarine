using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CrossplatApp.QrBarCode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrBarPage : ContentPage
    {
        public QrBarPage()
        {
            InitializeComponent();
        }

        private async void Btn_Scan(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (rst) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    UiQrText.Text = rst.Text;

                });

            };
        }
    }
}