using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deneysan_DAL.Entities;

namespace deneysan.Models
{
    public class HomePageWrapperModel
    {
        public IEnumerable<News> news { get; set; }
        public IEnumerable<References> references { get; set; }

        public HomePageWrapperModel(IEnumerable<News> news ,IEnumerable<References> references )
        {
            this.news = news;
            this.references = references;
        }
    }
}