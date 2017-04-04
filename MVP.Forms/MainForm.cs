using System;
using System.Configuration;
using System.Windows.Forms;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Servises;

namespace WFViewListBooksJournals.Forms
{
    public partial class MainForm : Form, IMainForm
    {
        private PresetnerMainForm _presetnerMain;
        private DisplayOfData DisplayData { get; set; }

        public MainForm()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _presetnerMain = new PresetnerMainForm(this, connectionString);
            DisplayData = new DisplayOfData();
            InitializeComponentMainForm();
        }
        public void InitializeComponentMainForm()
        {
            _presetnerMain.InitializeComponentMainForm();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            _presetnerMain.FillListBoxAllPublications();
        }

        public void ClearListBoxMain()
        {
            DisplayData.ClearListBox(listBoxMainForm);
        }

        public void FillListBoxMain(string publication)
        {
            DisplayData.FillListBox(listBoxMainForm, publication);
        }

        public void FillListBoxMain(string author, string nameBook, string year, int pages)
        {
            DisplayData.FillListBox(listBoxMainForm, author, nameBook, year, pages);
        }

        public void FillListBoxMain(string author, string articleTitle, string publication, string year, string numberIssu, string locationArticle)
        {
            DisplayData.FillListBox(listBoxMainForm, author, articleTitle, publication, year, numberIssu, locationArticle);
        }
        
        public void FillListBoxMain(string author, string title, string location)
        {
            DisplayData.FillListBox(listBoxMainForm, author, title, location);
        }

        public void ClearComboBoxAuthors()
        {
            DisplayData.ClearComboBox(comboBoxAuthors);
        }

        public void ClearComboBoxPublications()
        {
            DisplayData.ClearComboBox(comboBoxPublications);
        }

        public void FillComboBoxAuthors(string[] arrayAuthor)
        {
            DisplayData.FillComboBox(comboBoxAuthors, arrayAuthor);
        }

        public void FillComboBoxPublications(string[] arrayPublications)
        {
            DisplayData.FillComboBox(comboBoxPublications, arrayPublications);
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
            string authorId = comboBoxAuthors.SelectedItem.ToString();
            _presetnerMain.FillListBoxPublicationAuthor(authorId);
        }

        private void comboBoxPublications_SelectedIndexChanged(object sender, EventArgs e)
        {
            string publicationId = comboBoxPublications.SelectedItem.ToString();
            PublicationForm form = new PublicationForm(publicationId, this);
            form.Show();
        }
    }
}
