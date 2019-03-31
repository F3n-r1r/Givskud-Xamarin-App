using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Controllers
{
    class PopupController
    {
        public static void Message(string title, string text, string dismiss = "OK") {
            System.Diagnostics.Debug.WriteLine("Default popup");
        }
        public static void Default(string headline, string text, string dismiss = "OK") {
            Console.WriteLine("Popup fired");
        }
    }
}
