using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Controllers;
using GivskudApp.Resources;
using GivskudApp.ViewModel;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgramPage : ContentPage
	{

        private ProgramViewModel Binding { get; set; }

		public ProgramPage ()
		{

            Binding = new ProgramViewModel(ConfigurationManager.RemoteResources.Local.Events, ConfigurationManager.RemoteResources.Remote.Events);

            InitializeComponent();
            BindingContext = Binding;

            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationNoInternet"), "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationOutdatedContent"), "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

        }
        public async void OnMapPointClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

    }
}