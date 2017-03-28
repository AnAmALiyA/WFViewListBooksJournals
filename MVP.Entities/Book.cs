using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public List<Author> Authors { get; set; }
        public string Name { get; set; }
        public DateTime Date {get; set;}
        public int Pages { get; set; }
    }
}