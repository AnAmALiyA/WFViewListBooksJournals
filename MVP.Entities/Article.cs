using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Article
    {
        public Article()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}