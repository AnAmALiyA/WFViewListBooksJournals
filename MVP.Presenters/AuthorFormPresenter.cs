using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Views.Interfaces;

namespace WFViewListBooksJournals.Presenters
{
    public class AuthorFormPresenter
    {
        private IAuthorForm _authorForm;
        private AuthorRepository _authorRepository;
        private Dictionary<string, bool> Nationality { get; set; }

        public AuthorFormPresenter(IAuthorForm authorForm)
        {
            _authorForm = authorForm;
            _authorRepository = AuthorRepository.Instance;
            
            Nationality = new Dictionary<string, bool>() {
                { "ru", false },
                { "us", true }
            };
        }

        public void InitializeComponentAuthorForm()
        {
            _authorForm.InitializeComponentAuthorForm(Nationality);
        }

        public bool ExistAuthor(Author author)
        {
            return _authorRepository.ExistAuthor(author);
        }

        public void Save(Author author)
        {
            _authorRepository.Add(author);
        }
    }
}
