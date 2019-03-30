﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Diagnostics;

namespace GivskudApp.ResourceControllers {
    class ApiResourceController {

        private string _Api = "https://givskudbackoffice20190327114413.azurewebsites.net/umbraco/api";
        private string _Endpoint;
        private string _AcceptFormat;
        private Encoding _Encoding;
        private HttpClient _Client;
        private List<RequestHeader> _Headers = new List<RequestHeader>();

        public ApiResourceController(string _endpoint, List<RequestHeader> _headers = null, string _accept = null, Encoding _encoding = null) {
            
            _Endpoint = _endpoint;
            _Headers = _headers ?? new List<RequestHeader>();
            _AcceptFormat = _accept ?? "application/json";
            _Encoding = _encoding ?? Encoding.UTF8;
            _Client = ClientInit();
        }
        public async Task<string> Get() {
            HttpResponseMessage Response = await _Client.GetAsync(_Api + _Endpoint).ConfigureAwait(false);
            return Response.IsSuccessStatusCode ? await Response.Content.ReadAsStringAsync().ConfigureAwait(false) : null;
        }
        private HttpClient ClientInit() {

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
