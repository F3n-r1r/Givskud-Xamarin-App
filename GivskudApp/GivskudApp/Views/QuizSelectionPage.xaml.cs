using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLToolkit.Forms.Controls;

using GivskudApp.Models;
using GivskudApp.Controllers;
using GivskudApp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizSelectionPage : ContentPage
	{

        private QuizSelectionViewModel vm { get; set; }

		public QuizSelectionPage ()
		{

            vm = new QuizSelectionViewModel();

			InitializeComponent ();
            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel);
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            FlowListView.Init();

            BindingContext = vm;
        }
        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            QuizModel Item = e.Item as QuizModel;
            await Navigation.PushAsync(new QuizIngamePage(Item));
        }
    }
}