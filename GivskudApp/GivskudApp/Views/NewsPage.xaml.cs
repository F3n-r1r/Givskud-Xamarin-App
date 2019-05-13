using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;

using GivskudApp.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{

        private NewsViewModel Binding { get; set; }

        public NewsPage()
        {

            Binding = new NewsViewModel(ConfigurationManager.RemoteResources.Local.News, ConfigurationManager.RemoteResources.Remote.News);

            InitializeComponent();
            BindingContext = Binding;
            
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.NotificationNoInternet, "lost-connection-notification", "_VMIsDeviceOfflineNotification", true);
            ElementsController.RenderNotification(ApplicationLayoutTopLevel, AppResources.AppResources.NotificationOutdatedContent, "cached-content-notification", "_VMIsContentOutdatedNotification", false);

            Binding.InitializeService();

            NewsList.RefreshCommand = new Command(() =>
            {
                Binding.InitializeService();
                NewsList.EndRefresh();
            });

        }
        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            NewsModel SelectedItem = e.Item as NewsModel;
            await Navigation.PushAsync(new NewsDetailsPage(SelectedItem));
        }
    }
}