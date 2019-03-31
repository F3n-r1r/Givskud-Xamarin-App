using GivskudApp.Models;
using GivskudApp.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GivskudApp.ViewModel
{
    public class AnimalViewModel
    {
        AnimalService service = new AnimalService();
        int currentAnimal;

        public List<AnimalModel> Animals { get { return service.Animal; } }

        // The specific Animal item that is clicked (Used for -> AnimalDetailsPage)
        public AnimalModel SelectedAnimal
        {
            get
            {
                return service.Animal[currentAnimal];
            }
            set
            {
                int index = service.Animal.IndexOf(value);
                if (index >= 0)
                {
                    currentAnimal = index;
                }
            }
        }
    }
}
