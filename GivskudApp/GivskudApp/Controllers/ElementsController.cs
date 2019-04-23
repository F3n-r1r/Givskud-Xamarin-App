using System;
using System.Collections.Generic;
using System.Text;

using GivskudApp.Controllers;
using GivskudApp.Models;
using GivskudApp.Views;
using GivskudApp.ViewModel;

using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Controllers
{
    class ElementsController
    {
        public static void RenderScannerIcon(AbsoluteLayout TopLevel, INavigation Navigation)
        {

            // Create Frame wrapping element
            Frame FrameElement = new Frame
            {
                Opacity = 1,
                CornerRadius = 100,
                BackgroundColor = Color.FromHex("#FFDE1F28")
            };

            AbsoluteLayout.SetLayoutBounds(FrameElement, new Rectangle(0, 1, 60, 60));
            AbsoluteLayout.SetLayoutFlags(FrameElement, AbsoluteLayoutFlags.PositionProportional);

            // Attach event handlers
            TapGestureRecognizer ClickGesture = new TapGestureRecognizer();
            ClickGesture.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new ScannerPage());
            };
            FrameElement.GestureRecognizers.Add(ClickGesture);

            // Create inner image
            Image IconImage = new Image
            {
                Source = "Icon_Uncategorized_QR.png"
            };
            FrameElement.Content = IconImage;

            // Apply to parent
            TopLevel.Children.Add(FrameElement);

        }
        public static int GetScannerIconSize()
        {
            return 60;
        }
    }
}