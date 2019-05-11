﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Models;
using GivskudApp.Views;

namespace GivskudApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public List<NavigationModel> MenuList { get; set; }

        public MainPage()
        {
            InitializeComponent();
            

            MenuList = new List<NavigationModel>
            {
                // Adding menu items to menuList and you can define title ,page and icon
                new NavigationModel() { Title = "Homepage", Icon = "Icon_Navigation_Home.png", TargetType = typeof(HomePage) },
                new NavigationModel() { Title = "Games", Icon = "Icon_Navigation_Games.png", TargetType = typeof(GamePage) },
                new NavigationModel() { Title = "News", Icon = "Icon_Navigation_News.png", TargetType = typeof(NewsPage) },
                new NavigationModel() { Title = "Season Pass", Icon = "Icon_Navigation_SeasonPass.png", TargetType = typeof(SeasonPassPage) },
                new NavigationModel() { Title = "Animals", Icon = "Icon_Navigation_Animals.png", TargetType = typeof(AnimalsPage) },
                new NavigationModel() { Title = "Contact Us", Icon = "Icon_Navigation_Contact.png", TargetType = typeof(ContactPage) },
                new NavigationModel() { Title = "Buy Tickets", Icon = "Icon_Navigation_Tickets.png", TargetType = typeof(TicketsPage) },
            };

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            menuItemList.ItemsSource = MenuList;

            // Initial navigation, set to the home page
            menuItemList.SelectedItem = MenuList[0];
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (NavigationModel)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }

        public void InvokeNavigationChange()
        {
            System.Diagnostics.Debug.WriteLine("Invokation on MainPage instance");
        }
    }
}
