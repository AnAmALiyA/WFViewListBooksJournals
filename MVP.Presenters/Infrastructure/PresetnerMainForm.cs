using System;
using System.Collections.Generic;
using System.Linq;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Views.Interfaces;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class PresetnerMainForm
    {
        private IMainForm _mainForm;
        private AuthorRepository _authorRepository;
        private BookRepository _bookRepository;
        private JournalRepository _journalRepository;
        private NewspaperRepository _newspaperRepository;

        private List<string> Publications { get; set; }
        public enum EnumPublications { Book, Journal, Newspaper, Empty = 0 }

        public PresetnerMainForm(IMainForm mainForm)
        {
            _mainForm = mainForm;
            _authorRepository = AuthorRepository.Instance;
            _bookRepository = BookRepository.Instance;
            _journalRepository = JournalRepository.Instance;
            _newspaperRepository = NewspaperRepository.Instance;

            Publications = new List<string>() { "Book", "Journal", "Newspaper" };
        }

        public void InitializeComponentMainForm()
        {
            FillComboBoxAllAuthors();
            FillComboBoxAllPublications();
            FillListBoxMain();
        }
        
        public void FillComboBoxAllAuthors()
        {
            _mainForm.ClearComboBoxAuthors();

            List<Author> authorsList = _authorRepository.GetAll();            
            _mainForm.FillComboBoxAuthors(authorsList);
        }

        private void FillComboBoxAllPublications()
        {
            _mainForm.ClearComboBoxPublications();
            _mainForm.FillComboBoxPublications(Publications);
        }

        public void FillListBoxMain()
        {
            List<Book> bookList = _bookRepository.GetAll();            
            List<Journal> journalList = _journalRepository.GetAll();
            List<Newspaper> newspaperList = _newspaperRepository.GetAll();

            _mainForm.FillListBoxMain(Publications, bookList, journalList, newspaperList);
        }

        public void ShowAllArticles()
        {
            try
            {
                _mainForm.ClearListBoxMain();

                var queryJournal = from journal in _journalRepository.GetAll()
                                   from article in journal.Articles
                                   select article;                
                _mainForm.FillListBoxMain(queryJournal);

                var queryNewspaper = from newpaper in _newspaperRepository.GetAll()
                                     from article in newpaper.Articles
                                     select article;
                _mainForm.FillListBoxMain(queryNewspaper);
            }
            catch (Exception ex)
            {
                _mainForm.FillListBoxMain(ex.Message);
            }
        }        

        public void SaveBook()
        {
           _bookRepository.SaveDB();
        }

        public void SaveJournals()
        {
            _journalRepository.Save();
        }

        public void SaveNewspaper()
        {
            _newspaperRepository.Save();
        }

        public void FillListBoxPublicationAuthor(Author author)
        {
            _mainForm.ClearListBoxMain();
                        
            var queryBooks = GetAllBooks(author);
            if (queryBooks.Count() != (int)EnumPublications.Empty)
            {                
                _mainForm.FillListBoxMainBooks(Publications, queryBooks);
            }

            var queryJournals = GetAllJournals(author);
            if (queryJournals.Count() != (int)EnumPublications.Empty)
            {               
                _mainForm.FillListBoxMainJournals(Publications, queryJournals);
            }

            var queryNewspapers = GetAllNewspapers(author);
            if (queryNewspapers.Count() != (int)EnumPublications.Empty)
            {                
                _mainForm.FillListBoxMainNewspapers(Publications, queryNewspapers);
            }
        }

        private IEnumerable<Book> GetAllBooks(Author author)
        {
            var queryBooks = _bookRepository.GetAll().Where(b => b.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0);            

            return queryBooks;
        }

        private IEnumerable<Journal> GetAllJournals(Author author)
        {
            var queryJournals = _journalRepository.GetAll().Where(j => j.Articles.Where(ar => ar.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0).Count() != 0);

            return queryJournals;
        }

        private IEnumerable<Newspaper> GetAllNewspapers(Author author)
        {
            var queryNewspapers = _newspaperRepository.GetAll().Where(j => j.Articles.Where(ar => ar.Authors.Where(a => a.FirstName == author.FirstName & a.SecondName == author.SecondName & a.LastName == author.LastName & a.Age == author.Age).Count() != 0).Count() != 0);

            return queryNewspapers;
        }

    }
}
