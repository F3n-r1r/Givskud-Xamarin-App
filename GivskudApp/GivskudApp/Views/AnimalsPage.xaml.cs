using DLToolkit.Forms.Controls;
using GivskudApp.Models;
using GivskudApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, "There was a problem with your internet connection. Please connect your device to the internet", "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, "The content used is outdated. Please connect your device to the internet to see the newest content", "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

        }

        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel thisAnimal = (AnimalModel)e.Item;
            await Navigation.PushAsync(new AnimalDetailsPage(thisAnimal));
        }
    }

}
