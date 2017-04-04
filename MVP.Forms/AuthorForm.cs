using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Servises;

namespace WFViewListBooksJournals.Forms
{
    public partial class AuthorForm : Form, IAuthorForm
    {
        private PresetnerAuthorForm _authorForm;
        private DisplayOfData DisplayData { get; set; }
        private IPublicationForm _publicationForm;

        public AuthorForm(IPublicationForm publicationForm)
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _authorForm = new PresetnerAuthorForm(this, connectionString);
            DisplayData = new DisplayOfData();
            _publicationForm = publicationForm;

            InitializeComponentAuthorForm();
        }
        public void InitializeComponentAuthorForm()
        {
            _authorForm.InitializeComponentAuthorForm();
        }

        public void InitializeComponentAuthorForm(Dictionary<string, bool> nationality)
        {
            string[] list = nationality.Select(s => s.Key).ToArray();
            DisplayData.FillComboBox(comboBoxNationality, list);
        }
        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            bool success = _authorForm.Save(textBoxFirstName.Text, textBoxSecondName.Text, textBoxLastName.Text, textBoxAge.Text, comboBoxNationality.Text);
            if (success)
            {
                _publicationForm.InitializeComponentPublicationForm();
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
