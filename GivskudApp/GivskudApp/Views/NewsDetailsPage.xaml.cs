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
	public partial class NewsDetailsPage : ContentPage
	{
        private readonly NewsViewModel vm;

        public NewsDetailsPage ()
		{
			InitializeComponent ();

            DependencyService.Register<NewsViewModel>();
            vm = DependencyService.Get<NewsViewModel>();
            BindingContext = vm;
        }
	}
}