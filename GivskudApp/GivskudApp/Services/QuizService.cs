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
    class QuizService
    {
        public List<QuizModel> Data { get; private set; }

        public QuizService()
        {
            Data = new List<QuizModel>();
        }

        public void Get(string id = null)
        {
            ApiResource ApiResource = new ApiResource();
            string ApiResourceUrl = "/quiz/get" + (id == null ? "" : "?id=" + id);
            string ApiResourceJson = ApiResource.Get(ApiResourceUrl);

            if(ApiResourceJson != null)
            {
                try
                {
                    Data = JsonConvert.DeserializeObject<List<QuizModel>>(ApiResourceJson);
                } catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("QuizService: Cannot deserialize object. {0}", e.Message);
                    Data = new List<QuizModel>();
                }
            } else
            {
                Data = new List<QuizModel>();
            }

        }
    }
}
