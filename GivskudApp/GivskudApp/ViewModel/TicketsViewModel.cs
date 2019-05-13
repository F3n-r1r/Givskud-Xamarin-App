using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using Newtonsoft.Json;
using Plugin.Settings;

using GivskudApp.Resources;
using GivskudApp.Models;

namespace GivskudApp.ViewModel
{
    public class TicketsViewModel : INotifyPropertyChanged
    {

        public List<TicketModel> Data { get; private set; }

        public TicketsViewModel ()
        {

            Data = new List<TicketModel>();
            GetTickets();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void GetTickets()
        {

            string SavedTickets = CrossSettings.Current.GetValueOrDefault(ConfigurationManager.AppConfiguration.Tickets, null);

            if (SavedTickets != null)
            {
                List<TicketModel> Temp = JsonConvert.DeserializeObject<List<TicketModel>>(SavedTickets);
                if (Temp.Count > 0)
                {
                    foreach (TicketModel Ticket in Temp)
                    {
                        if (DateTime.Compare(Ticket.ValidUntil, DateTime.Now) <= 0)
                        {
                            Temp.Add(Ticket);
                        }
                    }
                    Data = Temp;
                }
            }

            OnPropertyChanged(nameof(Data));

        }

    }
    public class PurchaseTicketsViewModel : INotifyPropertyChanged
    {

        public int PriceTotal { get; private set; }
        public int PriceSubtotal { get {
                return (int)(PriceTotal * 0.75);
            }
        }
        public int PriceVAT {
            get {
                return (int)(PriceTotal * 0.25);
            }
        }

        public bool IsCheckoutVisible { get {
                return !IsInputVisible;
            }
        }
        public bool IsInputVisible { get; set; }
        public int CheckoutStatusStep { get {
                return IsInputVisible ? 1 : 2;
            }
        }
        public string CheckoutStatusHeadline { get {
                if(IsInputVisible)
                {
                    return AppResources.AppResources.BuyTicketsStep1;
                } else
                {
                    return AppResources.AppResources.BuyTicketsStep2;
                }
            }
        }

        public event EventHandler GoToTicketsPage;

        public Dictionary<string,ProductModel> ProductMatrix { get; private set; }
       
        public ICommand CommandGoBack { get; private set; }
        public ICommand CommandPayAndCollect { get; private set; }
        public ICommand CommandToCheckout { get; private set; }
        public ICommand TapCommandAdd { get; private set; }
        public ICommand TapCommandDecrease { get; private set; }
        
        public PurchaseTicketsViewModel()
        {

            IsInputVisible = true;
            ProductMatrix = new Dictionary<string, ProductModel>
            {
                { "Adults", new ProductModel {
                    Title = AppResources.AppResources.BuyTicketsAdults,
                    PricePerPerson = 150,
                    Quantity = 0
                }},
                { "Children", new ProductModel {
                    Title = AppResources.AppResources.BuyTicketsChildren,
                    PricePerPerson = 70,
                    Quantity = 0
                }},
                { "Newborns", new ProductModel {
                    Title = AppResources.AppResources.BuyTicketsNewborns,
                    PricePerPerson = 20,
                    Quantity = 0
                }},
                { "CDDK", new ProductModel {
                    Title = "Safari CD (Dansk)",
                    PricePerPerson = 100,
                    Quantity = 0
                }},
                { "CDEN", new ProductModel {
                    Title = "Safari CD (English)",
                    PricePerPerson = 100,
                    Quantity = 0
                }},
                { "CDDE", new ProductModel {
                    Title = "Safari CD (Deutsch)",
                    PricePerPerson = 100,
                    Quantity = 0
                }},
                { "CDNL", new ProductModel {
                    Title = "Safari CD (Nederlands)",
                    PricePerPerson = 100,
                    Quantity = 0
                }},
            };

            OnPropertyChanged(nameof(CheckoutStatusStep));
            OnPropertyChanged(nameof(IsInputVisible));
            OnPropertyChanged(nameof(ProductMatrix));

            CommandGoBack = new Command(() =>
            {
                
                IsInputVisible = true;
                OnPropertyChanged(nameof(IsInputVisible));
                OnPropertyChanged(nameof(IsCheckoutVisible));

                OnPropertyChanged(nameof(CheckoutStatusStep));
                OnPropertyChanged(nameof(CheckoutStatusHeadline));

            });
            CommandPayAndCollect = new Command(() =>
            {

                // Create purchase data
                TicketModel Purchase = new TicketModel
                {
                    PurchaseDate = DateTime.Now,
                    ValidUntil = DateTime.Now.AddDays(1),
                    Products = new List<ProductModel>(),
                    IsExpired = false
                };

                foreach(KeyValuePair<string,ProductModel> Item in ProductMatrix)
                {
                    if(Item.Value.Quantity > 0)
                    {
                        Purchase.Products.Add(Item.Value);
                    }
                }

                // Get saved data
                string SavedTickets = CrossSettings.Current.GetValueOrDefault(ConfigurationManager.AppConfiguration.Tickets, null);
                List<TicketModel> TicketInstances;

                if(String.IsNullOrEmpty(SavedTickets))
                {
                    TicketInstances = new List<TicketModel>();
                } else
                {
                    TicketInstances = JsonConvert.DeserializeObject<List<TicketModel>>(SavedTickets);
                }

                // Add new ticket and save
                TicketInstances.Add(Purchase);
                CrossSettings.Current.AddOrUpdateValue(ConfigurationManager.AppConfiguration.Tickets, JsonConvert.SerializeObject(TicketInstances));

                // Push navigation
                GoToTicketsPage?.Invoke(this, EventArgs.Empty);

            });
            CommandToCheckout = new Command(() =>
            {

                int Total = 0;
                bool HasItems = false;

                foreach(var Item in ProductMatrix)
                {
                    if(Item.Value.Quantity > 0)
                    {
                        HasItems = true;
                        Total += Item.Value.Quantity * Item.Value.PricePerPerson;
                    }
                }

                if(HasItems == true)
                {

                    PriceTotal = Total;

                    IsInputVisible = false;

                    OnPropertyChanged(nameof(IsInputVisible));
                    OnPropertyChanged(nameof(IsCheckoutVisible));

                    OnPropertyChanged(nameof(CheckoutStatusStep));
                    OnPropertyChanged(nameof(CheckoutStatusHeadline));

                    RefreshBasketTotals();

                }
                
            });

            TapCommandAdd = new Command<string>((key) =>
            {
                if(ProductMatrix.ContainsKey(key))
                {
                    ProductMatrix[key].Quantity++;
                    OnPropertyChanged(nameof(ProductMatrix));
                } else
                {
                    System.Diagnostics.Debug.WriteLine("The product dictionary does not contain key " + key);
                }
            });
            TapCommandDecrease = new Command<string>((key) =>
            {
                if (ProductMatrix.ContainsKey(key))
                {
                    if(ProductMatrix[key].Quantity > 0)
                    {
                        ProductMatrix[key].Quantity--;
                        OnPropertyChanged(nameof(ProductMatrix));
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("The product dictionary does not contain key " + key);
                }
            });
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void RefreshBasketTotals()
        {
            OnPropertyChanged(nameof(PriceTotal));
            OnPropertyChanged(nameof(PriceSubtotal));
            OnPropertyChanged(nameof(PriceVAT));
        }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}