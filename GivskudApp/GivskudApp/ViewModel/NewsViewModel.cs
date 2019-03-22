using GivskudApp.Models;
using GivskudApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GivskudApp.ViewModel
{
    public class NewsViewModel
    {
        NewsService service = new NewsService();
        int currentNews;

        public IList<NewsModel> News { get { return service.News; } }

        public NewsModel SelectedNews
        {
            get
            {
                return service.News[currentNews];
            }
            set
            {
                int index = service.News.IndexOf(value);
                if (index >= 0)
                {
                    currentNews = index;
                }
            }
        }

    }
}
