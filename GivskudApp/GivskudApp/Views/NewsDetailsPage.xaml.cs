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
	public partial class NewsDetailsPage : ContentPage
	{
        private readonly NewsViewModel vm;

        public NewsDetailsPage ()
		{
			InitializeComponent ();
            ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, true);

            DependencyService.Register<NewsViewModel>();
            vm = DependencyService.Get<NewsViewModel>();
            BindingContext = vm.SelectedNews;
        }
	}
}