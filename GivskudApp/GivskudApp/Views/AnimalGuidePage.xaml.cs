using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private readonly AnimalModel Model;

		public AnimalGuidePage (AnimalModel SelectedAnimal)
		{
            Model = SelectedAnimal;

            if(Model.QuizID == -1)
            {
                BtnGoToQuiz.IsVisible = false;
            }

			InitializeComponent ();

            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel);
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            BindingContext = SelectedAnimal;
        }
        async void BtnClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if(id == "AnimalInfoBtn")
            {
                await Navigation.PushAsync(new AnimalDetailsPage(Model));
            } else if (id == "AnimalMapBtn")
            {
                await Navigation.PushAsync(new MapPage());
            } else if (id == "AnimalQuizBtn")
            {
                // await Navigation.PushAsync(new QuizIngamePage(null, Model.QuizID.ToString()));
            }
        }
    }
}