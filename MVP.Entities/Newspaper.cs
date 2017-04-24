using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFViewListBooksJournals.Entities
{
    public class Newspaper
    {
        public Newspaper() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Article")]
        public virtual List<Article> Articles { get; set; }
    }
}