using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using GivskudApp.Models;

namespace GivskudApp.ViewModel
{
    class QuizIngameViewModel : INotifyPropertyChanged
    {

        private int CurrentQuestionIndex = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public QuizModel Data { get; private set; }

        public QuizIngameViewModel(QuizModel QuizData)
        {
            Data = QuizData;
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public QuizQuestionModel CurrentQuestion {
            get {
                return Data.Questions[CurrentQuestionIndex];
            }
        }
        public bool IsGameOver {
            get {
                return CurrentQuestionIndex > Data.Questions.Count;
            }
        }

        public void NextQuestion()
        {
            if(CurrentQuestionIndex + 1 < Data.Questions.Count)
            {
                CurrentQuestionIndex++;
                OnPropertyChanged("CurrentQuestion");
            } else
            {
                CurrentQuestionIndex++;
            }
        }
        public void AddPoint()
        {

        }
    }
}
