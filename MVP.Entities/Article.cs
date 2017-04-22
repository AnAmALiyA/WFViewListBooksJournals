using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Article
    {
        public Article()
        {
            Authors = new List<Author>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        
        public virtual List<Author> Authors { get; set; }
    }
}