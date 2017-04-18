using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Servises;
using System.Collections;
using static WFViewListBooksJournals.Presenters.Infrastructure.PresetnerMainForm;

namespace WFViewListBooksJournals.Forms
{
    public partial class MainForm : Form, IMainForm
    {
        private PresetnerMainForm _presetnerMain;
        private DisplayOfData _displayData;

        private List<Author> _authorList;
        private Dictionary<int, Book> _bookListForIndex;
        private Dictionary<int, Journal> _journalListForIndex;
        private Dictionary<int, Newspaper> _newspaperListForIndex;
        
        public ArrayList DataListBoxMain { get; set; }

        public MainForm()
        {
            InitializeComponent();
            
            _presetnerMain = new PresetnerMainForm(this);
            _displayData = DisplayOfData.Instance;           

            InitializeComponentMainForm();
        }

        public void InitializeComponentMainForm()
        {
            _presetnerMain.InitializeComponentMainForm();
        }
       
        public void ClearListBoxMain()
        {
            _displayData.ClearListBox(listBoxMainForm);
        }

        public void FillListBoxMain(List<string> publication, List<Book> bookList, List<Journal> journalList, List<Newspaper> newspaperList)
        {
            _displayData.ClearListBox(listBoxMainForm);

            _bookListForIndex = new Dictionary<int, Book>();
            _journalListForIndex = new Dictionary<int, Journal>();
            _newspaperListForIndex = new Dictionary<int, Newspaper>();

            FillListBoxMainBooks(publication, bookList);

            _displayData.FillListBoxEmptyLine(listBoxMainForm);
            FillListBoxMainJournals(publication, journalList);

            _displayData.FillListBoxEmptyLine(listBoxMainForm);
            FillListBoxMainNewspapers(publication, newspaperList);
        }
        
        public void FillListBoxMainBooks(List<string> publication, IEnumerable<Book> bookList)
        {
            _displayData.FillListBox(listBoxMainForm, publication[(int)EnumPublications.Book]);
            foreach (var book in bookList)
            {
                _displayData.FillListBox(listBoxMainForm, book, _bookListForIndex);
            }
        }

        public void FillListBoxMainJournals(List<string> publication, IEnumerable<Journal> journalList)
        {
            _displayData.FillListBox(listBoxMainForm, publication[(int)EnumPublications.Journal]);
            foreach (var journal in journalList)
            {
                _displayData.FillListBox(listBoxMainForm, journal, _journalListForIndex);               
            }
        }

        public void FillListBoxMainNewspapers(List<string> publication, IEnumerable<Newspaper> newspaperList)
        {
            _displayData.FillListBox(listBoxMainForm, publication[(int)EnumPublications.Newspaper]);
            foreach (var newspaper in newspaperList)
            {
                _displayData.FillListBox(listBoxMainForm, newspaper, _newspaperListForIndex);
            }
        }

        public void FillListBoxMain(IEnumerable<Article> articles)
        {
            foreach (Article article in articles)
            {
                _displayData.FillListBox(listBoxMainForm, article);
            }
        }

        public void FillListBoxMain(string text)
        {
            _displayData.ClearListBox(listBoxMainForm);
            _displayData.FillListBox(listBoxMainForm, text);
        }

        public void ClearComboBoxAuthors()
        {
            _displayData.ClearComboBox(comboBoxAuthors);
        }

        public void ClearComboBoxPublications()
        {
            _displayData.ClearComboBox(comboBoxPublications);
        }

        public void FillComboBoxAuthors(List<Author> authorList)
        {
            _authorList = authorList;
            var authorStringList = _authorList.Select(x => _displayData.GetStringAuthor(x));
            _displayData.FillComboBox(comboBoxAuthors, authorStringList);
        }

        public void FillComboBoxPublications(List<string> publications)
        {
            _displayData.FillComboBox(comboBoxPublications, publications);
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            _presetnerMain.FillListBoxMain();
        }

        private void buttonShowAllArticles_Click(object sender, EventArgs e)
        {
            _presetnerMain.ShowAllArticles();
        }

        private void buttonSaveBooks_Click(object sender, EventArgs e)
        {
            _presetnerMain.SaveBook();
        }

        private void buttonSaveJournals_Click(object sender, EventArgs e)
        {
            _presetnerMain.SaveJournals();
        }

        private void buttonSaveNewspapers_Click(object sender, EventArgs e)
        {
            _presetnerMain.SaveNewspaper();
        }

        private void comboBoxAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int authorId = comboBoxAuthors.SelectedIndex;
            Author author = _authorList[authorId];
            _presetnerMain.FillListBoxPublicationAuthor(author);
        }

        private void comboBoxPublications_SelectedIndexChanged(object sender, EventArgs e)
        {
            string publication = comboBoxPublications.SelectedItem.ToString();
            PublicationForm form = new PublicationForm(publication, this);
            form.Show();
        }
    }
}
