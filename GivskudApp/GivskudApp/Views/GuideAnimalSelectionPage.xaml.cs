using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLToolkit.Forms.Controls;

using GivskudApp.Models;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;
using GivskudApp.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuideSavannaPage : ContentPage
	{

        private AnimalViewModel Binding { get; set; }
       
        public GuideSavannaPage (int id)
		{

            Binding = new AnimalViewModel(ConfigurationManager.RemoteResources.Local.Animals, ConfigurationManager.RemoteResources.Remote.Animals, true, id.ToString());

            InitializeComponent();
            FlowListView.Init();

            BindingContext = Binding;

            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel);

            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationNoInternet"), "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationOutdatedContent"), "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

        }
        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel Item = e.Item as AnimalModel;
            await Navigation.PushAsync(new AnimalGuidePage(Item));
        }
    }
}