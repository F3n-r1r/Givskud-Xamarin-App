using GivskudApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GivskudApp.Services
{
    public class NewsService
    {
        public List<NewsModel> News { get; }

        // ###
        // In future iteration of the app this data should come from an api
        // ###
        public NewsService() // Need to use Encoding to make it utf-8 (ø æ å)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GivskudApp.MockData.NewsMockData.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                List<NewsModel> list = JsonConvert.DeserializeObject<List<NewsModel>>(json);

                News = new List<NewsModel>(list.OrderBy(n => n.PublishDate));
            }
        }
    }
}
