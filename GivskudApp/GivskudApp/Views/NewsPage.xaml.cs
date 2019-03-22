﻿using GivskudApp.Models;
using GivskudApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
		public NewsPage ()
		{
            DependencyService.Register<NewsViewModel>();

            InitializeComponent ();

            var vm = DependencyService.Get<NewsViewModel>();
            NewsList.ItemsSource = vm.News;
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