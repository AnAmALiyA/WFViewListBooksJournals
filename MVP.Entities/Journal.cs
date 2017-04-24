using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFViewListBooksJournals.Entities
{
    public class Journal
    {
        public Journal() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string NumberIssue { get; set; }

        [ForeignKey("Article")]
        public virtual List<Article> Articles { get; set; }
    }
}