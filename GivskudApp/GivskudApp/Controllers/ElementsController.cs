﻿using System;
using System.Collections.Generic;
using System.Text;

using GivskudApp.Controllers;

using Xamarin.Forms;

namespace GivskudApp.Controllers
{
    class ElementsController
    {
        public static void InitializeAbsoluteContent(StackLayout ContentLevel, AbsoluteLayout TopLevel, bool ShowScannerIcon = true, ScrollView ScrollableElement = null)
        {

            // Render icon
            if(ShowScannerIcon)
            {
                TopLevel.Children.Add(GetScannerIcon(ScrollableElement));
            }

        }
        public static Frame GetScannerIcon(ScrollView ScrollableElement)
        {

            // Create Frame wrapping element
            Frame FrameElement = new Frame
            {
                Opacity = 1,
                CornerRadius = 100,
                BackgroundColor = Color.FromHex("#FFDE1F28")
            };

            AbsoluteLayout.SetLayoutBounds(FrameElement, new Rectangle(0,1,60,60));
            AbsoluteLayout.SetLayoutFlags(FrameElement, AbsoluteLayoutFlags.PositionProportional);

            // Attach event handlers
            TapGestureRecognizer ClickGesture = new TapGestureRecognizer();
            ClickGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage.DisplayAlert("Alert!", "You clicked on scanner icon", "Dismiss");
            };
            FrameElement.GestureRecognizers.Add(ClickGesture);

            // Attach handler for scrolling
            if(ScrollableElement != null)
            {

                ScrollableElement.Scrolled += (s, e) =>
                {
                    Application.Current.MainPage.DisplayAlert("Alert!", "You scrolled!", "I know");
                    /*
                    ScrollView view = s as ScrollView;
                    double space = view.ContentSize.Height - view.Height;

                    if(space <= e.ScrollY)
                    {
                        Application.Current.MainPage.DisplayAlert("Alert!", "You reached bottom", "Dismiss");
                    }
                    */
                };
            }

            // Create inner image
            Image IconImage = new Image
            {
                Source = "icon_qrcode.png"
            };
            FrameElement.Content = IconImage;

            return FrameElement;

        }
        public static int GetScannerIconSize()
        {
            return 60;
        }
    }
}