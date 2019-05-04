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
        private GuiInstanceController.AnnaGuiInstance AnnaOverlay;
        private QuizIngameViewModel ViewModel;
        private bool IsLocked = false;
		public QuizIngamePage (QuizModel Data)
		{
            ViewModel = new QuizIngameViewModel(Data);

            InitializeComponent ();

            AnnaOverlay = new GuiInstanceController.AnnaGuiInstance(ApplicationLayoutTopLevel, ViewModel.CurrentQuestion.Question);

            RenderQuestionAnswers();
            BindingContext = ViewModel;
        
        }
        public void NextQuestion(object sender = null, EventArgs e = null)
        {

            ViewModel.NextQuestion();

            if(ViewModel.IsGameOver)
            {
                AnnaOverlay.HideTextBubble();
                IngameContent.IsVisible = false;
                ApplicationLayoutContentLevel.VerticalOptions = LayoutOptions.Center;
                AftergameContent.IsVisible = true;
            } else
            {
                RenderQuestionAnswers();
                IsLocked = false;
                NextQuestionBtn.IsVisible = false;
            }
        }
        public async void BackToGames(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }
        public void RenderQuestionAnswers()
        {

            AnnaOverlay.ChangeTextBubble(ViewModel.CurrentQuestion.Question);

            QuestionOptionsGrid.Children.Clear();
            int CorrectAnswer = ViewModel.CurrentQuestion.CorrectAnswer;

            Dictionary<int, string> Answers = ViewModel.CurrentQuestion.Answers;
            foreach(KeyValuePair<int,string> Answer in Answers)
            {
                int Index = Answer.Key - 1;

                Button AnswerBtn = new Button {
                    BackgroundColor = Color.FromHex("#FF97BE0D"),
                    TextColor = Color.Black,
                    Text = Answer.Value,
                    FontSize = 11
                };

                AnswerBtn.Clicked += (s, e) =>
                {
                    if(IsLocked == false) {

                        IsLocked = true;

                        if(Answer.Key == CorrectAnswer)
                        {
                            AnswerBtn.BackgroundColor = Color.Green;
                            ViewModel.AddPoint();
                        } else
                        {
                            AnswerBtn.BackgroundColor = Color.Red;
                        }

                        NextQuestionBtn.IsVisible = true;

                    } 
                };

                QuestionOptionsGrid.Children.Add(AnswerBtn, Index <= 1 ? Index : Index - 2, Index <= 1 ? 0 : 1);
            }
        }
    }
}