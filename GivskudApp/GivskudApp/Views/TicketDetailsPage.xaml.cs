using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicketDetailsPage : ContentPage
	{
		public TicketDetailsPage (TicketModel Data)
		{

			InitializeComponent ();

            BindingContext = Data;

		}
	}
}