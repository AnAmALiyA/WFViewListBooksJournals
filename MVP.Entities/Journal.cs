using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Journal
    {
        public Journal()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }        
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string NumberIssue { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}