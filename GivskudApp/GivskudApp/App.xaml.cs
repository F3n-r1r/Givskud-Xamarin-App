using System;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Resources;

using Plugin.Settings;
using Plugin.Multilingual;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GivskudApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            // Load store culture if available
            string StoredCulture = CrossSettings.Current.GetValueOrDefault(ConfigurationManager.AppConfiguration.LanguagePreset, null);
            
            if(!String.IsNullOrEmpty(StoredCulture))
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(StoredCulture);
            }

            // Initiate language plugin
            AppResources.AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;

            // Start app
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void ResetCache()
        {
            CrossSettings.Current.Remove(ConfigurationManager.RemoteResources.Local.Animals);
            CrossSettings.Current.Remove(ConfigurationManager.RemoteResources.Local.News);
            CrossSettings.Current.Remove(ConfigurationManager.RemoteResources.Local.Quizes);
            CrossSettings.Current.Remove(ConfigurationManager.RemoteResources.Local.Pass);
            CrossSettings.Current.Remove(ConfigurationManager.RemoteResources.Local.Events);
            CrossSettings.Current.Remove(ConfigurationManager.AppConfiguration.Tickets);
            CrossSettings.Current.Remove(ConfigurationManager.AppConfiguration.LanguagePreset);
        }

    }
}
