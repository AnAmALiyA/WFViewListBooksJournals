using System;
using System.Windows.Forms;

namespace WFViewListBooksJournals.Forms
{
    public partial class ErrorForm : Form
    {
        private ChoicePublicationForm Form { get; set; }

        public ErrorForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
