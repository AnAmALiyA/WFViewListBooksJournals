using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Presenters;
using WFViewListBooksJournals.Presenters.Common;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Common;

namespace WFViewListBooksJournals.Forms
{
    public partial class PublicationForm : Form, IPublicationForm
    {
        private readonly string _currentPublication;
        private DisplayOfData _displayData;
        private IMainForm _mainForm;
        private PublicationFormPresenter _presetnerPublication;
        private Validation _validation;

        private Dictionary<string, string> PublicationText { get; set; }
        private List<Author> _authorList;

        private Dictionary<int, Book> _bookListForIndex;
        private Dictionary<int, Journal> _journalListForIndex;
        private Dictionary<int, Newspaper> _newspaperListForIndex;

        
        private int SelectedIndexPublication { get; set; }

        public PublicationForm(string publication, IMainForm mainForm)
        {
            InitializeComponent();

            this.Text = publication;
            _currentPublication = publication;
            _displayData = DisplayOfData.Instance;
            _mainForm = mainForm;
            _presetnerPublication = new PublicationFormPresenter(this, publication);
            _validation = Validation.Instance;

            PublicationText = new Dictionary<string, string>();
            PublicationText.Add("NumberIssue", "Number Issue");
            PublicationText.Add("NameJournal", "Name Journal");

            SelectPublicationForm(publication);
            InitializeComponentPublicationForm();

            SelectedIndexPublication = (int)SystemVariablesPublications.NoSelectedIndex;
        }

        public void InitializeComponentPublicationForm()
        {
            _presetnerPublication.InitializeComponentPublicationForm();
        }

        private void SelectPublicationForm(string publication)
        {
            if (PublicationsType.Book.ToString() == _currentPublication)
            {
                AdjustDisplayBooks();
                _bookListForIndex = new Dictionary<int, Book>();
                return;
            }

            if (PublicationsType.Journal.ToString() == _currentPublication)
            {
                AdjustDisplayJournals();
                _journalListForIndex = new Dictionary<int, Journal>();
                return;
            }

            if (PublicationsType.Newspaper.ToString() == _currentPublication)
            {
                AdjustDisplayNewspapers();
                _newspaperListForIndex = new Dictionary<int, Newspaper>();
            }
        }

        private void AdjustDisplayBooks()
        {
            labelTitle.Visible = false;
            textBoxTitle.Visible = false;

            labelLocation.Visible = false;
            textBoxLocation.Visible = false;
        }

        private void AdjustDisplayJournals()
        {
            labelPages.Text = PublicationText["NumberIssue"];
            labelNamePublications.Text = PublicationText["NameJournal"];
        }

        private void AdjustDisplayNewspapers()
        {
            labelPages.Visible = false;
            textBoxPages.Visible = false;
        }

        public void ClearComboBoxAuthors()
        {
            _displayData.ClearComboBox(comboBoxAuthor);
        }

        public void FillOnlyComboBoxAuthors()
        {
            _presetnerPublication.FillComboBoxAllAuthors();
        }

        public void FillComboBoxAuthors(List<Author> authorList)
        {
            _authorList = authorList;
            var authorStringList = authorList.Select(x => _displayData.GetStringAuthor(x));
            _displayData.FillComboBox(comboBoxAuthor, authorStringList);
        }

        public void ClearListBoxMain()
        {
            _displayData.ClearListBox(listBoxPublicationForm);
        }

        public void FillListBoxMain(string publication)
        {
            _displayData.FillListBox(listBoxPublicationForm, publication);
        }

        public void FillListBoxMain(List<Book> bookList)
        {
            _bookListForIndex = new Dictionary<int, Book>();
            foreach (var book in bookList)
            {
                _displayData.FillListBox(listBoxPublicationForm, book, _bookListForIndex);
            }
        }

        public void FillListBoxMain(List<Journal> journalList)
        {
            _journalListForIndex = new Dictionary<int, Journal>();
            foreach (var journal in journalList)
            {
                _displayData.FillListBox(listBoxPublicationForm, journal, _journalListForIndex);
            }
        }

        public void FillListBoxMain(List<Newspaper> newspaperList)
        {
            _newspaperListForIndex = new Dictionary<int, Newspaper>();
            foreach (var newspaper in newspaperList)
            {
                _displayData.FillListBox(listBoxPublicationForm, newspaper, _newspaperListForIndex);
            }
        }

