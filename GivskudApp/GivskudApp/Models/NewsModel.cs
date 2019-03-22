using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class NewsModel
    {
        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime PublishDate { get; set; }
        public string Image { get; set; }
    }
}
