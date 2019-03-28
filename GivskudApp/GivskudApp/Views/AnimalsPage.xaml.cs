using DLToolkit.Forms.Controls;
using GivskudApp.Models;
using GivskudApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalsPage : ContentPage
	{
		public AnimalsPage ()
		{
            DependencyService.Register<AnimalViewModel>();

            InitializeComponent();
            FlowListView.Init();

            var vm = DependencyService.Get<AnimalViewModel>();
            AnimalList.FlowItemsSource = vm.Animals;
        }

        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel thisAnimal = (AnimalModel)e.Item;
            var vm = DependencyService.Get<AnimalViewModel>();
            vm.SelectedAnimal = thisAnimal;
            await Navigation.PushAsync(new AnimalDetailsPage());
        }
    }
}
