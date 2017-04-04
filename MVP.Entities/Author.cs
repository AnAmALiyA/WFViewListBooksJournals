﻿using System.Collections.Generic;

namespace WFViewListBooksJournals.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool InitialsOption { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
