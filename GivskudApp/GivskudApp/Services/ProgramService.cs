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
        public List<ProgramModel> Program { get; }

        public ProgramService() // Need to use Encoding to make it utf-8 (ø æ å)
        {

            ApiResource ApiResource = new ApiResource();
            string ApiResourceJson = ApiResource.Get("/events/get");

            if(ApiResourceJson != null) {
                try {
                    List<ProgramModel> Temporary = JsonConvert.DeserializeObject<List<ProgramModel>>(ApiResourceJson);
                    for(int i = 0; i < Temporary.Count; i++) {
                        if(Temporary[i].IsBoundToDate) {
                            if(DateTime.Compare(Temporary[i].EventDate.Date, DateTime.Now.Date) == 0) {
                                return;
                            } else {
                                Temporary.RemoveAt(i);
                            }
                        } else {
                            return;
                        }
                    }
                    Program = Temporary;
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("ProgramService: Cannot deserialize object. {0}", e.Message);
                    Program = new List<ProgramModel>();
                }
            } else {
                Program = new List<ProgramModel>();
            }
            

            /*
            ApiResourceController ApiResource = new ApiResourceController("/events/get");
            using(var Response = ApiResource.Get()) {
                if(Response.Result != null) {
                    List<ProgramModel> Temp = JsonConvert.DeserializeObject<List<ProgramModel>>(Response.Result);
                    for(int i = 0; i < Temp.Count; i++) {
                        if(Temp[i].IsBoundToDate) {
                            if(DateTime.Compare(Temp[i].EventDate.Date, DateTime.Now.Date) == 0) {
                                return;
                            } else {
                                Temp.RemoveAt(i);
                            }
                        } else {
                            return;
                        }
                    }
                    Program = Temp;
                } else {
                    Program = new List<ProgramModel>();
                }
            }
            */
        }
    }
}
