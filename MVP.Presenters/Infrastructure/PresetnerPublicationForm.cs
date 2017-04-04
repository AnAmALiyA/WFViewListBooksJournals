using System;
using System.Collections.Generic;
using System.Linq;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Presenters.Services;
using WFViewListBooksJournals.Views.Interfaces;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class PresetnerPublicationForm
    {
        private IPublicationForm _publicationForm;
        private UnitOfWork _unitOfWork;
        private readonly string _publication;
        private int Selected { get; set; }
        private Validation ValidationClass { get; set; }
        private enum EnumSystem { NoSelected = -1 };
        private string[] Publications { get; set; }
        private enum EnumPublications { Book, Journal, Newspaper }

        public PresetnerPublicationForm(IPublicationForm publicationForm, string publication, string connectionString)
        {
            _publicationForm = publicationForm;
            _unitOfWork = new UnitOfWork(connectionString);
            _publication = publication;

            ValidationClass = new Validation();
            Publications = new string[] { "Book", "Journal", "Newspaper" };
        }

        public void InitializeComponentPublicationForm()
        {
            FillComboBoxAllAuthors();
            FillListBoxPublication();
        }

        private void FillComboBoxAllAuthors()
        {
            _publicationForm.ClearComboBoxAuthors();

            List<Author> authors = _unitOfWork.Author.GetAll();
            string[] arrayAuthor = new string[authors.Count];
            for (int i = 0; i < authors.Count; i++)
            {
                arrayAuthor[i] = authors[i].GetStringAuthor();
            }
            _publicationForm.FillComboBoxAuthors(arrayAuthor);
        }

        public void FillListBoxPublication()
        {
            try
            {
                if (Publications[(int)EnumPublications.Book] == _publication)
                {
                    FillListBoxBooks();
                    return;
                }

                if (Publications[(int)EnumPublications.Journal] == _publication)
                {
                    FillListBoxJournals();
                    return;
                }

                if (Publications[(int)EnumPublications.Newspaper] == _publication)
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

            var books = _unitOfWork.Books.GetAll();
            FillListBoxMain(books);
        }

        private void FillListBoxJournals()
        {
            _publicationForm.ClearListBoxMain();

            var journals = _unitOfWork.Journals.GetAll();
            FillListBoxMain(journals);
        }

        private void FillListBoxNewspapers()
        {
            _publicationForm.ClearListBoxMain();

            var newspapers = _unitOfWork.Newspapers.GetAll();
            FillListBoxMain(newspapers);
        }

        private void FillListBoxMain(IEnumerable<Book> books)
        {
            string titlePublication = Publications[(int)EnumPublications.Book];
            _publicationForm.FillListBoxMain(titlePublication);
            foreach (Book book in books)
            {
                _publicationForm.FillListBoxMain(book.Authors.GetStringAuthors(), book.Name, book.Date.Year.ToString(), book.Pages);
            }
        }

        private void FillListBoxMain(IEnumerable<Journal> journals)
        {
            string titlePublication = Publications[(int)EnumPublications.Journal];
            _publicationForm.FillListBoxMain(titlePublication);
            foreach (Journal journal in journals)
            {
                foreach (Article article in journal.Articles)
                {
                    _publicationForm.FillListBoxMain(article.Authors.GetStringAuthors(), article.Title, journal.Name, journal.Date.Year.ToString(), journal.NumberIssue, article.Location);
                }
            }
        }

        private void FillListBoxMain(IEnumerable<Newspaper> newspapers)
        {
            string titlePublication = Publications[(int)EnumPublications.Newspaper];
            _publicationForm.FillListBoxMain(titlePublication);
            foreach (Newspaper newspaper in newspapers)
            {
                foreach (Article article in newspaper.Articles)
                {
                    _publicationForm.FillListBoxMain(article.Authors.GetStringAuthors(), article.Title, newspaper.Name, newspaper.Date.Year.ToString(), newspaper.Date.ToString("d.MMMM"), article.Location);
                }
            }
        }

        public void FillFieldsForm(int selected)
        {
            Selected = selected;
            int selectedAuthor = default(int);

            if (EnumPublications.Book.ToString() == _publication)
            {
                List<Book> books = _unitOfWork.Books.GetAll().ToList();
                Book book = books[selected];

                Author indexAuthor = book.Authors.ToList()[default(int)];
                selectedAuthor = _unitOfWork.Author.GetAll().IndexOf(indexAuthor);

                _publicationForm.FillFields(selectedAuthor, book.Name, book.Date, book.Pages);
            }

            if (EnumPublications.Journal.ToString() == _publication)
            {
                var journals = _unitOfWork.Journals.GetAll().ToList();
                Journal journal = FindPublication(journals, selected);

                if (journal == null)
                {
                    return;
                }

                Author indexAuthor = journal.Articles.ToList()[default(int)].Authors.ToList()[default(int)];
                selectedAuthor = _unitOfWork.Author.GetAll().IndexOf(indexAuthor);

                Article articleJournal = journal.Articles.ToList()[default(int)];

                _publicationForm.FillFields(selectedAuthor, articleJournal.Title, articleJournal.Location, journal.Name, journal.Date, journal.NumberIssue);
            }

            if (EnumPublications.Newspaper.ToString() == _publication)
            {
                var newspapers = _unitOfWork.Newspapers.GetAll().ToList();
                Newspaper newspaper = FindPublication(newspapers, selected);

                if (newspaper == null)
                {
                    return;
                }

                Author indexAuthor = newspaper.Articles.ToList()[default(int)].Authors.ToList()[default(int)];
                selectedAuthor = _unitOfWork.Author.GetAll().IndexOf(indexAuthor);

                Article articleNewspaper = newspaper.Articles.ToList()[default(int)];

                _publicationForm.FillFields(selectedAuthor, articleNewspaper.Title, articleNewspaper.Location, newspaper.Name, newspaper.Date);
            }
        }

        private Journal FindPublication(List<Journal> journals, int selected)
        {
            int tempIndex = 0;
            foreach (Journal journal in journals)
            {
                foreach (Article article in journal.Articles)
                {
                    if (tempIndex == selected)
                    {
                        var tempArticle = new List<Article>() { new Article() { Id = article.Id, Authors = article.Authors, Title = article.Title, Location = article.Location } };
                        return new Journal() { Id = journal.Id, Articles = tempArticle, Name = journal.Name, Date = journal.Date, NumberIssue = journal.NumberIssue };
                    }
                    tempIndex++;
                }
            }
            return null;
        }

        private Newspaper FindPublication(List<Newspaper> newspapers, int selected)
        {
            int tempIndex = 0;
            foreach (Newspaper newspaper in newspapers)
            {
                foreach (Article article in newspaper.Articles)
                {
                    if (tempIndex == selected)
                    {
                        var tempArticle = new List<Article>() { new Article() { Id = article.Id, Authors = article.Authors, Title = article.Title, Location = article.Location } };
                        return new Newspaper() { Id = newspaper.Id, Articles = tempArticle, Name = newspaper.Name, Date = newspaper.Date };
                    }
                    tempIndex++;
                }
            }
            return null;
        }

        public bool Validation(string author, string namePublication, string pages, DateTime date, string title, string location)
        {
            bool valid = false;

            valid = CheckEmptyFields(author, namePublication, pages, title, location);
            if (valid)
            {
                return true;
            }

            valid = CompareInsertValueFields(author, namePublication, pages, date, title, location);
            if (valid)
            {
                return true;
            }

            valid = ValidationData(author, namePublication, pages, date, title, location);
            if (valid)
            {
                return true;
            }
            return false;
        }
        
        private bool CheckEmptyFields(string author, string namePublication, string pages, string title, string location)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                return ValidationClass.CheckEmptyFields(author, namePublication, pages);
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                return ValidationClass.CheckEmptyFields(author, namePublication, pages, title, location);
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                return ValidationClass.CheckEmptyFields(author, namePublication, title, location);
            }
            return false;
        }

        private bool CompareInsertValueFields(string author, string namePublication, string pages, DateTime date, string title, string location)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                return _unitOfWork.Books.Find(author, namePublication, date, pages);
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                return _unitOfWork.Journals.Find(author, namePublication, pages, date, title, location);
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                return _unitOfWork.Newspapers.Find(author, namePublication, date, title, location);
            }
            return false;
        }

        private bool ValidationData(string author, string namePublication, string pages, DateTime date, string title, string location)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                return ValidationClass.ValidationData(namePublication, date, pages);
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                return ValidationClass.ValidationData(namePublication, pages, date, title, location);
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                return ValidationClass.ValidationData(namePublication, date, title, location);
            }
            return false;
        }

        public void AddDataReposotiry(string author, string namePublication, string pages, DateTime date, string title, string location)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                _unitOfWork.Books.Create(author, namePublication, date, pages);
                return;
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                _unitOfWork.Journals.Create(author, namePublication, pages, date, title, location);
                return;
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                _unitOfWork.Newspapers.Create(author, namePublication, date, title, location);
                return;
            }
        }

        public void UpdateReposotiry(string author, string namePublication, string pages, DateTime date, string title, string location)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                List<Book> books = _unitOfWork.Books.GetAll().ToList();
                Book book = books[Selected];

                _unitOfWork.Books.Udate(book, author, namePublication, date, pages, Selected);
                return;
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                var journals = _unitOfWork.Journals.GetAll().ToList();
                Journal journal = FindPublication(journals, Selected);

                _unitOfWork.Journals.Udate(journal, author, namePublication, pages, date, title, location, Selected);
                return;
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                var newspapers = _unitOfWork.Newspapers.GetAll().ToList();
                Newspaper newspaper = FindPublication(newspapers, Selected);

                _unitOfWork.Newspapers.Udate(newspaper, author, namePublication, date, title, location, Selected);
                return;
            }
        }

        public void Delete(int selectedIndex)
        {
            if (Publications[(int)EnumPublications.Book] == _publication)
            {
                var books = _unitOfWork.Books.GetAll().ToList();
                Book book = books[selectedIndex];
                _unitOfWork.Books.Delete(book);
            }

            if (Publications[(int)EnumPublications.Journal] == _publication)
            {
                var journals = _unitOfWork.Journals.GetAll().ToList();
                Journal journal = FindPublication(journals, selectedIndex);
                _unitOfWork.Journals.Delete(journal);
            }

            if (Publications[(int)EnumPublications.Newspaper] == _publication)
            {
                var newspapers = _unitOfWork.Newspapers.GetAll().ToList();
                Newspaper newspaper = FindPublication(newspapers, selectedIndex);
                _unitOfWork.Newspapers.Delete(newspaper);
            }
            FillListBoxPublication();
        }
    }
}
