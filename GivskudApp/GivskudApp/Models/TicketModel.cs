using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class TicketModel
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public List<ProductModel> Products { get; set; }
        public bool IsExpired { get; set; }
        public string ValidUntilConverted { get {
                return AppResources.AppResources.TicketsValidUntil + " " + ValidUntil.ToString("dd/MM/yyyy");
            }
        }
    }
    public class ProductModel
    {
        public string Title { get; set; }
        public int PricePerPerson { get; set; }
        public int Quantity { get; set; }
    }
}