        private void listBoxPublicationForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexPublication = listBoxPublicationForm.SelectedIndex;
        }

        private void listBoxPublicationForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxPublicationForm.SelectedIndex == default(int) || listBoxPublicationForm.SelectedIndex == (int)SystemVariablesPublications.NoSelectedIndex)
            {
                return;
            }

            FillFieldsForm(SelectedIndexPublication);
        }

        private void FillFieldsForm(int SelectedIndex)
        {
            if (PublicationsType.Book.ToString() == _currentPublication)
            {
                var selectedBook = _bookListForIndex[SelectedIndex];
                FillFields(selectedBook);
                return;
            }

            if (PublicationsType.Journal.ToString() == _currentPublication)
            {
                var selectedJournal = _journalListForIndex[SelectedIndex];
                FillFields(selectedJournal);
                return;
            }

            if (PublicationsType.Newspaper.ToString() == _currentPublication)
            {
                var selectedNewspaper = _newspaperListForIndex[SelectedIndex];
                FillFields(selectedNewspaper);
            }
        }

        private void FillFields(Book book)
        {
            var authorList = book.Authors;
            int indexAuthor = GetIndexAuthor(authorList);

            comboBoxAuthor.SelectedIndex = indexAuthor;
            textBoxName.Text = book.Name;
            dateTimePickerDate.Value = book.Date;
            textBoxPages.Text = book.Pages.ToString();
        }

        private void FillFields(Journal journal)
        {
            var authorList = journal.Articles.First().Authors;
            int indexAuthor = GetIndexAuthor(authorList);
            comboBoxAuthor.SelectedIndex = indexAuthor;

            string journalTitle = journal.Articles.First().Title;
            textBoxTitle.Text = journalTitle;

            string journalLocation = journal.Articles.First().Location;
            textBoxLocation.Text = journalLocation;

            textBoxName.Text = journal.Name;
            dateTimePickerDate.Value = journal.Date;
            textBoxPages.Text = journal.NumberIssue;
        }

        private void FillFields(Newspaper newspaper)
        {
            var authorList = newspaper.Articles.First().Authors;
            int indexAuthor = GetIndexAuthor(authorList);

            comboBoxAuthor.SelectedIndex = indexAuthor;

            string newspaperTitle = newspaper.Articles.First().Title;
            textBoxTitle.Text = newspaperTitle;

            string newspaperLocation = newspaper.Articles.First().Location;
            textBoxLocation.Text = newspaperLocation;

            textBoxName.Text = newspaper.Name;
            dateTimePickerDate.Value = newspaper.Date;
        }

        private int GetIndexAuthor(ICollection<Author> authors)
        {
            var firstAuthor = authors.First();
            var stringAuthor = _displayData.GetStringAuthor(firstAuthor);
            int indexAuthor = _authorList.IndexOf(firstAuthor);
            return indexAuthor;
        }

        private void ClearFields()
        {
            comboBoxAuthor.SelectedIndex = (int)SystemVariablesPublications.NoSelectedIndex;
            textBoxName.Text = string.Empty;
            dateTimePickerDate.Value = DateTime.Now;
            textBoxPages.Text = string.Empty;
            textBoxTitle.Text = string.Empty;
            textBoxLocation.Text = string.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var selectedAuthor = _authorList[comboBoxAuthor.SelectedIndex];
            bool isAdd = false;

            if (PublicationsType.Book.ToString() == _currentPublication)
            {
                isAdd = buttonAdd_Click_Book(selectedAuthor);
            }

            if (PublicationsType.Journal.ToString() == _currentPublication)
            {
                isAdd = buttonAdd_Click_Journal(selectedAuthor);
            }

            if (PublicationsType.Newspaper.ToString() == _currentPublication)
            {
                isAdd = buttonAdd_Click_Newspaper(selectedAuthor);
            }

            if (isAdd)
            {
                ClearFields();
                _presetnerPublication.FillListBoxPublication();
            }
        }

        private bool buttonAdd_Click_Book(Author selectedAuthor)
        {
            var selectedBook = _bookListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedBook, selectedAuthor, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
            if (valid)
            {
                int pages = Int32.Parse(textBoxPages.Text);
                _presetnerPublication.AddDataReposotiry(selectedAuthor, textBoxName.Text, dateTimePickerDate.Value, pages);
                return true;
            }
            return false;
        }

        private bool buttonAdd_Click_Journal(Author selectedAuthor)
        {
            var selectedJournal = _journalListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedJournal, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
            if (valid)
            {
                _presetnerPublication.AddDataReposotiry(selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
                return true;
            }
            return false;
        }

        private bool buttonAdd_Click_Newspaper(Author selectedAuthor)
        {
            var selectedNewspaper = _newspaperListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedNewspaper, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value);
            if (valid)
            {
                _presetnerPublication.AddDataReposotiry(selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value);
                return true;
            }
            return false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var selectedAuthor = _authorList[comboBoxAuthor.SelectedIndex];
            bool isAdd = false;

            if (PublicationsType.Book.ToString() == _currentPublication)
            {
                isAdd = buttonUpdate_Click_Book(selectedAuthor);
            }

            if (PublicationsType.Journal.ToString() == _currentPublication)
            {
                isAdd = buttonUpdate_Click_Journal(selectedAuthor);
            }

            if (PublicationsType.Newspaper.ToString() == _currentPublication)
            {
                isAdd = buttonUpdate_Click_Newspaper(selectedAuthor);
            }

            if (isAdd)
            {
                ClearFields();
                _presetnerPublication.FillListBoxPublication();
            }
        }

        private bool buttonUpdate_Click_Book(Author selectedAuthor)
        {
            var selectedBook = _bookListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedBook, selectedAuthor, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
            if (valid)
            {
                int pages = Int32.Parse(textBoxPages.Text);
                _presetnerPublication.UpdateReposotiry(selectedBook, selectedAuthor, textBoxName.Text, dateTimePickerDate.Value, pages);
                return true;
            }
            return false;
        }

        private bool buttonUpdate_Click_Journal(Author selectedAuthor)
        {
            var selectedJournal = _journalListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedJournal, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
            if (valid)
            {
                _presetnerPublication.UpdateReposotiry(selectedJournal, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value, textBoxPages.Text);
                return true;
            }
            return false;
        }

        private bool buttonUpdate_Click_Newspaper(Author selectedAuthor)
        {
            var selectedNewspaper = _newspaperListForIndex[SelectedIndexPublication];

            bool valid = _validation.ValidationDataPublication(selectedNewspaper, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value);
            if (valid)
            {
                _presetnerPublication.UpdateReposotiry(selectedNewspaper, selectedAuthor, textBoxTitle.Text, textBoxLocation.Text, textBoxName.Text, dateTimePickerDate.Value);
                return true;
            }
            return false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxPublicationForm.SelectedIndex == default(int) || listBoxPublicationForm.SelectedIndex == (int)SystemVariablesPublications.NoSelectedIndex)
            {
                return;
            }

            if (PublicationsType.Book.ToString() == _currentPublication)
            {
                buttonDelete_Click_Book();
            }

            if (PublicationsType.Journal.ToString() == _currentPublication)
            {
                buttonDelete_Click_Journal();
            }

            if (PublicationsType.Newspaper.ToString() == _currentPublication)
            {
                buttonDelete_Click_Newspaper();
            }
        }

        private void buttonDelete_Click_Book()
        {
            var selectedBook = _bookListForIndex[SelectedIndexPublication];
            _presetnerPublication.Delete(selectedBook);

            SelectedIndexPublication = (int)SystemVariablesPublications.NoSelectedIndex;
            _presetnerPublication.FillListBoxPublication();
        }

        private void buttonDelete_Click_Journal()
        {
            var selectedJournal = _journalListForIndex[SelectedIndexPublication];
            _presetnerPublication.Delete(selectedJournal);

            SelectedIndexPublication = (int)SystemVariablesPublications.NoSelectedIndex;
            _presetnerPublication.FillListBoxPublication();
        }

        private void buttonDelete_Click_Newspaper()
        {
            var selectedNewspaper = _newspaperListForIndex[SelectedIndexPublication];
            _presetnerPublication.Delete(selectedNewspaper);

            SelectedIndexPublication = (int)SystemVariablesPublications.NoSelectedIndex;
            _presetnerPublication.FillListBoxPublication();
        }

        private void buttonNewAuthor_Click(object sender, EventArgs e)
        {
            AuthorForm authorForm = new AuthorForm(this);
            authorForm.Show();
        }

        private void BeforeClosed(object sender, FormClosingEventArgs e)
        {
            _mainForm.InitializeComponentMainForm();
        }
    }
}
