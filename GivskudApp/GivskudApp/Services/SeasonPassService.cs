using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

using Newtonsoft.Json;

using Plugin.Settings;

using GivskudApp.Models;
using GivskudApp.ResourceControllers;
using GivskudApp.CryptographyService;
using GivskudApp.Controllers;

namespace GivskudApp.Services {
    class SeasonPassService {

        public SeasonPassModel SeasonPass = null;
        private string _SeasonPassDataKey = "applicationSeasonPassObject";
        
        public void Remove() {

            Task.Run(() => {
                SeasonPass = null;
                CrossSettings.Current.Remove(_SeasonPassDataKey);
            });
            
        }

        public void Get(string input = null) {

            if(input == null) {

                string StoredValue = CrossSettings.Current.GetValueOrDefault(_SeasonPassDataKey, null);

                if(StoredValue == null) {
                    SeasonPass = null;
                } else {
                    SeasonPass = ReturnOrRefuse(StoredValue);
                }

            } else {

                string inputEncrypted = EncDecService.Hash(input);
                SeasonPass = ReturnOrRefuse(inputEncrypted);
                    
                if(SeasonPass != null) {
                    CrossSettings.Current.AddOrUpdateValue(_SeasonPassDataKey, inputEncrypted);
                }

            }

        }

        private SeasonPassModel ReturnOrRefuse(string encrypted) {
            
            ApiResource ApiResource = new ApiResource();
            Dictionary<string, string> ApiResourceHeaders = new Dictionary<string, string>();
            ApiResourceHeaders.Add("PassID", encrypted);
            string ApiResourceJson = ApiResource.Get("/seasonpass/get", ApiResourceHeaders);
            
            if(ApiResourceJson != null) {
                try {
                    List<SeasonPassModel> ResponseData = JsonConvert.DeserializeObject<List<SeasonPassModel>>(ApiResourceJson);
                    if(ResponseData != null && ResponseData.Count > 0) {
                        
                        SeasonPassModel Temp = ResponseData[0];
                        
                        if(IsPassValid(Temp.ValidTo)) {
                            
                            Temp.Holder = EncDecService.Decrypt(Temp.Holder);

                            CrossSettings.Current.AddOrUpdateValue(_SeasonPassDataKey, encrypted);

                            return Temp;

                        } else {
                            return null;
                        }
                    } else {
                        return null;
                    }
                } catch (Exception e) {
                    return null;
                }
            } else {
                return null;
            }
        }
        
        private bool IsPassValid(string ValidTo) {

            DateTime To = DateTime.ParseExact(ValidTo, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date;
            DateTime Today = DateTime.Now.Date;

            return To.CompareTo(Today) >= 0;

        }

    }
}
