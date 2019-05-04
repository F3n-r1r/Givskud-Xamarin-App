using System;
using System.Collections.Generic;
using System.Text;

using GivskudApp.Models;

namespace GivskudApp.ViewModel
{
    class AnimalGuideViewModel
    {

        public AnimalModel _AnimalData;

        public bool IsQuizAvailable {
            get {
                return _AnimalData.QuizID != -1;
            }
        }

        public AnimalGuideViewModel(AnimalModel SelectedAnimal)
        {
            _AnimalData = SelectedAnimal;
        }
    }
}
