using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Models
{
    public class NavigationModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}
