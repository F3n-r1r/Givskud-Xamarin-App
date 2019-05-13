using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.ViewModel;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicketsPagePurchase : ContentPage
	{

        public PurchaseTicketsViewModel Binding { get; private set; }

		public TicketsPagePurchase()
		{
            
			InitializeComponent ();

            Binding = new PurchaseTicketsViewModel();
            BindingContext = Binding;

            Binding.GoToTicketsPage += (object sender, EventArgs e) =>
            {
                Application.Current.MainPage = new MainPage();
            };

        }
	}
}