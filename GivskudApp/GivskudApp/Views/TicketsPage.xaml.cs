using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;
using GivskudApp.ViewModel;

using Plugin.Settings;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicketsPage : ContentPage
	{

        private TicketsViewModel Binding { get; set; }

		public TicketsPage ()
		{

			InitializeComponent ();

            Binding = new TicketsViewModel();
            BindingContext = Binding;

            System.Diagnostics.Debug.WriteLine("Number of items: " + Binding.Data.Count.ToString());
            
        }
        public async void GoToTicketPurchase (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicketsPagePurchase());
        }
        async void ItemClicked(object sender, ItemTappedEventArgs e)
        {

            TicketModel SelectedItem = e.Item as TicketModel;
            await Navigation.PushAsync(new TicketDetailsPage(SelectedItem));

        }
    }
}