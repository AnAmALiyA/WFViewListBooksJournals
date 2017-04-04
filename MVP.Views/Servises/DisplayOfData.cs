using System;
using System.Windows.Forms;

namespace WFViewListBooksJournals.Views.Servises
{
    public class DisplayOfData
    {
        public void ClearListBox(ListBox listBox)
        {
            listBox.Items.Clear();
        }

        public void FillListBoxEmptyLine(ListBox listBox)
        {
            listBox.Items.Add(Environment.NewLine);
        }

        public void FillListBox(ListBox listBox, string publication)
        {
            listBox.Items.Add(publication);
        }

        public void FillListBox(ListBox listBox, string author, string nameBook, string year, int pages)
        {
            listBox.Items.Add(string.Format("\t{0} {1}, – {2} г. - {3} ст.", author, nameBook, year, pages));
        }

        public void FillListBox(ListBox listBox, string author, string articleTitle, string publication, string year, string numberIssu, string locationArticle)
        {
            listBox.Items.Add(string.Format("\t{0} {1}. {2}, - {3} г. - {4}. - C. {5}", author, articleTitle, publication, year, numberIssu, locationArticle));
        }

        public void FillListBox(ListBox listBox, string author, string title, string location)
        {
            listBox.Items.Add(string.Format("\t{0} {1}. - C. {2}", author, title, location));
        }

        public void ClearComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
        }

        public void FillComboBox(ComboBox comboBox, string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                comboBox.Items.Add(array[i]);
            }            
        }
    }
}
