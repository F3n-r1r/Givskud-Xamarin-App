using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLToolkit.Forms.Controls;

using GivskudApp.Models;
using GivskudApp.ViewModel;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizIngamePage : ContentPage
	{
        private GuiInstanceController.AnnaGuiInstance AnnaOverlay { get; set; }
        private QuizIngameViewModel ViewModel { get; set; }
        
		public QuizIngamePage (QuizModel Data)
		{

            InitializeComponent();

            ViewModel = new QuizIngameViewModel(Data);
            AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel, ViewModel.Question.Question);

            BindingContext = ViewModel;

            ViewModel.RefreshGameboardEvent += (sender, e) =>
            {
                if(ViewModel.IsGameOver)
                {
                    AnnaOverlay.HideTextBubble();
                } else
                {
                    System.Diagnostics.Debug.WriteLine(ViewModel.Question.Question);
                    AnnaOverlay.ChangeTextBubble(ViewModel.Question.Question);
                }
            };
            ViewModel.EndGameSessionEvent += (sender, e) =>
            {
                AnnaOverlay.HideOverlay();
                ApplicationLayoutContentLevel.VerticalOptions = LayoutOptions.Center;
            };
            ViewModel.ReturnToGamesEvent += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };
        }
    }
}