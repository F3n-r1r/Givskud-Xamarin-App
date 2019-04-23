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
        private bool _IsGameOver = false;
        private int CurrentQuestionIndex = 0;
        private int _UserScore = 0;
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
                return CurrentQuestionIndex + 1 <= Data.Questions.Count ? Data.Questions[CurrentQuestionIndex] : null;
            }
        }
        public bool IsGameOver {
            get {
                return _IsGameOver;
            }
            set {
                _IsGameOver = value;
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
                IsGameOver = true;
                OnPropertyChanged("IsGameOver");
            }
        }
        public void AddPoint()
        {
            _UserScore++;
        }
        public int GetPoints()
        {
            return _UserScore;
        }
        public string[] MessageFinalScoreDescription {
            get {
                switch(GetPoints() / Data.Questions.Count)
                {
                    case int n when (n >= 100):
                        return new string[] { "Wow! Everything correct! (100%)", "img.jpg" };
                    case int n when (n >= 75):
                        return new string[] { "Wow! Everything correct! (75-99%)", "img.jpg" };
                    case int n when (n >= 50):
                        return new string[] { "Wow! Everything correct! (50-75%)", "img.jpg" };
                    case int n when (n >= 25):
                        return new string[] { "Wow! Everything correct! (25-50%)", "img.jpg" };
                    default:
                        return new string[] { "Wow! Everything correct! (0-25%)", "img.jpg" };
                }
            }
        }
        public string MessageFinalScoreOverview {
            get {
                return GetPoints().ToString() + "/" + Data.Questions.Count.ToString();
            }
        }
    }
}
