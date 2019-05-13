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
	public partial class GuidePage : ContentPage
	{

        GuiInstanceController.AnnaGuiInstance AnnaOverlay;

        public GuidePage ()
		{

			InitializeComponent ();
            AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel, AppResources.AppResources.GuidedTourIntroduction);

        }

        // Handle button click -> Push new page
        async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if (id == "GuideSavannaPageBtn")
            {
                await Navigation.PushAsync(new GuideSavannaSelectionPage());
            }
            else if (id == "QuizPageBtn")
            {
                await Navigation.PushAsync(new QuizSelectionPage());
            }
        }

    }
}