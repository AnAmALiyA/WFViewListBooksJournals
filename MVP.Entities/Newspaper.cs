using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Newspaper
    {
        public Newspaper()
        {
            Articles = new List<Article>();
        }
        public int Id { get; set; }        
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}