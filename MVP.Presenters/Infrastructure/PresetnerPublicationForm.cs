using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Views.Interfaces;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class PresetnerPublicationForm
    {
        private IPublicationForm _publicationForm;

        private readonly string _publication;
        private AuthorRepository _authorRepository;
        private BookRepository _bookRepository;
        private JournalRepository _journalRepository;
        private NewspaperRepository _newspaperRepository;

        private enum EnumPublications { Book, Journal, Newspaper }

        public PresetnerPublicationForm(IPublicationForm publicationForm, string publication)
        {
            _publicationForm = publicationForm;
            _publication = publication;

            _authorRepository = AuthorRepository.Instance;
            _bookRepository = BookRepository.Instance;
            _journalRepository = JournalRepository.Instance;
            _newspaperRepository = NewspaperRepository.Instance;
        }

        public void InitializeComponentPublicationForm()
        {
            FillComboBoxAllAuthors();
            FillListBoxPublication();
        }

        public void FillComboBoxAllAuthors()
        {
            _publicationForm.ClearComboBoxAuthors();

            List<Author> authorList = _authorRepository.GetAll();
            _publicationForm.FillComboBoxAuthors(authorList);
        }

        public void FillListBoxPublication()
        {
            try
            {
                if (EnumPublications.Book.ToString() == _publication)
                {
                    FillListBoxBooks();
                    return;
                }
                if (EnumPublications.Journal.ToString() == _publication)
                {
                    FillListBoxJournals();
                    return;
                }

                if (EnumPublications.Newspaper.ToString() == _publication)
                {
                    FillListBoxNewspapers();
                    return;
                }
            }
            catch (Exception ex)
            {
                _publicationForm.ClearListBoxMain();
                _publicationForm.FillListBoxMain(ex.Message);
            }
        }

        private void FillListBoxBooks()
        {
            _publicationForm.ClearListBoxMain();
            
            var books = _bookRepository.GetAll();
            _publicationForm.FillListBoxMain(EnumPublications.Book.ToString());
            _publicationForm.FillListBoxMain(books);
        }

        private void FillListBoxJournals()
        {
            _publicationForm.ClearListBoxMain();

            var journals = _journalRepository.GetAll();
            _publicationForm.FillListBoxMain(EnumPublications.Journal.ToString());
            _publicationForm.FillListBoxMain(journals);
        }

        private void FillListBoxNewspapers()
        {
            _publicationForm.ClearListBoxMain();

            var newspapers = _newspaperRepository.GetAll();
            _publicationForm.FillListBoxMain(EnumPublications.Newspaper.ToString());
            _publicationForm.FillListBoxMain(newspapers);
        }

        public void AddDataReposotiry(Author author, string namePublication, DateTime date, int pages)
        {
            _bookRepository.Create(author, namePublication, date, pages);
        }

        public void AddDataReposotiry(Author author, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            _journalRepository.Create(author, title, location, namePublication, date, numberIssue);
        }

        public void AddDataReposotiry(Author author, string title, string location, string namePublication, DateTime date)
        {
            _newspaperRepository.Create(author, title, location, namePublication, date);
        }

        public void UpdateReposotiry(Book selectedBook, Author selectedAuthor, string namePublication, DateTime date, int pages)
        {
            _bookRepository.Update(selectedBook, selectedAuthor, namePublication, date, pages);
        }

        public void UpdateReposotiry(Journal selectedJournal, Author selectedAuthor, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            _journalRepository.Update(selectedJournal, selectedAuthor, title, location, namePublication, date, numberIssue);
        }

        public void UpdateReposotiry(Newspaper selectedNewspaper, Author selectedAuthor, string title, string location, string namePublication, DateTime date)
        {
            _newspaperRepository.Update(selectedNewspaper, selectedAuthor, title, location, namePublication, date);
        }

        public void Delete(Book book)
        {
            _bookRepository.Delete(book);
        }

        public void Delete(Journal journal)
        {
            _journalRepository.Delete(journal);
        }

        public void Delete(Newspaper newspaper)
        {
            _newspaperRepository.Delete(newspaper);
        }


    }
}
