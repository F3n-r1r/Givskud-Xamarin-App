using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{

        public NewsPage()
        {
            DependencyService.Register<NewsViewModel>();
            var vm = DependencyService.Get<NewsViewModel>();

            InitializeComponent();

            Device.BeginInvokeOnMainThread(() => {
                ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, true);
                NewsList.ItemsSource = vm.News;
            });

            NewsList.RefreshCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.Refresh();
                    NewsList.ItemsSource = vm.News;
                    NewsList.EndRefresh();
                });
            });
            
        }
        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            NewsModel thisNews = (NewsModel)e.Item;
            var vm = DependencyService.Get<NewsViewModel>();
            vm.SelectedNews = thisNews;
            await Navigation.PushAsync(new NewsDetailsPage());
        }
    }
}