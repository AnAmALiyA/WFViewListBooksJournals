using System;
using System.Windows.Forms;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Infrastructure;
using WFViewListBooksJournals.Views.Infrastructure.Mapping;
using WFViewListBooksJournals.Views.ModelView;
using WFViewListBooksJournals.Entities;


namespace WFViewListBooksJournals.Forms
{
    public partial class ChoicePublicationForm : Form, IChoicePublicationForm
    {
        private BussinesLogicPublications bussinesLogic;
        public dynamic SelectedValue { get; set; }
        public int SelectedValueDelete { get; set; }
        public int SelectedIndexListBox { get; set; }
        private enum EnumIndexDelete { NotDelete = -1 }
        
        public ComboBox ComboBoxAuthor { get { return comboBoxAuthor; } set { comboBoxAuthor = value; } }
        public ListBox ListBox1Form2 { get { return listBox1Form2; } set { listBox1Form2 = value; } }

        public DataDisplay DataDisplay { get; set; }
        public MappingEntity Mapping { get; set; }

        public ChoicePublicationForm(string publication)
        {
            InitializeComponent();
            Text = publication;
            
            DataDisplay = new DataDisplay();
            Mapping = new MappingEntity();
            
            //bussinesLogic = new BussinesLogicPublications(this);
            bussinesLogic = BussinesLogicPublications.Instance;
            bussinesLogic.ChoicePublicationForm = this;

            bussinesLogic.GetDataChoicePublicationForm();
        }

        public void AdjustDisplayBooks()
        {
            labelTitle.Visible = false;
            textBoxTitle.Visible = false;

            labelLocation.Visible = false;
            textBoxLocation.Visible = false;
        }

        public void AdjustDisplayJournals()
        {
            DataDisplay.ChangeNameField(labelPages, labelNamePublications);
        }

        public void AdjustDisplayNewspapers()
        {
            labelPages.Visible = false;
            textBoxPages.Visible = false;
        }

        public void FillDataMainListBox()
        {
            switch (Text)
            {
                case "Books":
                    bussinesLogic.ShowListBooks(ListBox1Form2);                    
                    break;
                case "Journals":
                    bussinesLogic.ShowListJournals(ListBox1Form2);
                    break;
                case "Newspapers":
                    bussinesLogic.ShowListNewspapers(ListBox1Form2);
                    break;
                default:
                    break;
            }
        }

        public void ShowForm()
        {
            this.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidatioButtonAdd())
            {
                if (bussinesLogic.AddDataReposotiry(Text, ComboBoxAuthor.SelectedIndex, textBoxName.Text, textBoxPages.Text, dateTimePicker1, textBoxTitle.Text, textBoxLocation.Text))
                {
                    ClearFields();
                    DataDisplay.Clear(listBox1Form2);
                    SelectedValue = null;
                    FillDataMainListBox();
                }
            }
        }

        private bool ValidatioButtonAdd()
        {
            if (bussinesLogic.CompareInsertValueFields(Text, SelectedValue, ComboBoxAuthor.Text, textBoxName.Text, textBoxPages.Text, dateTimePicker1.Text, textBoxTitle.Text, textBoxLocation.Text))
            {
                if (bussinesLogic.ValidationData(Text, ComboBoxAuthor.Text, textBoxName.Text, textBoxPages.Text, textBoxTitle.Text, textBoxLocation.Text))
                {
                    return true;
                }
            }
            return false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //if (bussinesLogic.CompareInsertValueFields(Text, SelectedValue, ComboBoxAuthor.Text, textBoxName.Text, textBoxPages.Text, dateTimePicker1.Text, textBoxTitle.Text, textBoxLocation.Text))
            //{
            //    if (bussinesLogic.ValidationData(Text, ComboBoxAuthor.Text, textBoxName.Text, textBoxPages.Text, textBoxTitle.Text, textBoxLocation.Text))
            
            if (ValidatioButtonAdd())
            {
                bussinesLogic.UpdateReposotiry(Text, SelectedIndexListBox, ComboBoxAuthor.SelectedIndex, textBoxName.Text, textBoxPages.Text, dateTimePicker1, textBoxTitle.Text, textBoxLocation.Text);
                ClearFields();
                DataDisplay.Clear(listBox1Form2);
                SelectedValue = null;
                FillDataMainListBox();
            }
            //}
        }

        private void listBox1Form2_DoubleClick(object sender, EventArgs e)
        {
            SelectedIndexListBox = listBox1Form2.SelectedIndex;
            if (listBox1Form2.SelectedIndex != 0)
            {
                bussinesLogic.InsertSelectedValue(Text, SelectedIndexListBox, SelectedValue);//может понадобиться ref
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1Form2.SelectedIndex != 0 & SelectedValueDelete != (int)EnumIndexDelete.NotDelete)
            {
                bussinesLogic.SelectedValueDelete(Text, SelectedValueDelete);
                bussinesLogic.ClearListBox(listBox1Form2);
                FillDataMainListBox();
                SelectedValueDelete = (int)EnumIndexDelete.NotDelete;
            }
        }

        private void listBox1Form2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1Form2.SelectedIndex != 0)
            {
                SelectedValueDelete = listBox1Form2.SelectedIndex;
            }
        }

        private void buttonNewAuthor_Click(object sender, EventArgs e)
        {
            //bussinesLogic.ShowFormAddAuthor();

            AddAuthorForm newForm = new AddAuthorForm();
            //HaveForm.DataDisplay.GetTitleFormAuthor(newForm);
            newForm.Show();
        }

        public void ClearFields()
        {
            comboBoxAuthor.Text = string.Empty;
            textBoxName.Text = string.Empty;
            textBoxPages.Text = string.Empty;
            dateTimePicker1.Text = DateTime.Now.ToString();
            textBoxTitle.Text = string.Empty;
            textBoxLocation.Text = string.Empty;
        }
        
        public void InsertFields(Book book)
        {
            BookView bookView = Mapping.GetBook(book);
            comboBoxAuthor.Text = bookView.Author;
            textBoxName.Text = bookView.Name;
            dateTimePicker1.Text = bookView.Date.ToString();
            textBoxPages.Text = bookView.Pages.ToString();
        }

        public void InsertFields(Journal journal)
        {
            JournalView journalView = Mapping.GetJournal(journal);
            comboBoxAuthor.Text = journalView.Articles[0].Author;
            textBoxName.Text = journalView.Name;
            dateTimePicker1.Text = journalView.Date.ToString();
            textBoxPages.Text = journalView.NumberIssue;
            textBoxTitle.Text = journalView.Articles[0].Title;
            textBoxLocation.Text = journalView.Articles[0].Location;
        }

        public void InsertFields(Newspaper newspaper)
        {
            NewspaperView newspaperView = Mapping.GetNewspaper(newspaper);
            comboBoxAuthor.Text = newspaperView.Articles[0].Author;
            textBoxName.Text = newspaperView.Name;
            dateTimePicker1.Text = newspaperView.Date.ToString();
            textBoxTitle.Text = newspaperView.Articles[0].Title;
            textBoxLocation.Text = newspaperView.Articles[0].Location;
        }

        public void ClearComboBoxAuthor()
        {
            DataDisplay.Clear(comboBoxAuthor);
        }
    }
}
