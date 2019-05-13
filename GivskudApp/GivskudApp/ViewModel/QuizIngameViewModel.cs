using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using GivskudApp.Services;
using GivskudApp.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace GivskudApp.ViewModel
{
    class QuizIngameViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand BackToGamesCommand { get; private set; }
        public ICommand AnswerCommand { get; private set; }
        public ICommand NextQuestionCommand { get; private set; }

        public QuizModel Data { get; set; }
        public QuizQuestionModel Question { get { return Data.Questions[QuestionIndex]; } }

        public event EventHandler ReturnToGamesEvent;
        public event EventHandler EndGameSessionEvent;
        public event EventHandler RefreshGameboardEvent;

        public bool IsGameInProgress { get; private set; }
        public bool IsGameOver { get; private set; }

        public bool IsGamePaused { get; private set; }

        public List<string> Result { get; private set; }

        public int QuestionIndex { get; private set; }
        public int Points { get; set; }

        public QuizIngameViewModel(QuizModel _Data)
        {

            Data = _Data;
            QuestionIndex = 0;

            IsGameInProgress = true;
            IsGameOver = false;
            IsGamePaused = false;

            // Back to games command
            BackToGamesCommand = new Command(() =>
            {
                ReturnToGamesEvent?.Invoke(this, EventArgs.Empty);
            });
            // Question answer command
            AnswerCommand = new Command<string>((answer) =>
            {
                if(!IsGamePaused && Int32.TryParse(answer, out int AnswerId))
                {
                    IsGamePaused = true;
                    OnPropertyChanged(nameof(IsGamePaused));

                    if(AnswerId == Question.CorrectAnswer)
                    {
                        Points++;
                    }
                }
            });
            // Next quetion command
            NextQuestionCommand = new Command(() =>
            {
                if(QuestionIndex + 1 == Data.Questions.Count)
                {

                    IsGameInProgress = false;
                    IsGameOver = true;

                    VerifyResult();

                    OnPropertyChanged(nameof(IsGameInProgress));
                    OnPropertyChanged(nameof(IsGameOver));

                    EndGameSessionEvent?.Invoke(this, EventArgs.Empty);

                } else
                {

                    QuestionIndex++;
                    OnPropertyChanged(nameof(Question));

                }

                IsGamePaused = false;
                OnPropertyChanged(nameof(IsGamePaused));
                RefreshGameboardEvent?.Invoke(this, EventArgs.Empty);

            }); 
        }
        private void VerifyResult()
        {

            Result = new List<string>();

            // Message
            Result.Add(Points.ToString() + " / " + Data.Questions.Count.ToString());

            // Image
            int Percentage = Data.Questions.Count / Points;
            if (Percentage >= 80)
            {
                Result.Add("Icon_Game_AwardLevel4.png");
                Result.Add(AppResources.AppResources.QuizEndTitle04);
            }
            else if (Percentage >= 60)
            {
                Result.Add("Icon_Game_AwardLevel3.png");
                Result.Add(AppResources.AppResources.QuizEndTitle03);
            }
            else if (Percentage >= 40)
            {
                Result.Add("Icon_Game_AwardLevel2.png");
                Result.Add(AppResources.AppResources.QuizEndTitle02);
            }
            else
            {
                Result.Add("Icon_Game_AwardLevel1.png");
                Result.Add(AppResources.AppResources.QuizEndTitle01);
            }

            OnPropertyChanged(nameof(Result));
            
        }
    }
}
