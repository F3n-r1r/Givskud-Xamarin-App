using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

using Newtonsoft.Json;

using Plugin.Settings;

using GivskudApp.Models;
using GivskudApp.Resources;
using GivskudApp.CryptographyService;

namespace GivskudApp.Services {

    class PassService
    {

        public event EventHandler IsPassInvalidEvent;

        private CacheResource CacheResource { get; set;}
        private ApiResource ApiResource { get; set; }

        public SeasonPassModel Data { get; private set; }

        public PassService()
        {
            CacheResource = new CacheResource(ConfigurationManager.RemoteResources.Local.Pass);
            ApiResource = new ApiResource(ConfigurationManager.RemoteResources.Remote.Pass);
        }
        public void Remove()
        {
            Task.Run(() =>
            {
                CacheResource.Remove();
                Data = null;
            });
        }
        public void Get(string _Input = null)
        {
            
            // Fetch

            if(_Input == null)
            {
                // Loog for stored pass
                ResourceResponse Response = CacheResource.Get();
                if(Response.IsValid)
                {
                    SeasonPassModel Pass = JsonConvert.DeserializeObject<SeasonPassModel>(Response.Data);
                    if(IsPassValid(Pass.ValidTo))
                    {
                        Data = Pass;
                    } else
                    {
                        Data = null;
                        IsPassInvalidEvent?.Invoke(this, EventArgs.Empty);
                    }
                }
            } else
            {

                string InputEncrypted = EncDecService.Hash(_Input);
                
                if(ApiResource.Headers.ContainsKey("PassID"))
                {
                    ApiResource.Headers["PassID"] = InputEncrypted;
                } else
                {
                    ApiResource.Headers.Add("PassID", InputEncrypted);
                }
                
                ResourceResponse Response = ApiResource.Get();

                if(Response.IsValid)
                {

                    List<SeasonPassModel> Temp = JsonConvert.DeserializeObject<List<SeasonPassModel>>(Response.Data);
                    
                    if (Temp.Count > 0)
                    {
                        SeasonPassModel Pass = Temp[0];
                        if(IsPassValid(Pass.ValidTo))
                        {
                            CacheResource.AddOrUpdate(JsonConvert.SerializeObject(Pass));
                            Data = Pass;
                        } else
                        {
                            IsPassInvalidEvent?.Invoke(this, EventArgs.Empty);
                            Data = null;
                        }
                    }
                    else
                    {
                        IsPassInvalidEvent?.Invoke(this, EventArgs.Empty);
                        Data = null;
                    }
                    
                }

            }

            // Decrypt 

            if(Data != null)
            {
                Data.Holder = EncDecService.Decrypt(Data.Holder);
            }

        }
        private bool IsPassValid(string ValidTo)
        {

            DateTime To = DateTime.ParseExact(ValidTo, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date;
            DateTime Today = DateTime.Now.Date;

            return To.CompareTo(Today) >= 0;

        }
    }
    /*
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
    */
}
