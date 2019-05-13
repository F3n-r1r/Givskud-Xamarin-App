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

            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("Initialized page; Current language: " + CrossMultilingual.Current.CurrentCultureInfo);

        }
        // Handle language change
        public async void LanguageChangeEvent(object sender, EventArgs e)
        {

            // Collect info
            Image source = sender as Image;
            string languageId = source.ClassId;

            // Change culture
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(languageId);
            AppResources.AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            // Save cache value
            CrossSettings.Current.AddOrUpdateValue(ConfigurationManager.AppConfiguration.LanguagePreset, languageId);

            // Pop navigation
            await PopAllModals();

        }
        public async Task<Page> PopAllModals()
        {
            Page root = null;

            if (Navigation.ModalStack.Count() == 0)
                return null;

            for (var i = 0; i <= Navigation.ModalStack.Count(); i++)
            {
                root = await Navigation.PopModalAsync(false);
            }
            return root;
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