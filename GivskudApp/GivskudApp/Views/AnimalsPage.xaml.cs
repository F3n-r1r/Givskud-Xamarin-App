using DLToolkit.Forms.Controls;
using GivskudApp.Models;
using GivskudApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalsPage : ContentPage
	{

        private AnimalViewModel Binding { get; set; }

		public AnimalsPage (string areaid = null)
		{
         
            Binding = new AnimalViewModel(areaid);

            InitializeComponent();
            FlowListView.Init();

            BindingContext = Binding;

        }

        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel thisAnimal = (AnimalModel)e.Item;
            var vm = Binding;
            vm.SelectedAnimal = thisAnimal;
            await Navigation.PushAsync(new AnimalDetailsPage());
        }
    }
}
