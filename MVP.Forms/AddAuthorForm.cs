using System.Windows.Forms;
using WFViewListBooksJournals.Presenters.Infrastructure;
using WFViewListBooksJournals.Views.Interfaces;
using WFViewListBooksJournals.Views.Infrastructure;

namespace WFViewListBooksJournals.Forms
{
    public partial class AddAuthorForm : Form, IAddAuthorForm
    {
        private BussinesLogicPublications bussinesLogic;
        private string[] Nationality { get; set; }
        //private DataDisplay DataDisplay { get; set; }

        //public AddAuthorForm(BussinesLogicPublications bussinesLogic)
        public AddAuthorForm()
        {
            InitializeComponent();
            Nationality = new string[] { "Ru", "En" };

            //bussinesLogic = new BussinesLogicPublications(this);
            bussinesLogic = BussinesLogicPublications.Instance;
            bussinesLogic.AddAuthorForm = this;

            //DataDisplay = new DataDisplay();
            Text = new DataDisplay().GetTitleFormAuthor();

            PresetItems();
        }

        private void PresetItems()
        {
            this.comboBox1.Text = Nationality[0];
            this.comboBox1.Items.Add(Nationality[0]);
            this.comboBox1.Items.Add(Nationality[1]);
        }

        private bool GetNationality(string nationality)
        {
            if (nationality == Nationality[0])
            {
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (bussinesLogic.ValidationDataAuthor(textBoxFirst.Text, textBoxSecond.Text, textBoxLast.Text, textBoxAge.Text))
            {
                if (bussinesLogic.FindAutor(textBoxFirst.Text, textBoxSecond.Text, textBoxLast.Text, textBoxAge.Text) == null)
                {
                    bussinesLogic.AddAuthor(textBoxFirst.Text, textBoxSecond.Text, textBoxLast.Text, GetNationality(comboBox1.Items.ToString()), textBoxAge.Text);
                }
                this.Close();
                bussinesLogic.UpdateDropDownListAuthor();
            }
        }
    }
}
