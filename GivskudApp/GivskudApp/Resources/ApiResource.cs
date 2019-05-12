using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Resources;

using Plugin.Settings;

namespace GivskudApp.Resources
{
    public class ApiResource : Resource
    {

        public Dictionary<string, string> Headers { get; set; }
        public string Attribute { get; set; }

        public ApiResource(string Path) : base(Path)
        {
            Headers = new Dictionary<string, string>();
            Attribute = string.Empty;
        }
        public override ResourceResponse Get()
        {

            var AsyncRequest = GetAsync();

            return new ResourceResponse
            {
                IsValid = String.IsNullOrEmpty(AsyncRequest.Result) ? false : true,
                Data = String.IsNullOrEmpty(AsyncRequest.Result) ? null : AsyncRequest.Result
            };

        }

        private async Task<string> GetAsync()
        {

            using (HttpClient Client = GetClient())
            {

                string Result = null;

                try
                {

                    HttpResponseMessage HttpRequest = await Client.GetAsync(GetApiBase() + Path + Attribute).ConfigureAwait(false);

                    if (HttpRequest.StatusCode == HttpStatusCode.OK)
                    {

                        if (HttpRequest.Content != null)
                        {
                            HttpContentHeaders ContentHeaders = HttpRequest.Content.Headers;
                            if (ContentHeaders.ContentType.MediaType == "application/json")
                            {
                                Result = await HttpRequest.Content.ReadAsStringAsync();
                            }
                        }

                    }
                    else
                    {
                        switch (HttpRequest.StatusCode)
                        {
                            case HttpStatusCode.Unauthorized:
                                Console.WriteLine("ApiResource failure: Unauthorized");
                                break;
                            case HttpStatusCode.NotFound:
                                Console.WriteLine("ApiResource failure: Resource not found");
                                break;
                            default:
                                Console.WriteLine("ApiResource failure: Status code");
                                break;
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Unhandled exception: {0}", e.Message);
                }

                return Result;

            }

        }
        private HttpClient GetClient()
        {

            HttpClient Client = new HttpClient();

            // Charset header
            Client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            // Authentication
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetApiKey());

            // Additional headers
            if(Headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> Entry in Headers)
                {
                    Client.DefaultRequestHeaders.Add(Entry.Key, Entry.Value);
                }
            }

            return Client;

        }
        private string GetApiBase()
        {
            return ConfigurationManager.ApiConfiguration.ApiRoot;
        }
        private string GetApiKey()
        {
            return ConfigurationManager.ApiConfiguration.ApiAuth;
        }

    }
}
