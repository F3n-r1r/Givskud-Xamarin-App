using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Resources;

using GivskudApp.Models;
using GivskudApp.Controllers;
using GivskudApp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalGuidePage : ContentPage
	{

        private GuideAnimalViewModel Binding { get; set; }

		public AnimalGuidePage (AnimalModel SelectedAnimal)
		{

            Binding = new GuideAnimalViewModel(ConfigurationManager.RemoteResources.Local.Quizes, ConfigurationManager.RemoteResources.Remote.Quizes, SelectedAnimal, true, SelectedAnimal.QuizID);

            InitializeComponent();

            BindingContext = Binding;

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationNoInternet"), "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("NotificationOutdatedContent"), "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);
            
            Binding.InitializeService();

            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel, AppResources.AppResources.ResourceManager.GetString("AnimalGuidePageHeadline01") + " " + Binding.AnimalData.Name + ". " + AppResources.AppResources.ResourceManager.GetString("AnimalGuidePageHeadline02"));

        }
        async void BtnClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if(id == "AnimalInfoBtn")
            {
                await Navigation.PushAsync(new AnimalDetailsPage(Binding.AnimalData));
            } else if (id == "AnimalMapBtn")
            {
                await Navigation.PushAsync(new MapPage());
            } else if (id == "AnimalQuizBtn")
            {
                await Navigation.PushAsync(new QuizIngamePage(Binding.QuizData));
            }
        }
    }
}