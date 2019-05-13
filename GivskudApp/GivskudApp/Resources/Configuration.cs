using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.Resources
{
    public static class ConfigurationManager
    {
        public class RemoteResources
        {
            public class Remote
            {
                public const string Animals = "/animals/get";
                public const string News = "/news/get";
                public const string Quizes = "/quiz/get";
                public const string Pass = "/seasonpass/get";
                public const string Events = "/events/get";
            }
            public class Local
            {
                public const string Animals = "applicationResourceCacheAnimals";
                public const string News = "applicationResourceCacheNews";
                public const string Quizes = "applicationResourceCacheQuiz";
                public const string Pass = "applicationResourceCachePass";
                public const string Events = "applicationResourceCacheEvents";
            }
        }
        public class ApiConfiguration
        {
            public const string ApiRoot = "https://givskudmobileapplicationbackoffice.azurewebsites.net/umbraco/api";
            public const string ApiAuth = "C4oILgIT7dTqLye9LJZ0Hr9Xedp7RleQAxw5NVHE";
        }
        public class AppConfiguration
        {
            public const string LanguagePreset = "applicationLanguagePreset";
            public const string Tickets = "applicationTicketCache";
        }
    };
};
