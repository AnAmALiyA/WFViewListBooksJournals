using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date {get; set;}
        public int Pages { get; set; }

        public virtual List<Author> Authors { get; set; }
    }
}