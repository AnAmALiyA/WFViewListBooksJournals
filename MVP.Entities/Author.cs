using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Author
    {
        public Author() { }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool InitialsOption { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}