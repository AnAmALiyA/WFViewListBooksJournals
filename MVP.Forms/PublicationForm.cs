using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Servises;

namespace WFViewListBooksJournals.Forms
{
    public partial class PublicationForm : Form, IPublicationForm
    {
        private PresetnerPublicationForm _presetnerPublication;
        private IMainForm _mainForm;
        private DisplayOfData DisplayData { get; set; }
        private Dictionary<Enum, string> DictionaryPublication { get; set; }
        private enum EnumPublicationForm { NoSelectedIndex = -1 }
        private enum EnumDictionaryPublication {
            LabelPages,
            LabelNamePublications,
            Book,
            Journal,
            Newspaper
        }
        private int SelectedIndex { get; set; }

        public PublicationForm(string publication, IMainForm mainForm)
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _presetnerPublication = new PresetnerPublicationForm(this, publication, connectionString);
            DisplayData = new DisplayOfData();
            this.Text = publication;
            SelectedIndex = (int)EnumPublicationForm.NoSelectedIndex;

            DictionaryPublication = new Dictionary<Enum, string>();
                DictionaryPublication.Add(EnumDictionaryPublication.LabelPages, "Number Issue");
                DictionaryPublication.Add(EnumDictionaryPublication.LabelNamePublications, "Name Journal");
                DictionaryPublication.Add(EnumDictionaryPublication.Book, "Book");
                DictionaryPublication.Add(EnumDictionaryPublication.Journal, "Journal");
                DictionaryPublication.Add(EnumDictionaryPublication.Newspaper, "Newspaper");

            SelectPublicationForm(publication);
            InitializeComponentPublicationForm();
            _mainForm = mainForm;
    }
        public void InitializeComponentPublicationForm()
        {
            _presetnerPublication.InitializeComponentPublicationForm();
        }
        private void SelectPublicationForm(string publication)
        {
            if (DictionaryPublication[EnumDictionaryPublication.Book]== publication)
            {
                AdjustDisplayBooks();
                return;
            }

            if (DictionaryPublication[EnumDictionaryPublication.Journal] == publication)
            {
                AdjustDisplayJournals();
                return;
            }

            if (DictionaryPublication[EnumDictionaryPublication.Newspaper] == publication)
            {
                AdjustDisplayNewspapers();
                return;
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
            labelPages.Text = DictionaryPublication[EnumDictionaryPublication.LabelPages];
            labelNamePublications.Text = DictionaryPublication[EnumDictionaryPublication.LabelNamePublications];
        }

        private void AdjustDisplayNewspapers()
        {
            labelPages.Visible = false;
            textBoxPages.Visible = false;
        }

        public void ClearComboBoxAuthors()
        {
            DisplayData.ClearComboBox(comboBoxAuthor);
        }

        public void FillComboBoxAuthors(string[] arrayAuthor)
        {
            DisplayData.FillComboBox(comboBoxAuthor, arrayAuthor);
        }

        public void ClearListBoxMain()
        {
            DisplayData.ClearListBox(listBoxPublicationForm);
        }

        public void FillListBoxMain(string publication)
        {
            DisplayData.FillListBox(listBoxPublicationForm, publication);
        }

        public void FillListBoxMain(string author, string nameBook, string year, int pages)
        {
            DisplayData.FillListBox(listBoxPublicationForm, author, nameBook, year, pages);
        }

        public void FillListBoxMain(string author, string articleTitle, string publication, string year, string numberIssu, string locationArticle)
        {
            DisplayData.FillListBox(listBoxPublicationForm, author, articleTitle, publication, year, numberIssu, locationArticle);
        }

        public void FillListBoxMain(string author, string title, string location)
        {
            DisplayData.FillListBox(listBoxPublicationForm, author, title, location);
        }

        private void listBoxPublicationForm_SelectedIndexChanged(object sender, EventArgs e)
         {
            if (listBoxPublicationForm.SelectedIndex!= default(int))
            {
                SelectedIndex = listBoxPublicationForm.SelectedIndex;
                SelectedIndex--;
            }
        }

        private void listBoxPublicationForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxPublicationForm.SelectedIndex != default(int))
            {
                SelectedIndex = listBoxPublicationForm.SelectedIndex;
                SelectedIndex--;
                _presetnerPublication.FillFieldsForm(SelectedIndex);
            }
        }

        public void FillFields(int selectedAuthor, string name, DateTime date, int pages)
        {
            comboBoxAuthor.SelectedIndex = selectedAuthor;
            textBoxName.Text = name;
            dateTimePickerDate.Value = date;
            textBoxPages.Text = pages.ToString();
        }

        public void FillFields(int selectedAuthor, string title, string location, string name, DateTime date, string numberIssue)
        {
            comboBoxAuthor.SelectedIndex = selectedAuthor;
            textBoxTitle.Text = title;
            textBoxLocation.Text = location;
            textBoxName.Text = name;
            dateTimePickerDate.Value = date;
            textBoxPages.Text = numberIssue;
        }

        public void FillFields(int selectedAuthor, string title, string location, string name, DateTime date)
        {
            comboBoxAuthor.SelectedIndex = selectedAuthor;
            textBoxTitle.Text = title;
            textBoxLocation.Text = location;
            textBoxName.Text = name;
            dateTimePickerDate.Value = date;            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string tempItems = comboBoxAuthor.Items[comboBoxAuthor.SelectedIndex].ToString();
            bool valid = _presetnerPublication.Validation(tempItems, textBoxName.Text, textBoxPages.Text, dateTimePickerDate.Value, textBoxTitle.Text, textBoxLocation.Text);

            if (valid)
            {
                return;
            }

            _presetnerPublication.AddDataReposotiry(tempItems, textBoxName.Text, textBoxPages.Text, dateTimePickerDate.Value, textBoxTitle.Text, textBoxLocation.Text);
            
            _presetnerPublication.FillListBoxPublication();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string tempItems = comboBoxAuthor.Items[comboBoxAuthor.SelectedIndex].ToString();
            bool valid = _presetnerPublication.Validation(tempItems, textBoxName.Text, textBoxPages.Text, dateTimePickerDate.Value, textBoxTitle.Text, textBoxLocation.Text);

            if (valid)
            {
                return;
            }

            _presetnerPublication.UpdateReposotiry(tempItems, textBoxName.Text, textBoxPages.Text, dateTimePickerDate.Value, textBoxTitle.Text, textBoxLocation.Text);
            
            _presetnerPublication.FillListBoxPublication();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != (int)EnumPublicationForm.NoSelectedIndex)
            {
                _presetnerPublication.Delete(SelectedIndex);
                SelectedIndex = (int)EnumPublicationForm.NoSelectedIndex;
            }
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
