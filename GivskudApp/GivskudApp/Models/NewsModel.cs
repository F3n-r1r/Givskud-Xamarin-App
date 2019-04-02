using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class NewsModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string PublishedBy { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
