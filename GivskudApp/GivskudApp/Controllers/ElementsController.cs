using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace GivskudApp.Controllers
{
    class ElementsController
    {
        public static void InitializeAbsoluteContent(StackLayout ContentLevel, AbsoluteLayout TopLevel, bool ShowScannerIcon = true)
        {

            // Initialize layout
            AbsoluteLayout.SetLayoutFlags(ContentLevel, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ContentLevel, new Rectangle(0, 0, 1, 1));

            // Render icon
            if(ShowScannerIcon)
            {
                TopLevel.Children.Add(GetScannerIcon());
            }

        }
        public static Frame GetScannerIcon()
        {

            // Create Frame wrapping element
            Frame FrameElement = new Frame
            {
                CornerRadius = 100,
                BackgroundColor = Color.FromHex(Application.Current.Resources["ColorPrimary"].ToString())
            };

            AbsoluteLayout.SetLayoutBounds(FrameElement, new Rectangle(0,1,60,60));
            AbsoluteLayout.SetLayoutFlags(FrameElement, AbsoluteLayoutFlags.PositionProportional);

            // Attach event handlers
            TapGestureRecognizer ClickGesture = new TapGestureRecognizer();
            ClickGesture.Tapped += (s, e) =>
            {

            };
            FrameElement.GestureRecognizers.Add(ClickGesture);

            // Create inner image
            /*
            Image IconImage = new Image
            {
                Source = "icon_qrcode.png"
            };
            FrameElement.Content = IconImage;
            */

            return FrameElement;

        }
        public static int GetScannerIconSize()
        {
            return 60;
        }
    }
}