using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Presenters;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Common;

namespace WFViewListBooksJournals.Forms
{
    public partial class AuthorForm : Form, IAuthorForm
    {
        private AuthorFormPresenter _presetnerAuthorForm;
        private DisplayOfData _displayData;
        private IPublicationForm _publicationForm;
        private Validation _validation;
        private Dictionary<string, bool> Nationality { get; set; }

        public AuthorForm(IPublicationForm publicationForm)
        {
            InitializeComponent();

            _presetnerAuthorForm = new AuthorFormPresenter(this);
            _displayData = DisplayOfData.Instance;
            _publicationForm = publicationForm;
            _validation = Validation.Instance;

            InitializeComponentAuthorForm();
        }

        public void InitializeComponentAuthorForm()
        {
            _presetnerAuthorForm.InitializeComponentAuthorForm();
        }

        public void InitializeComponentAuthorForm(Dictionary<string, bool> nationality)
        {
            Nationality = nationality;
            var list = Nationality.Select(s => s.Key);
            _displayData.FillComboBox(comboBoxNationality, list);
        }

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            bool valid =_validation.ValidationDataAuthor(textBoxFirstName.Text, textBoxSecondName.Text, textBoxLastName.Text, textBoxAge.Text, comboBoxNationality.Text);            
            if (!valid)
            {
                return;
            }
            int age = Int32.Parse(textBoxAge.Text);
            bool nationality = Nationality[comboBoxNationality.SelectedItem.ToString()];            

            Author author = new Author() { FirstName = textBoxFirstName.Text, SecondName = textBoxSecondName.Text, LastName = textBoxLastName.Text, Age = age, InitialsOption = nationality };
            bool exist = _presetnerAuthorForm.ExistAuthor(author);
            if (exist)
            {
                return;
            }
            _presetnerAuthorForm.Save(author);            
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void BeforeClosed(object sender, FormClosingEventArgs e)
        {
            _publicationForm.FillOnlyComboBoxAuthors();
        }
    }
}
