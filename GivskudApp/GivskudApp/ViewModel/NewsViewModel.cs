using DLToolkit.Forms.Controls;
using GivskudApp.Models;
using GivskudApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GivskudApp.ViewModel
{
    public class NewsViewModel
    {
        NewsService service = new NewsService();
        int currentNews;

        public List<NewsModel> News { get { return service.News; } }
        
        // The specific News item that is clicked (Used for -> NewsDetailsPage)
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

        public void Refresh()
        {
            service.Get();
        }

    }
}
