using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using GivskudApp.ResourceControllers;
using GivskudApp.Models;

namespace GivskudApp.Services
{
    public class ProgramService
    {
        public List<ProgramModel> Program { get; private set; }

        public ProgramService()
        {
            Program = new List<ProgramModel>();

            ApiResource ApiResource = new ApiResource();
            string ApiResourceJson = ApiResource.Get("/events/get");

            if(ApiResourceJson != null)
            {
                try
                {
                    Program = JsonConvert.DeserializeObject<List<ProgramModel>>(ApiResourceJson);
                } catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
