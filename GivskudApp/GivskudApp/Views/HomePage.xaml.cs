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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
            ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, false);
        }

        // Handle button click -> Push new page
        async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if (id == "TourPageBtn")
            {
                await Navigation.PushAsync(new GuidePage());
            }
            else if (id == "MapPageBtn")
            {
                await Navigation.PushAsync(new MapPage());
            }
            else if (id == "ProgramPageBtn")
            {
                await Navigation.PushAsync(new ProgramPage());
            }

        }
    }
}