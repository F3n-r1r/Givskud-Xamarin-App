using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;
using GivskudApp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalGuidePage : ContentPage
	{

        AnimalGuideViewModel vm;

		public AnimalGuidePage (AnimalModel SelectedAnimal)
		{
			InitializeComponent ();
            vm = new AnimalGuideViewModel(SelectedAnimal);
            BindingContext = vm;
		}
        async void BtnClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if(id == "AnimalInfoBtn")
            {
                await Navigation.PushAsync(new HomePage());
            } else if (id == "AnimalMapBtn")
            {
                await Navigation.PushAsync(new HomePage());
            } else if (id == "AnimalQuizBtn")
            {
                await Navigation.PushAsync(new HomePage());
            }
        }
    }
}