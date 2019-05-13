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

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationNoInternet"), "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationInvalidPass"), "invalid-pass-notification", "_VMIsPassOutdatedNotification", false);

            Binding.InitializeService();

        }
    }
}