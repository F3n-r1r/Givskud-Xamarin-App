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
      
        public List<AnimalModel> Animal { get; }

        public AnimalService()
        {

            ApiResource ApiResource = new ApiResource();
            string ApiResourceJson = ApiResource.Get("/animals/get");

            if(ApiResourceJson != null) {
                try {
                    Animal = JsonConvert.DeserializeObject<List<AnimalModel>>(ApiResourceJson); 
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("NewsService: Cannot deserialize object. {0}", e.Message);
                    Animal = new List<AnimalModel>();
                }
            } else {
                Animal = new List<AnimalModel>();
            }

        }
    }
}
