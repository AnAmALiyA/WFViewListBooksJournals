using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFViewListBooksJournals.Entities
{
    public class Book
    {
        public Book() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Pages { get; set; }

        [ForeignKey("Author")]
        public virtual List<Author> Authors { get; set; }
    }
}