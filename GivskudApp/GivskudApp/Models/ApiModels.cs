using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models {
    class ApiModels {
        public class SeasonPass {
            public string ID { get; set; }
            public string Holder { get; set; }
            public string ValidFrom { get; set; }
            public string ValidTo { get; set; }
            public string AcquiredOn { get; set; }
        }
        public class Animal {
            public string ID { get; set; }
            public string Icon { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public List<string> Content { get; set; }
            public List<string> Height { get; set; }
            public List<string> Length { get; set; }
            public List<string> Weight { get; set; }
            public string PregnancyTime { get; set; }
            public List<string> Descendants { get; set; }
            public List<string> Lifetime { get; set; }
            public string Continent { get; set; }
            public string Status { get; set; }
            public string Eats { get; set; }
            public string Species { get; set; }
        }
        public class Event {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Desc { get; set; }
            public bool IsBoundToDate { get; set; }
            public DateTime EventDate { get; set; }
            public DateTime EventTime { get; set; }
        }
        public class Post {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public string Image { get; set; }
            public string PublishedOn { get; set; }
            public string PublishedBy { get; set; }
        }
    }
}
