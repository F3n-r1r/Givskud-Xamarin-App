using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using GivskudApp.Views;

using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Threading.Tasks;

namespace GivskudApp.Helpers
{
    class ConnectionHelper
    {
        public static bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
