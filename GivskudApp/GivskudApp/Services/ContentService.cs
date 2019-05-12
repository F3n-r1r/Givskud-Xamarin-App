using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GivskudApp.Resources;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace GivskudApp.Services
{
    class ContentService : ServiceBase
    {
        public ContentService(string LocalPath, string OnlinePath) : base(LocalPath, OnlinePath)
        {

        }
    }
    public abstract class ServiceBase
    {

        public List<string> Log { get; set; }

        private bool UsesCache { get; set; }
        private string CacheBuffer { get; set; }
        private string ApiBuffer { get; set; }

        public int CacheValidityTime { get; set; }

        public ApiResource ApiResource { get; set; }
        public CacheResource CacheResource { get; set; }

        public ServiceBase(string LocalPath, string OnlinePath)
        {
            ApiResource = new ApiResource(OnlinePath);
            CacheResource = new CacheResource(LocalPath);
        }

        public virtual ServiceResponse Get()
        {
            ServiceResponse Response = GetFromResource();
            return Response;
        }
        public ServiceResponse GetFromResource()
        {

            // Create empty Response data
            string Response = null;
            bool IsDeviceConnected = IsDeviceOnline();

            if (IsDeviceConnected == true && GetFromApiResource())
            {
                UsesCache = false;
                Response = ApiBuffer;
            }
            else
            {
                if (GetFromCacheResource())
                {
                    UsesCache = true;
                    Response = CacheBuffer;
                }
            }

            return new ServiceResponse
            {
                Data = Response,
                UsesCache = UsesCache
            };

        }

        public void SetApiResourceHeaders(Dictionary<string, string> Headers)
        {
            ApiResource.Headers = Headers;
        }
        public void SetApiResourceAttribute(string Attribute)
        {
            ApiResource.Attribute = Attribute;
        }

        private bool GetFromCacheResource()
        {

            ResourceResponse Response = CacheResource.Get();

            if (Response.IsValid)
            {
                CacheBuffer = Response.Data;
            }

            return Response.IsValid;

        }
        private bool GetFromApiResource()
        {

            ResourceResponse Response = ApiResource.Get();

            if (Response.IsValid)
            {
                ApiBuffer = Response.Data;
                CacheResource.AddOrUpdate(Response.Data);
            }

            return Response.IsValid;

        }

        public bool IsDeviceOnline()
        {

            return CrossConnectivity.Current.IsConnected;

        }

    }
    public class ServiceResponse
    {

        public string Data { get; set; }
        public bool UsesCache { get; set; }

    }
}
