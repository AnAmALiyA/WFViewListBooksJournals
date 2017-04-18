using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Views.Servises
{
    public class DisplayOfData
    {
        private static DisplayOfData _instance;

        private DisplayOfData() { }

        public static DisplayOfData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DisplayOfData();
                }
                return _instance;
            }
        }

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

        public void FillListBox(ListBox listBox, Book book, Dictionary<int, Book> bookList)
        {
            string authors = GetStringAuthorList(book.Authors);
            string year = book.Date.Year.ToString();
            listBox.Items.Add(string.Format("\t{0} {1}, – {2} г. - {3} ст.", authors, book.Name, year, book.Pages));

            int index = listBox.Items.Count;
            index--;
            bookList.Add(index, book);
        }

        public void FillListBox(ListBox listBox, Journal journal, Dictionary<int, Journal> journalList)
        {
            foreach (Article article in journal.Articles)
            {
                string authors = GetStringAuthorList(article.Authors);
                string year = journal.Date.Year.ToString();
                listBox.Items.Add(string.Format("\t{0} {1}. {2}, - {3} г. - {4}. - C. {5}", authors, article.Title, journal.Name, year, journal.NumberIssue, article.Location));

                int index = listBox.Items.Count;
                index--;
                var tempJournal = new Journal() { Articles = new List<Article>() { article }, Date = journal.Date, Name = journal.Name, NumberIssue = journal.NumberIssue };
                journalList.Add(index, tempJournal);
            }            
        }

        public void FillListBox(ListBox listBox, Newspaper newspaper, Dictionary<int, Newspaper> newspaperList)
        {
            foreach (Article article in newspaper.Articles)
            {
                string authors = GetStringAuthorList(article.Authors);
                string year = newspaper.Date.Year.ToString();
                string number = newspaper.Date.ToString("d.MMMM");
                listBox.Items.Add(string.Format("\t{0} {1}. {2}, - {3} г. - {4}. - C. {5}", authors, article.Title, newspaper.Name, year, number, article.Location));

                int index = listBox.Items.Count;
                index--;
                var tempNewspaper = new Newspaper() { Articles = new List<Article>() { article }, Date = newspaper.Date, Name = newspaper.Name};
                newspaperList.Add(index, tempNewspaper);
            }
        }

        public void FillListBox(ListBox listBox, Article article)
        {
            string authors = GetStringAuthorList(article.Authors);
            listBox.Items.Add(string.Format("\t{0} {1}. - C. {2}", authors, article.Title, article.Location));

        }

        public void ClearComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
        }

        public void FillComboBox(ComboBox comboBox, IEnumerable<string> listItemString)
        {
            foreach (var itemString in listItemString)
            {
                comboBox.Items.Add(itemString);
            }
        }

        public string GetStringAuthor(Author author)
        {
            string tempAuthor = string.Empty;
            tempAuthor += author.InitialsOption ? author.FirstName + author.LastName + " " + author.SecondName : author.SecondName + " " + author.FirstName + author.LastName;
            tempAuthor += author.Age == default(int) ? string.Empty : " " + author.Age.ToString();
            return tempAuthor;
        }

        public string GetStringAuthorList(ICollection<Author> listAuthors)
        {
            string authors = string.Empty;
            foreach (Author author in listAuthors)
            {
                string stringAuthor = GetStringAuthor(author);
                authors += stringAuthor;
            }
            return authors;
        }
    }
}
