using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLToolkit.Forms.Controls;

using GivskudApp.Models;
using GivskudApp.Resources;
using GivskudApp.Controllers;
using GivskudApp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizSelectionPage : ContentPage
	{

        private QuizViewModel Binding { get; set; }
        
		public QuizSelectionPage ()
		{

            Binding = new QuizViewModel(ConfigurationManager.RemoteResources.Local.Quizes, ConfigurationManager.RemoteResources.Remote.Quizes);

            InitializeComponent();
            FlowListView.Init();

            BindingContext = Binding;

            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel);

            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.NotificationNoInternet, "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.NotificationOutdatedContent, "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

        }
        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            QuizModel Item = e.Item as QuizModel;
            await Navigation.PushAsync(new QuizIngamePage(Item));
        }
    }
}