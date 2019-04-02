using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class ProgramModel {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsBoundToDate { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
    }
}
