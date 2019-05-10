using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using Plugin.Settings;

namespace GivskudApp.Resources
{
    public class CacheResource : Resource
    {
        public CacheResource(string Path) : base(Path)
        {

        }
        public override ResourceResponse Get()
        {

            string MemoryTrace = CrossSettings.Current.GetValueOrDefault(Path, string.Empty);

            return new ResourceResponse
            {
                IsValid = String.IsNullOrEmpty(MemoryTrace) ? false : true,
                Data = String.IsNullOrEmpty(MemoryTrace) ? null : MemoryTrace
            };

        }
        public void AddOrUpdate(string DataStringified)
        {
            CrossSettings.Current.AddOrUpdateValue(Path, DataStringified);
        }
        public void Remove()
        {
            CrossSettings.Current.Remove(Path);
        }
    }
}
