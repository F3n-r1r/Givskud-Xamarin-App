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

        public static void RenderAnnaOverlay(AbsoluteLayout TopLevel, string Msg = null)
        {
            // Inner Wrapper
            AbsoluteLayout InnerWrappingElement = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Bubble
            if (Msg != null)
            {
                Frame SpeachBubble = CreateAnnaSpeachBubble(Msg);
                InnerWrappingElement.Children.Add(SpeachBubble);
            }

            // Anna
            Image AnnaImage = new Image
            {
                Source = "Graphic_Anna.png",
                VerticalOptions = LayoutOptions.End
            };

            Frame AnnaWrapper = new Frame
            {
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                IsClippedToBounds = true
            };

            AbsoluteLayout.SetLayoutFlags(AnnaWrapper, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(AnnaWrapper, new Rectangle(0, 1, 0.6, 1));

            AnnaWrapper.Content = AnnaImage;
            InnerWrappingElement.Children.Add(AnnaWrapper);

            // Outer Wrapper
            StackLayout GlobalWrappingElement = new StackLayout
            {
                Padding = new Thickness(0),
                IsClippedToBounds = true
            };
            AbsoluteLayout.SetLayoutFlags(GlobalWrappingElement, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(GlobalWrappingElement, new Rectangle(0, 0, 1, 1));

            GlobalWrappingElement.Children.Add(InnerWrappingElement);

            // Render on the pagelo
            TopLevel.Children.Insert(0,GlobalWrappingElement);
        }
        public static Frame CreateAnnaSpeachBubble(string Msg)
        {

            Label MessageElement = new Label
            {
                MaxLines = 5,
                LineBreakMode = LineBreakMode.WordWrap,
                Text = Msg,
                FontSize = 12,
                TextColor = Color.Black,
                ClassId = "backgroundOverlayBubbleLabel"
            };
            Frame MessageBubble = new Frame
            {
                CornerRadius = 14,
                IsClippedToBounds = true,
                Padding = new Thickness(10,12),
                Margin = new Thickness(0),
                BackgroundColor = Color.White
            };
            MessageBubble.Content = MessageElement;

            StackLayout InnerWrapper = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 0, 4, 0),
                IsClippedToBounds = true
            };
            InnerWrapper.Children.Add(MessageBubble);

            Frame GlobalWrapper = new Frame
            {
                CornerRadius = 0,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                IsClippedToBounds = true
            };
            AbsoluteLayout.SetLayoutFlags(GlobalWrapper, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(GlobalWrapper, new Rectangle(1,0.05,0.55,0.7));

            GlobalWrapper.Content = InnerWrapper;

            return GlobalWrapper;
        }
        public static void ChangeAnnaOverlayContent(string msg)
        {
            Label Output = Application.Current.FindByName<Label>("backgroundOverlayBubbleLabel");
            if(Output != null)
            {
                Output.Text = msg;
            };
        }
    }
}