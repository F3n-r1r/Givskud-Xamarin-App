using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Models;
using GivskudApp.Services;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalDetailsPage : ContentPage
	{
        private readonly AnimalViewModel vm;

        public AnimalDetailsPage ()
		{

			InitializeComponent ();
            ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, true);

            DependencyService.Register<AnimalViewModel>();
            vm = DependencyService.Get<AnimalViewModel>();
            BindingContext = vm.SelectedAnimal;
            
        }
    }
}