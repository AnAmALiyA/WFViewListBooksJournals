using System;
using System.Windows.Forms;
using System.Configuration;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Infrastructure;
using WFViewListBooksJournals.Views.Infrastructure.Mapping;

namespace WFViewListBooksJournals.Forms
{
    public partial class MainForm : Form, IMainForm
    {
        private BussinesLogicPublications bussinesLogic;
        public DataDisplay DataDisplay { get; set; }
        public MappingEntity Mapping { get; set; }        

        public MainForm()
        {
            InitializeComponent();            
            DataDisplay = new DataDisplay();
            Mapping = new MappingEntity();

            //bussinesLogic = new BussinesLogicPublications(this);
            bussinesLogic = BussinesLogicPublications.Instance;
            bussinesLogic.MainForm = this;

            ShowAll_Click(null, null);
            bussinesLogic.CreateDropDownListAuthor(comboBox1, listBox1);
            bussinesLogic.CreateDropDownListPublications(comboBox2);
        }

        private void SaveNewspapers_Click(object sender, EventArgs e)
        {
            bussinesLogic.SaveNewspapers();
        }

        private void ShowAll_Click(object sender, EventArgs e)
        {
            bussinesLogic.ShowListLiterary(listBox1);
        }

        private void ShowAllArticles_Click(object sender, EventArgs e)
        {
            bussinesLogic.ShowAllArticles(listBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string author = comboBox1.SelectedItem.ToString();
            bussinesLogic.ShowEdition(author, listBox1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string publication = comboBox2.SelectedItem.ToString();
            ShowChoicePublication(publication);
        }

        private void ShowChoicePublication(string publication)
        {
            ChoicePublicationForm newForm = new ChoicePublicationForm(publication);

            ////создать нужную форму
            //if (publication == ListPublications[0])
            //{
            //    AdjustDisplayBooks(NewForm2);
            //}

            //if (publication == ListPublications[1])
            //{
            //    AdjustDisplayJournals(NewForm2);
            //}

            //if (publication == ListPublications[2])
            //{
            //    AdjustDisplayNewspapers(NewForm2);
            //}
            //NewForm2.FillDataMainListBox(publication);

            //CreateDropDownListAuthor(NewForm2.comboBoxAuthor, NewForm2.listBox1Form2);
            //ListAuthorsForm2 = GetListListsAuthorForm2();

            newForm.ShowForm();
        }

        private void buttonSaveJournals_Click(object sender, EventArgs e)
        {
            bussinesLogic.SaveJournals();
        }

        private void buttonSaveBooks_Click(object sender, EventArgs e)
        {
            bussinesLogic.SaveBooks(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public void CreateErrorForm(int error)
        {
            ErrorForm errorForm = new ErrorForm();
            errorForm.Text = DataDisplay.GetTitleErrorForm();
            errorForm.label1.Text = DataDisplay.Error(error);
            errorForm.Show();
        }
    }
}
