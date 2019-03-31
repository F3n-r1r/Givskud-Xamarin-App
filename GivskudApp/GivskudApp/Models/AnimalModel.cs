using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class AnimalModel
    {
        public string ID { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<string> Content { get; set; }

        public string Height { get; set; }
        public string Length { get; set; }
        public string Weight { get; set; }
        public string PregnancyTime { get; set; }
        public string Descendants { get; set; }
        public string Lifetime { get; set; }
        public string Continent { get; set; }
        public string Status { get; set; }
        public string Eats { get; set; }
        public string Species { get; set; }
    }
}