using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLToolkit.Forms.Controls;

using GivskudApp.Models;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuideSavannaPage : ContentPage
	{

        private AnimalViewModel vm { get; set; }

        public GuideSavannaPage (int id)
		{

            vm = new AnimalViewModel(id.ToString());

            InitializeComponent ();
            GuiInstanceController.AnnaGuiInstance AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel);
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            FlowListView.Init();

            BindingContext = vm;

        }
        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            AnimalModel Item = e.Item as AnimalModel;
            await Navigation.PushAsync(new AnimalGuidePage(Item));
        }
    }
}