using DLToolkit.Forms.Controls;
using GivskudApp.Models;
using GivskudApp.ViewModel;

using GivskudApp.Resources;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalsPage : ContentPage
	{

        private AnimalViewModel Binding { get; set; }

	    public AnimalsPage ()
	    {
         
            Binding = new AnimalViewModel(ConfigurationManager.RemoteResources.Local.Animals, ConfigurationManager.RemoteResources.Remote.Animals);

            InitializeComponent();
            FlowListView.Init();

            BindingContext = Binding;

            

            // AppResources.AppResources.NotificationNoInternet
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationNoInternet"), "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationOutdatedContent"), "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

        }

        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel thisAnimal = (AnimalModel)e.Item;
            await Navigation.PushAsync(new AnimalDetailsPage(thisAnimal));
        }
    }

}
