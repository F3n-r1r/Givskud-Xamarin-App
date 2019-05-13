using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Plugin.Settings;
using Plugin.Multilingual;

using GivskudApp.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
        }
        // Handle language change
        public async void LanguageChangeEvent(object sender, EventArgs e)
        {

            Image source = sender as Image;

            string languageId = source.ClassId;
            
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(languageId);

            CrossSettings.Current.AddOrUpdateValue(ConfigurationManager.AppConfiguration.LanguagePreset, languageId);

            await Navigation.PushAsync(new HomePage());

        }   
        // Handle button click -> Push new page
        async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.ClassId;

            if (id == "TourPageBtn")
            {
                await Navigation.PushAsync(new GuidePage());
            }
            else if (id == "MapPageBtn")
            {
                await Navigation.PushAsync(new MapPage());
            }
            else if (id == "ProgramPageBtn")
            {
                await Navigation.PushAsync(new ProgramPage());
            }

        }
    }
}