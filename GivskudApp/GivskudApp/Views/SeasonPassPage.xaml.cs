using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Controllers;
using GivskudApp.ViewModel;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeasonPassPage : ContentPage
	{

        private SeasonPassViewModel Binding { get; set; }

		public SeasonPassPage ()
		{

            Binding = new SeasonPassViewModel();

			InitializeComponent ();
            BindingContext = Binding;

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, "There was a problem with your internet connection. Please connect your device to the internet", "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, "The pass with the ID / the pass you saved seems to be invalid. Please try again.", "invalid-pass-notification", "_VMIsPassOutdatedNotification", false);

            Binding.InitializeService();

        }
    }
}