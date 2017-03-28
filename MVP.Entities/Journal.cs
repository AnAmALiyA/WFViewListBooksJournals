using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Journal
    {
        public List<Article> Articles { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string NumberIssue { get; set; }
    }
}