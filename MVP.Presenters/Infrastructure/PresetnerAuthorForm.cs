using System.Collections.Generic;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Views.Interfaces;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class PresetnerAuthorForm
    {
        private IAuthorForm _authorForm;
        private UnitOfWork _unitOfWork;
        private Validation ValidationClass { get; set; }
        private Dictionary<string, bool> Nationality { get; set; }

        public PresetnerAuthorForm(IAuthorForm authorForm, string connectionString)
        {
            _authorForm = authorForm;
            _unitOfWork = new UnitOfWork(connectionString);

            ValidationClass = new Validation();
            Nationality = new Dictionary<string, bool>() {
                { "ru", false },
                { "us", true }
            };
        }

        public void InitializeComponentAuthorForm()
        {
            _authorForm.InitializeComponentAuthorForm(Nationality);
        }
        
        public bool Save(string firstName, string secondName, string lastName, string age, string nationality)
        {
            bool valid = false;

            valid = ValidationClass.NullField(firstName, secondName, lastName, nationality);
            if (valid)
            {
                return false;
            }

            bool initialsOption = Nationality[nationality];

            if (age == string.Empty)
            {
                valid = ValidationClass.CheckCorrectnessDataAuthor(firstName, secondName, lastName);
            }

            if (age != string.Empty)
            {
                valid = ValidationClass.CheckCorrectnessDataAuthor(firstName, secondName, lastName, age);
            }
            
            if (valid)
            {
                return false;
            }

            valid = CompareInsertValueFields(firstName, secondName, lastName, age, initialsOption);
            if (valid)
            {
                return false;
            }
            _unitOfWork.Author.Add(firstName, secondName, lastName, age, initialsOption);

            return true;
        }

        private bool CompareInsertValueFields(string firstName, string secondName, string lastName, string age, bool initialsOption)
        {
            return _unitOfWork.Author.Find(firstName, secondName, lastName, age, initialsOption);            
        }
    }
}
