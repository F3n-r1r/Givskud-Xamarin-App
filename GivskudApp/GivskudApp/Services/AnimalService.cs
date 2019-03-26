using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using GivskudApp.Models;
using System.Reflection;
using System.IO;
using System.Linq;

namespace GivskudApp.Services
{
    public class AnimalService
    {

        public List<AnimalModel> Data = new List<AnimalModel>();

 



        public List<AnimalModel> Animal { get; }

        public AnimalService() // Need to use Encoding to make it utf-8 (ø æ å)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GivskudApp.MockData.AnimalMockData.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                List<AnimalModel> list = JsonConvert.DeserializeObject<List<AnimalModel>>(json);

                Animal = new List<AnimalModel>(list.OrderBy(n => n.Name));
            }
        }

    }
}
