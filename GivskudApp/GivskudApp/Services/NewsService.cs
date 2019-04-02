using GivskudApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using GivskudApp.ResourceControllers;

namespace GivskudApp.Services
{
    public class NewsService
    {
        public List<NewsModel> News { get; }

        public NewsService()
        {
        
            ApiResource ApiResource = new ApiResource();
            string ApiResourceJson = ApiResource.Get("/news/get");

            if(ApiResourceJson != null) {
                try {
                    News = JsonConvert.DeserializeObject<List<NewsModel>>(ApiResourceJson);
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("NewsService: Cannot deserialize object. {0}", e.Message);
                    News = new List<NewsModel>();
                }
            } else {
                News = new List<NewsModel>();
            }

        }
    }
}
