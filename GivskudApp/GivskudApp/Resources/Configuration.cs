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
            }
            public class Local
            {
                public const string Animals = "applicationResourceCacheAnimals";
                public const string News = "applicationResourceCacheNews";
                public const string Quizes = "applicationResourceCacheQuiz";
                public const string Pass = "applicationResourceCachePass";
            }
        }
        public class ApiConfiguration
        {
            // public const string ApiRoot = "https://givskud-app-admin20190504114828.azurewebsites.net/umbraco/api";
            public const string ApiRoot = "https://860d3a86.ngrok.io/umbraco/api";
            public const string ApiAuth = "C4oILgIT7dTqLye9LJZ0Hr9Xedp7RleQAxw5NVHE";
        }
        public class AppConfiguration
        {
            public const string LanguagePreset = "applicationLanguagePreset";
        }
    };
};
