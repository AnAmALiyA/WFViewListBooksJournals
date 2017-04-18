using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Services;
using System.Collections;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class BookRepository
    {
        private static BookRepository _instance;
        private DataBase _dataBase;
        private readonly ADOContext _context;
        

        public BookRepository()
        {
            _dataBase = DataBase.Instance;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; //или занятся поиском в приложении
            _context = new ADOContext(connectionString);
        }

        public static BookRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookRepository();
                }
                return _instance;
            }
        }

        public List<Book> GetAll()
        {
            List<Book> tempListBook = new List<Book>();
            var books = _dataBase.Books;
            tempListBook.AddRange(books);
            return tempListBook;
        }

        public void SaveDB()
        {
            _context.DeleteBooksAuthors();
            _context.DeleteAuthors();
            _context.DeleteBooks();

            _context.SaveBooks(_dataBase.Books);
        }
        
        public void Create(Author author, string namePublication, DateTime date, int pages)
        {
            List<Author> collectionAuthor = new List<Author>();            
            collectionAuthor.Add(author);
            
            var newBook = new Book() { Authors = collectionAuthor, Name = namePublication, Date = date, Pages = pages };
            _dataBase.Books.Add(newBook);
        }

        public void Update(Book selectedBook, Author selectedAuthor, string namePublication, DateTime date, int pages)
        {
            var books = _dataBase.Books;
            var updateBook = books.First(x => x == selectedBook);

            updateBook.Name = namePublication;
            updateBook.Date = date;
            updateBook.Pages = pages;

            Author existAuthor = updateBook.Authors.First(a=>a == selectedAuthor);             
            if (existAuthor == null)
            {
                updateBook.Authors.Add(selectedAuthor);
            }
        }

        public void Delete(Book book)
        {
            var books = _dataBase.Books;
            books.Remove(book);
            _dataBase.Books = books;
        }
    }
}
