using WFViewListBooksJournals.Models.Services;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class UnitOfWork
    {
        private DataBase AllLiterary { get; set; }
        private readonly ADOContext _context;

        private AuthorRepository author;
        private BookRepository book;
        private JournalRepository journal;
        private NewspaperRepository newspaper;        

        public UnitOfWork(string connectionString)
        {
            AllLiterary = new DataBase();
            _context = new ADOContext(connectionString);
        }
       
        public AuthorRepository Author
        {
            get
            {
                if(author == null)
                {
                    author = new AuthorRepository(AllLiterary);
                }
                return author;
            }
        }
        public BookRepository Books
        {
            get
            {
                if (book == null)
                {
                    book = new BookRepository(AllLiterary, _context, this);
                }
                return book;
            }
        }

        public JournalRepository Journals
        {
            get
            {
                if (journal == null)
                {
                    journal = new JournalRepository(AllLiterary, this);
                }
                return journal;
            }
        }

        public NewspaperRepository Newspapers
        {
            get
            {
                if (newspaper == null)
                {
                    newspaper = new NewspaperRepository(AllLiterary, this);
                }
                return newspaper;
            }
        }
    }
}
