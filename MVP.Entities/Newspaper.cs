using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Newspaper
    {
        public Newspaper()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }        
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}