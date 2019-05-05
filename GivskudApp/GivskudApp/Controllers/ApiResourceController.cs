using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

using GivskudApp.Views;
using GivskudApp.Controllers;

namespace GivskudApp.ResourceControllers {

    class ApiResource {

        private string AuthenticationToken = "C4oILgIT7dTqLye9LJZ0Hr9Xedp7RleQAxw5NVHE";    
        private string ApiBaseUri = "https://givskud-app-admin20190504114828.azurewebsites.net/umbraco/api";

        public string Get(string Endpoint, Dictionary<string,string> Headers = null) {

            return GetAsync(Endpoint, Headers).Result;

        }

        private async Task<string> GetAsync(string Route, Dictionary<string,string> Headers) {
            
            using(HttpClient Client = GetClient(Headers)) {

                string Result = null;

                try {

                    HttpResponseMessage HttpRequest = await Client.GetAsync(ApiBaseUri + Route).ConfigureAwait(false);
                    
                    if(HttpRequest.StatusCode == HttpStatusCode.OK) {
                        
                        if(HttpRequest.Content != null) {
                            HttpContentHeaders ContentHeaders = HttpRequest.Content.Headers;
                            if(ContentHeaders.ContentType.MediaType == "application/json") {

                                Result = await HttpRequest.Content.ReadAsStringAsync();
                                Console.WriteLine("Test success");

                            } else {
                                Console.WriteLine("Test failure: Response body is expected to be in 'application/json' format.");
                            }
                        } else {
                            Console.WriteLine("Test failure: Request content is null");
                        }

                    } else {
                        switch(HttpRequest.StatusCode) {
                            case HttpStatusCode.Unauthorized:
                                Console.WriteLine("Test failure: Unauthorized");
                                break;
                            case HttpStatusCode.NotFound:
                                Console.WriteLine("Test failure: Resource not found");
                                break;
                            default:
                                Console.WriteLine("Test failure: Status code");
                                break;
                        }
                    }

                } catch (Exception e) {
                    Console.WriteLine("Unhandled exception: {0}", e.Message);
                }

                return Result;

            }

        }

        private HttpClient GetClient(Dictionary<string, string> Headers = null) {

            HttpClient Client = new HttpClient();

            // Charset header
            Client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            // Authentication
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthenticationToken);

            // Additional headers

            if(Headers != null) {
                foreach(KeyValuePair<string,string> Entry in Headers) {
                    Client.DefaultRequestHeaders.Add(Entry.Key, Entry.Value);
                }
            }

            return Client;

        }

        public bool IsConnected()
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                return true;
            } else
            {
                return false;
            }
        }

    }

    [Obsolete]
    class ApiResourceController {

        private string _Api = "https://givskudbackoffice20190402102444.azurewebsites.net/umbraco/api";
        private string _Endpoint;
        private string _AcceptFormat;
        private Encoding _Encoding;
        private List<RequestHeader> _Headers = new List<RequestHeader>();

        public ApiResourceController(string _endpoint, List<RequestHeader> _headers = null, string _accept = null, Encoding _encoding = null) {
            _Endpoint = _endpoint;
            _Headers = _headers ?? new List<RequestHeader>();
            _AcceptFormat = _accept ?? "application/json";
            _Encoding = _encoding ?? Encoding.UTF8;
        }
        public async Task<string> Get() {
            using(HttpClient _Client = GetClient()) {
                try {
                    HttpResponseMessage Request = await _Client.GetAsync(_Api + _Endpoint).ConfigureAwait(false);
                    if(Request.StatusCode == System.Net.HttpStatusCode.OK) {
                        return await Request.Content.ReadAsStringAsync();
                    } else {
                        return null;
                    }
                } catch (Exception e) {
                    return null;
                }
            }
        }
        private HttpClient GetClient() {

            const string AuthKey = "C4oILgIT7dTqLye9LJZ0Hr9Xedp7RleQAxw5NVHE";

            HttpClient C = new HttpClient();

            C.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthKey);
            C.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_AcceptFormat));

            if(_Headers.Count() > 0) {
                foreach(RequestHeader Header in _Headers) {
                    C.DefaultRequestHeaders.Add(Header.Key, Header.Value);
                }
            }

            return C;
        }
    }
    class RequestHeader {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
