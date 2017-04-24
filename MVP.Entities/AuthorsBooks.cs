using System.ComponentModel.DataAnnotations.Schema;

namespace WFViewListBooksJournals.Entities
{
    public class AuthorsBooks
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
    }
}
