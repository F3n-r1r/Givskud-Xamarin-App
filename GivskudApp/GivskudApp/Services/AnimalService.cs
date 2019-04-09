﻿using System;
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
    public class Model {
        public int Area {get; set;}
    }

    public class AnimalService
    {

        public List<AnimalModel> Animal = null;

        public AnimalService()
        {

            Animal = null;

        }
        public void Fetch() {

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
        public void SortByArea(int AreaID) {

            Animal = Animal == null || Animal.Count == 0 ? Animal : Animal.Where(x => x.AreaID == AreaID).ToList();

        }
    }
}
