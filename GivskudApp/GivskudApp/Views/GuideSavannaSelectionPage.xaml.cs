using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuideSavannaSelectionPage : ContentPage
	{
		public GuideSavannaSelectionPage ()
		{
			InitializeComponent ();
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);
        }
        async void BtnClicked_GoToSavanna(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if (id == "sav01")
            {
                await Navigation.PushAsync(new GuideSavannaPage(1136));
            }
            else if (id == "sav02")
            {
                await Navigation.PushAsync(new GuideSavannaPage(1137));
            } else if (id == "sav03")
            {
                await Navigation.PushAsync(new GuideSavannaPage(1138));
            }
        }
    }
}