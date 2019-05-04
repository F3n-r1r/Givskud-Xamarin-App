using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Interfaces;

namespace GivskudApp.Controllers
{
    class GuiInstanceController
    {
        public class GuiInstance : IGuiInstance
        {
            
            protected AbsoluteLayout Master { get; set; }
            protected StackLayout OverlayParent { get; set; }

            public GuiInstance(AbsoluteLayout _Master)
            {
                Master = _Master;
                OverlayParent = CreateOverlayParent();
            }
            public void ShowOverlay()
            {
                OverlayParent.IsVisible = true;
            }
            public void HideOverlay()
            {
                OverlayParent.IsVisible = false;
            }
            protected StackLayout CreateOverlayParent()
            {
                StackLayout Output = new StackLayout
                {
                    Padding = new Thickness(0),
                    IsClippedToBounds = true
                };
                AbsoluteLayout.SetLayoutFlags(Output, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(Output, new Rectangle(0, 0, 1, 1));

                return Output;
            }
            protected virtual void AppendToMaster()
            {
                Master.Children.Add(OverlayParent);
            }

        }


        public class AnnaGuiInstance : GuiInstance  
        {
            private AbsoluteLayout InnerWrapper = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            private Label Message;
            private Frame MessageElement;
            private bool IsMessageVisibleByDefault;

            public AnnaGuiInstance(AbsoluteLayout _Master, string _Message = null) : base(_Master)
            {

                IsMessageVisibleByDefault = _Message != null;

                Message = new Label
                {
                    MaxLines = 5,
                    LineBreakMode = LineBreakMode.WordWrap,
                    Text = _Message,
                    FontSize = 12,
                    TextColor = Color.Black
                };

                MessageElement = CreateMessageElement();

                InnerWrapper.Children.Add(MessageElement);
                InnerWrapper.Children.Add(CreateAnnaElement());

                AppendToMaster();
            }
            private Frame CreateMessageElement()
            {

                Frame Main = new Frame
                {
                    CornerRadius = 0,
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(0),
                    Padding = new Thickness(0),
                    IsClippedToBounds = true
                };
                AbsoluteLayout.SetLayoutFlags(Main, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(Main, new Rectangle(1, 0.125, 0.55, 0.7));

                StackLayout Inner = new StackLayout
                {
                    Spacing = 0,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0),
                    Padding = new Thickness(0, 0, 4, 0),
                    IsClippedToBounds = true
                };

                Frame Bubble = new Frame
                {
                    CornerRadius = 14,
                    IsClippedToBounds = true,
                    Padding = new Thickness(10, 12),
                    Margin = new Thickness(0),
                    BackgroundColor = Color.White
                };
                Bubble.Content = Message;

                // Concat and return
                Inner.Children.Add(Bubble);
                Main.Content = Inner;

                Main.IsVisible = IsMessageVisibleByDefault;

                return Main;

            }
            private Frame CreateAnnaElement()
            {

                Frame Wrapper = new Frame
                {
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(0),
                    Padding = new Thickness(0),
                    IsClippedToBounds = true
                };

                AbsoluteLayout.SetLayoutFlags(Wrapper, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(Wrapper, new Rectangle(0, 1, 0.6, 1));

                Wrapper.Content = new Image
                {
                    Source = "Graphic_Anna.png",
                    VerticalOptions = LayoutOptions.End
                };

                return Wrapper;

            }
            protected override void AppendToMaster()
            {
                OverlayParent.Children.Add(InnerWrapper);
                Master.Children.Insert(0,OverlayParent);
            }
            public void HideTextBubble()
            {
                MessageElement.IsVisible = false;
            }
            public void ShowTextBubble()
            {
                MessageElement.IsVisible = true;
            }
            public void ChangeTextBubble(string _Message)
            {
                Message.Text = _Message;
            }
        }
    }
}
