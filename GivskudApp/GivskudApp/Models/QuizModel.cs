using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class QuizModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsLockedByDefault { get; set; }
        public List<QuizQuestionModel> Questions { get; set; }
    }
    public class QuizQuestionModel
    {
        public string Question { get; set; }
        public Dictionary<int, string> Answers { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
