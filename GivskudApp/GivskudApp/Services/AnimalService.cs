using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Plugin.Settings;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Linq;

using GivskudApp.Models;
using GivskudApp.ResourceControllers;

namespace GivskudApp.Services
{
    public class AnimalService
    {

        public List<AnimalModel> Data = new List<AnimalModel>();
        
        public List<AnimalModel> Animal { get; }

        public AnimalService()
        {

            string CachedResource = CrossSettings.Current.GetValueOrDefault("applicationAnimalListCacheObject", null);

            if(CachedResource != null) {

                AnimalCache CachedResourceDeserialized = JsonConvert.DeserializeObject <AnimalCache>(CachedResource);

                if(DateTime.Compare(CachedResourceDeserialized.ValidUntil.Date, DateTime.Now.Date) >= 0) {
                    Animal = CachedResourceDeserialized.List;
                } else {
                    Animal = Fetch();
                }

            } else {

                List<AnimalModel> FetchedData = Fetch();
                AnimalCache CacheData = new AnimalCache {
                    ValidUntil = DateTime.Now.AddDays(1),
                    List = FetchedData
                };
                CrossSettings.Current.AddOrUpdateValue("applicationAnimalListCacheObject", JsonConvert.SerializeObject(CacheData)) ;
                Animal = FetchedData;

            }
        }
        
        private List<AnimalModel> Fetch() {
            ApiResourceController ApiResource = new ApiResourceController("/animals/get");
            using(var Response = ApiResource.Get()) {
                if(Response == null) {
                    return null;
                } else {
                    return JsonConvert.DeserializeObject<List<AnimalModel>>(Response.Result);
                }
            }
        }
    }
    public class AnimalCache {
        public DateTime ValidUntil { get; set; }
        public List<AnimalModel> List { get; set; }
    }
}
