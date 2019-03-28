using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class AnimalModel
    {
        public int ID { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<string> Content { get; set; }

        public List<int> Height { get; set; }
        public List<int> Length { get; set; }
        public List<int> Weight { get; set; }
        public int PDays { get; set; }
        public List<int> Descendants { get; set; }
        public List<int> Lifetime { get; set; }
        public List<string> Continent { get; set; }
        public string Species { get; set; }
        public string Status { get; set; }
        public string Eats { get; set; }
    }
    //public class AnimalInfo {
    //    public List<int> Height { get; set; }
    //    public List<int> Length { get; set; }
    //    public List<int> Weight { get; set; }
    //    public int PDays { get; set; }
    //    public List<int> Descendants { get; set; }
    //    public List<int> Lifetime { get; set; }
    //    public List<string> Continent { get; set; }
    //    public string Species { get; set; }
    //    public string Status { get; set; }
    //    public string Eats { get; set; }
    //}
}