using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Globalization;

namespace GivskudApp.Models
{
    class SeasonPassModel
    {
        public string ID { get; set; }
        public string Holder { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string AcquiredOn { get; set; }

        public bool IsValid() {

            DateTime ValidToDate = DateTime.ParseExact(ValidTo, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date;
            DateTime Today = DateTime.Now.Date;

            return ValidToDate.CompareTo(Today) >= 0;
            
        }
    }
}
