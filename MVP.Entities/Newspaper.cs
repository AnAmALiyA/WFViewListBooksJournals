using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Newspaper
    {
        public List<Article> Articles { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}