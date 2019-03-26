using GivskudApp.Models;
using GivskudApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.ViewModel
{
    public class AnimalViewModel
    {
        AnimalService service = new AnimalService();

        public List<AnimalModel> Animals { get { return service.Animal; } }
    }
}
