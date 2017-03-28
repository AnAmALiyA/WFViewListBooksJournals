using System;

namespace WFViewListBooksJournals.Views.ModelView
{
    public class BookView
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Pages { get; set; }
    }
}
