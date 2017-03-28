using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Article
    {
        public List<Author> Author { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
    }
}