using System;
using System.Collections.Generic;
using System.Linq;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Services;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class BookRepository
    {
        private static IEnumerable<Book> ListEntities { get; set; }
        private readonly ADOContext _context;
        private UnitOfWork _unitOfWork;

        public BookRepository(AllLiterary allLiterary, ADOContext context, UnitOfWork unitOfWork)
        {
            ListEntities = allLiterary.Books as IEnumerable<Book>;
            this._context = context;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Book> GetAll()
        {
            return ListEntities;
        }

        public void SaveDB()
        {
            _context.DeleteBooksAuthors();
            _context.DeleteAuthors();
            _context.DeleteBooks();

            _context.SaveBooks(ListEntities);
        }

        public bool Find(string author, string namePublication, DateTime date, string pages)
        {
            foreach (var book in ListEntities)
            {
                if(book.Name != namePublication & book.Date != date & book.Pages.ToString() != pages)
                {
                    continue;
                }
                foreach (var item in book.Authors)
                {
                    if (item.GetStringAuthor()!=author)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Create(string author, string name, DateTime date, string pages)
        {
            ICollection<Author> collectionAuthor = new List<Author>();
            Author item = _unitOfWork.Author.GetAll().Find(x => x.GetStringAuthor() == author);
            collectionAuthor.Add(item);

            List<Book> books = ListEntities.ToList();
            books.Add(new Book() { Authors = collectionAuthor, Name=name, Date=date, Pages=Int32.Parse(pages) });
            ListEntities = books as ICollection<Book>;
        }

        public void Udate(Book book, string author, string name, DateTime date, string pages, int selected)
        {
            bool existAuthor = false;
            foreach (Author itemAuthor in book.Authors)
            {
                if (itemAuthor.GetStringAuthor()==author)
                {
                    existAuthor = true;
                    return;
                }
            }

            if (!existAuthor)
            {
                Author newAuthor = _unitOfWork.Author.GetAll().Find(a => a.GetStringAuthor() == author);
                book.Authors.Add(newAuthor);
            }            

            book.Name = name;
            book.Date = date;
            book.Pages = Int32.Parse(pages);
            
            var books = GetAll().ToList();
            books[selected] = book;
            ListEntities = books as ICollection<Book>;
        }
        
        public void Delete(Book book)
        {
            var books = GetAll();

            books = books.Except(new List<Book>(){ book });

            ListEntities = books;
        }
    }
}
