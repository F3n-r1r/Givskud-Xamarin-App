using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using GivskudApp.Models;
using Newtonsoft.Json;

namespace GivskudApp.Services
{
    public class ProgramService
    {
        public IList<ProgramModel> Program { get; }

        // ###
        // In future iteration of the app this data should come from an api
        // ###
        public ProgramService() // Need to use Encoding to make it utf-8 (ø æ å)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GivskudApp.MockData.ProgramMockData.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                List<ProgramModel> list = JsonConvert.DeserializeObject<List<ProgramModel>>(json);

                Program = new List<ProgramModel>(list.OrderBy(n => n.Title));
            }
        }
    }
}
