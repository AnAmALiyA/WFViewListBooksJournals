using System;
using System.Collections.Generic;
using System.Linq;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Presenters.Services;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class PresetnerMainForm
    {
        private IMainForm _mainForm;
        private UnitOfWork _unitOfWork;
        private string[] Publications { get; set; }
        private enum EnumPublications { Book, Journal, Newspaper, Empty = 0 }
        public PresetnerMainForm(IMainForm mainForm, string connectionString)
        {
            _mainForm = mainForm;
            _unitOfWork = new UnitOfWork(connectionString);
            Publications = new string[] { "Book", "Journal", "Newspaper" };
        }

        public void InitializeComponentMainForm()
        {
            FillComboBoxAllAuthors();
            FillComboBoxAllPublications();
            FillListBoxAllPublications();
        }
        
        public void FillComboBoxAllAuthors()
        {
            _mainForm.ClearComboBoxAuthors();

            List<Author> authors = _unitOfWork.Author.GetAll();
            string[] arrayAuthor = new string[authors.Count];
            for (int i = 0; i < authors.Count; i++)
            {
                arrayAuthor[i] = authors[i].GetStringAuthor();
            }
            _mainForm.FillComboBoxAuthors(arrayAuthor);
        }

        private void FillComboBoxAllPublications()
        {
            _mainForm.ClearComboBoxPublications();
            _mainForm.FillComboBoxPublications(Publications);
        }

        public void FillListBoxAllPublications()
        {
            _mainForm.ClearListBoxMain();
            IEnumerable<Book> books = _unitOfWork.Books.GetAll();
            FillListBoxMain(books);

            _mainForm.FillListBoxMain(string.Empty);           
            IEnumerable<Journal> journals = _unitOfWork.Journals.GetAll();
            FillListBoxMain(journals);
           
            _mainForm.FillListBoxMain(string.Empty);            
            IEnumerable<Newspaper> newspapers = _unitOfWork.Newspapers.GetAll();
            FillListBoxMain(newspapers);
        }

        private void FillListBoxMain(IEnumerable<Book> books)
        {
            string titlePublication = Publications[(int)EnumPublications.Book];
            _mainForm.FillListBoxMain(titlePublication);
            foreach (Book book in books)
            {
                _mainForm.FillListBoxMain(book.Authors.GetStringAuthors(), book.Name, book.Date.Year.ToString(), book.Pages);
            }
        }

        private void FillListBoxMain(IEnumerable<Journal> journals)
        {
            string titlePublication = Publications[(int)EnumPublications.Journal];
            _mainForm.FillListBoxMain(titlePublication);
            foreach (Journal journal in journals)
            {
                foreach (Article article in journal.Articles)
                {
                    _mainForm.FillListBoxMain(article.Authors.GetStringAuthors(), article.Title, journal.Name, journal.Date.Year.ToString(), journal.NumberIssue, article.Location);
                }
            }
        }

        private void FillListBoxMain(IEnumerable<Newspaper> newspapers)
        {
            string titlePublication = Publications[(int)EnumPublications.Newspaper];
            _mainForm.FillListBoxMain(titlePublication);
            foreach (Newspaper newspaper in newspapers)
            {
                foreach (Article article in newspaper.Articles)
                {
                    _mainForm.FillListBoxMain(article.Authors.GetStringAuthors(), article.Title, newspaper.Name, newspaper.Date.Year.ToString(), newspaper.Date.ToString("d.MMMM"), article.Location);
                }
            }
        }

        public void ShowAllArticles()
        {
            _mainForm.ClearListBoxMain();

            try
            {
                var queryJournal = from journal in _unitOfWork.Journals.GetAll()
                                   from article in journal.Articles
                                   select article;

                FillListBoxArticles(queryJournal);

                var queryNewspaper = from newpaper in _unitOfWork.Newspapers.GetAll()
                                     from article in newpaper.Articles
                                     select article;

                FillListBoxArticles(queryNewspaper);
            }
            catch (Exception ex)
            {
                _mainForm.ClearListBoxMain();
                _mainForm.FillListBoxMain(ex.Message);
            }
        }

        private void FillListBoxArticles(IEnumerable<Article> articles)
        {
            foreach (Article article in articles)
            {
                _mainForm.FillListBoxMain(article.Authors.GetStringAuthors(), article.Title, article.Location);
            }
        }

        public void SaveBook()
        {
            _unitOfWork.Books.SaveDB();
        }

        public void SaveJournals()
        {
            _unitOfWork.Journals.Save();
        }

        public void SaveNewspaper()
        {
            _unitOfWork.Newspapers.Save();
        }

        public void FillListBoxPublicationAuthor(string selectedAuthor)
        {
            Author author = _unitOfWork.Author.GetAll().Find(a => a.GetStringAuthor() == selectedAuthor);            

            _mainForm.ClearListBoxMain();
                        
            var queryBooks = GetAllBooks(author);
            if (queryBooks.Count() != (int)EnumPublications.Empty)
            {
                FillListBoxMain(queryBooks);
            }

            var queryJournals = GetAllJournals(author);
            if (queryJournals.Count() != (int)EnumPublications.Empty)
            {
                FillListBoxMain(queryJournals);
            }

            var queryNewspapers = GetAllNewspapers(author).Distinct();
            if (queryNewspapers.Count() != (int)EnumPublications.Empty)
            {
                FillListBoxMain(queryNewspapers);
            }
        }

        private IEnumerable<Book> GetAllBooks(Author author)
        {
            var queryBooks = _unitOfWork.Books.GetAll().Where(b => b.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0);            

            return queryBooks;
        }

        private IEnumerable<Journal> GetAllJournals(Author author)
        {
            var queryJournals = _unitOfWork.Journals.GetAll().Where(j => j.Articles.Where(ar => ar.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0).Count() != 0);

            return queryJournals;
        }
        private IEnumerable<Newspaper> GetAllNewspapers(Author author)
        {
            var queryNewspapers = _unitOfWork.Newspapers.GetAll().Where(j => j.Articles.Where(ar => ar.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0).Count() != 0);

            return queryNewspapers;
        }

    }
}
