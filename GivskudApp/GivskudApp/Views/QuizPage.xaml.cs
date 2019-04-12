using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Models;
using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizPage : ContentPage
	{
        public QuizPage()
        {
            InitializeComponent();
            QuizList.FlowItemsSource = new List<QuizModel> {
                new QuizModel {
                    ID = 1234,
                    Title = "Quiz 1",
                    Image = "https://www.sciencemag.org/sites/default/files/styles/inline__450w__no_aspect/public/NationalGeographic_1561927_16x9.jpg?itok=q7LvZb-6",
                    IsLockedByDefault = false,
                    Questions = new List<QuizQuestionModel> {
                        new QuizQuestionModel {
                            Question = "Question 1",
                            Answers = new Dictionary<int, string> {
                                {1, "Answer 1 Q1" },
                                {2, "Answer 2 Q1" },
                                {3, "Answer 3 Q1" },
                                {4, "Answer 4 Q1" }
                            },
                            CorrectAnswer = 2
                        },
                        new QuizQuestionModel {
                            Question = "Question 2",
                            Answers = new Dictionary<int, string> {
                                {1, "Answer 1 Q2" },
                                {2, "Answer 2 Q2" },
                                {3, "Answer 3 Q2" },
                                {4, "Answer 4 Q2" }
                            },
                            CorrectAnswer = 1
                        }
                    }
                },
                new QuizModel {
                    ID = 1234,
                    Title = "Quiz 2",
                    Image = "https://www.sciencemag.org/sites/default/files/styles/inline__450w__no_aspect/public/NationalGeographic_1561927_16x9.jpg?itok=q7LvZb-6",
                    IsLockedByDefault = false,
                    Questions = new List<QuizQuestionModel> {
                        new QuizQuestionModel {
                            Question = "Question 1",
                            Answers = new Dictionary<int, string> {
                                {1, "Answer 1 Q1" },
                                {2, "Answer 2 Q1" },
                                {3, "Answer 3 Q1" },
                                {4, "Answer 4 Q1" }
                            },
                            CorrectAnswer = 2
                        },
                        new QuizQuestionModel {
                            Question = "Question 2",
                            Answers = new Dictionary<int, string> {
                                {1, "Answer 1 Q2" },
                                {2, "Answer 2 Q2" },
                                {3, "Answer 3 Q2" },
                                {4, "Answer 4 Q2" }
                            },
                            CorrectAnswer = 1
                        }
                    }
                }
            };
        }
        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            QuizModel Item = e.Item as QuizModel;
            await Navigation.PushAsync(new QuizIngamePage(Item));
        }
    }
}