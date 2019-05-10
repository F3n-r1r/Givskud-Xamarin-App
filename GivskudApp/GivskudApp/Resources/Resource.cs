using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Resources
{
    public abstract class Resource
    {

        public string Path { get; set; }

        public Resource(string _Path)
        {
            Path = _Path;
        }

        public abstract ResourceResponse Get();

    }
    public class ResourceResponse
    {
        public bool IsValid { get; set; }
        public string Data { get; set; }
    }
}
