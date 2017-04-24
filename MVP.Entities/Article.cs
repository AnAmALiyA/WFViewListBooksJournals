using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFViewListBooksJournals.Entities
{
    public class Article
    {
        public Article() { }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }

        [ForeignKey("Author")]
        public virtual List<Author> Authors { get; set; }
    }
}