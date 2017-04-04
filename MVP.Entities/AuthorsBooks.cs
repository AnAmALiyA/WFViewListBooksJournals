using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFViewListBooksJournals.Entities
{
    public class AuthorsBooks
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }
    }
}
