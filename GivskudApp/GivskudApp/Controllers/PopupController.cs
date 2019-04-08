using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace GivskudApp.Controllers
{
    class PopupController
    {
        public static async void Simple(string Title, string Msg, string Confirm) {
            await Application.Current.MainPage.DisplayAlert(Title, Msg, Confirm);
        }
    }
}
