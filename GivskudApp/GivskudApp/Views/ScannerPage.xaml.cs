using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing;
using ZXing.Mobile;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerPage : ContentPage
	{

        public ZXing.Net.Mobile.Forms.ZXingScannerView scanner = new ZXing.Net.Mobile.Forms.ZXingScannerView();

        public ScannerPage ()
		{
			InitializeComponent ();

            Scan();
        }

        public void Scan()
        {
            try
            {
                scanner.Options = new MobileBarcodeScanningOptions()
                {
                    UseFrontCameraIfAvailable = false,
                    PossibleFormats = new List<BarcodeFormat>(),
                    TryHarder = true,
                    AutoRotate = false,
                    TryInverted = true,
                    DelayBetweenContinuousScans = 2000
                };

                scanner.IsVisible = true;
                scanner.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
                scanner.Options.PossibleFormats.Add(BarcodeFormat.DATA_MATRIX);
                scanner.Options.PossibleFormats.Add(BarcodeFormat.EAN_13);


                scanner.OnScanResult += (result) => {

                    // Stop scanning
                    scanner.IsAnalyzing = false;
                    if (scanner.IsScanning)
                    {
                        scanner.AutoFocus();
                    }

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () => {
                        if (result != null)
                        {
                            await DisplayAlert("Scan Value", result.Text, "OK");
                        }
                    });
                };

                ScannerActionPage.Children.Add(scanner, 0, 0);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }


        protected override void OnAppearing() {
            base.OnAppearing();

            scanner.IsScanning = true;
        }

        protected override void OnDisappearing() {
            scanner.IsScanning = false;

            base.OnDisappearing();
        }
    }
}