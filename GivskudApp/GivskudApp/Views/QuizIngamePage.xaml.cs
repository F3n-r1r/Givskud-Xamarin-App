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

        private bool IsLocked = false;

		public QuizIngamePage (QuizModel Data)
		{

            InitializeComponent();

            ViewModel = new QuizIngameViewModel(Data);
            AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel, ViewModel.Question.Question);

            BindingContext = ViewModel;

        }
        public async void BtnClicked(object sender, EventArgs e)
        {
            Button s = sender as Button;
            
            switch(s.ClassId)
            {
                case "MoveForwardBtn":
                    NextQuestionBtn.IsVisible = false;
                    if(ViewModel.IsGameOver)
                    {
                        AnnaOverlay.HideTextBubble();
                    }
                    break;
                case "CloseGameBtn":
                    await Navigation.PopAsync();
                    break;
            }
        }
        public void AnswerBtnClicked(object sender, EventArgs e)
        {
            Button s = sender as Button;
            Int32.TryParse(s.ClassId, out int AnswerId);

            if(AnswerId != 0 && IsLocked == false)
            {

                IsLocked = true;

                if(AnswerId == ViewModel.Question.CorrectAnswer)
                {
                    s.BackgroundColor = Color.Green;
                } else
                {
                    s.BackgroundColor = Color.Red;
                }
            }
        }
    }
}