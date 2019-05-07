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

        public ICommand AnswerCommand { get; private set; }
        public ICommand NextQuestionCommand { get; private set; }

        public QuizModel Data { get; set; }
        public QuizQuestionModel Question { get { return Data.Questions[QuestionIndex]; } }

        public bool IsGameInProgress { get; private set; }
        public bool IsGameOver { get; private set; }

        public int QuestionIndex { get; private set; }
        public int Points { get; set; }

        public QuizIngameViewModel(QuizModel _Data)
        {
            Data = _Data;
            QuestionIndex = 0;

            // Question answer command
            AnswerCommand = new Command<string>((answer) =>
            {
                Int32.TryParse(answer, out int AnswerId);
                if(AnswerId != 0)
                {
                    if(AnswerId == Question.CorrectAnswer)
                    {
                        Points++;
                        System.Diagnostics.Debug.WriteLine("Correct answer");
                    } else
                    {
                        System.Diagnostics.Debug.WriteLine("Incorrect answer");
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

                    OnPropertyChanged(nameof(IsGameInProgress));
                    OnPropertyChanged(nameof(IsGameOver));
                } else
                {
                    QuestionIndex++;
                    OnPropertyChanged(nameof(Question));
                }
            }); 
        }

    }
}
