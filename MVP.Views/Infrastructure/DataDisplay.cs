using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WFViewListBooksJournals.Views.ModelView;

namespace WFViewListBooksJournals.Views.Infrastructure
{
    public class DataDisplay
    {
        private List<string> TextDataApplication { get; set; }
        private List<string> ErrorList { get; set; }

        public DataDisplay()
        {
            TextDataApplication = new List<string>(new string[] { "Attention", "Number Issue", "Name Journal", "Create Author" });
            ErrorList = new List<string>(new string[] { "Not all fields are filled.", "Not all fields are filled in correctly."});
        }

        public void Clear(ListBox listBox1)
        {
            listBox1.Items.Clear();
        }

        public void Clear(ComboBox comboBox)
        {
            comboBox.Items.Clear();
        }

        public void ShowTitle(ListBox listBox1, string title)
        {
            listBox1.Items.Add(title);
        }

        public void ShowEmptyLine(ListBox listBox1)
        {
            listBox1.Items.Add(Environment.NewLine);
        }

        public void ShowBook(ListBox listBox1, string author, string nameBook, string year, int pages)
        {
            listBox1.Items.Add(string.Format("\t{0} {1}, – {2} г. - {3} ст.", author, nameBook, year, pages));
        }

        public void ShowJournal(ListBox listBox1, string author, string articleTitle, string nameJournal, string year, string numberIssu, string locationArticle)
        {
            listBox1.Items.Add(string.Format("\t{0} {1}. {2}, - {3} г. - {4}. - C. {5}", author, articleTitle, nameJournal, year, numberIssu, locationArticle));
        }
        
        public void ShowNewspaper(ListBox listBox1, string author, string articleTitle, string nameNewspaper, string year, string numberIssu, string locationArticle)
        {
            listBox1.Items.Add(string.Format("\t{0} {1}. {2}, - {3} г. - {4}. - C. {5}", author, articleTitle, nameNewspaper, year, numberIssu, locationArticle));
        }

        public void ShowArticle(ListBox listBox1, IEnumerable<ArticleView> articles)
        {
            foreach (ArticleView article in articles)
            {
                listBox1.Items.Add(string.Format("\t{0} {1}. - C. {2}", article.Author, article.Title, article.Location));
            }
        }

        public void ShowText(ListBox listBox1, string message)
        {
            listBox1.Items.Add(message);
        }

        public void ChangeNameField(Label labelPages, Label labelNamePublication)
        {
            labelPages.Text = TextDataApplication[1];
            labelNamePublication.Text = TextDataApplication[2];
        }

        public string GetTitleErrorForm()
        {
            return TextDataApplication[0];
        }

        public string GetTitleFormAuthor()
        {
            return TextDataApplication[3];
        }

        public string Error(int error)
        {
            if (error == 0)
            {
                return ErrorList[0];
            }
            return ErrorList[1];
        }
    }
}