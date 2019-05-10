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
                BackgroundColor = Color.FromHex("#FFDE1F28"),
                Margin = new Thickness(10,0,0,10),
                Padding = new Thickness(0)
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
                Source = "Icon_Uncategorized_QR.png",
                Scale = 0.4
            };
            FrameElement.Content = IconImage;

            // Apply to parent
            TopLevel.Children.Add(FrameElement);

        }
        public static int GetScannerIconSize()
        {
            return 80;
        }
        public static void RenderNotification(AbsoluteLayout TopLevel, string Message, string EventProperty, string BindingProperty, bool IsWarning)
        {

            string TapToCloseMsg = "(Tap to close)";

            StackLayout Wrapper = new StackLayout
            {
                IsClippedToBounds = true,
                Padding = new Thickness(7)
            };
            AbsoluteLayout.SetLayoutBounds(Wrapper, new Rectangle(0, 0, 1, .15));
            AbsoluteLayout.SetLayoutFlags(Wrapper, AbsoluteLayoutFlags.All);

            Wrapper.SetBinding(StackLayout.IsVisibleProperty, BindingProperty);

            Frame NotificationFrame = new Frame
            {
                BackgroundColor = IsWarning ? Color.FromRgb(255, 91, 101) : Color.FromRgb(82, 61, 56),
                IsClippedToBounds = true,
                CornerRadius = 6
            };
            
            TapGestureRecognizer ClickedRecognizer = new TapGestureRecognizer();

            ClickedRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "VMCloseOverlayCommand");
            ClickedRecognizer.CommandParameter = EventProperty;
            
            NotificationFrame.GestureRecognizers.Add(ClickedRecognizer);

            Label NotificationMessage = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 11,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                Text = Message + " " + TapToCloseMsg
            };

            NotificationFrame.Content = NotificationMessage;

            Wrapper.Children.Add(NotificationFrame);

            // Apply to parent
            TopLevel.Children.Add(Wrapper);

        }
    }
}