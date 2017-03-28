namespace WFViewListBooksJournals.Models.Repositories
{
    public class UnitOfWork
    {
        private BookRepository _book;
        private JournalRepository _journal;
        private NewspaperRepository _newspaper;
        public AllLiterary AllLiterary { get; set; }

        public UnitOfWork()
        {
            AllLiterary = new AllLiterary();
        }

        public BookRepository Books
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(AllLiterary);
                }
                return _book;
            }
        }

        public JournalRepository Journals
        {
            get
            {
                if (_journal == null)
                {
                    _journal = new JournalRepository(AllLiterary);
                }
                return _journal;
            }
        }

        public NewspaperRepository Newspapers
        {
            get
            {
                if (_newspaper == null)
                {
                    _newspaper = new NewspaperRepository(AllLiterary);
                }
                return _newspaper;
            }
        }
    }
}
