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
	public partial class ContactPage : ContentPage
	{
		public ContactPage ()
		{
			InitializeComponent ();
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);
        }
        public async void MapButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

    }
}