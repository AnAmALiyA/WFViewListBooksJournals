using System;
using System.Collections.Generic;
using System.Linq;
using WFViewListBooksJournals.Views.Interfaces;
using System.Windows.Forms;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Repositories;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class BussinesLogicPublications
    {
        private static BussinesLogicPublications instance;

        private UnitOfWork UnitOfWork { get; set; }       
        private List<string> ListPublications { get; set; }
        private Validation Validation { get; set; }
        public enum EnumArticlesCount { One = 1 }
        private static List<List<Author>> ListAuthorsForm2 { get; set; }

        private IMainForm _mainForm;
        private IChoicePublicationForm _choicePublicationForm;
        private IAddAuthorForm _authorForm;

        private BussinesLogicPublications()
        {
            UnitOfWork = new UnitOfWork();            
            ListPublications = new List<string>(new string[] { "Books", "Journals", "Newspapers" });
            Validation = new Validation();
        }

        public static BussinesLogicPublications Instance
        {
            get
            {
                if (instance == null)
                { instance = new BussinesLogicPublications(); }
                return instance;
            }
        }

        public IMainForm MainForm
        {
            get { return _mainForm; }
            set { _mainForm = value; }     
        }

        public IAddAuthorForm AddAuthorForm
        {
            get { return _authorForm; }
            set { _authorForm = value; }
        }

        public IChoicePublicationForm ChoicePublicationForm
        {
            get { return _choicePublicationForm; }
            set { _choicePublicationForm = value; }
        }

        public void ShowListLiterary(ListBox listBox1)
        {
            MainForm.DataDisplay.Clear(listBox1);
            ShowListBooks(listBox1);
            MainForm.DataDisplay.ShowEmptyLine(listBox1);
            ShowListJournals(listBox1);
            MainForm.DataDisplay.ShowEmptyLine(listBox1);
            ShowListNewspapers(listBox1);
        }

        public void ShowListBooks(ListBox listBox1)
        {
            MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[0]);
            foreach (Book book in UnitOfWork.Books.ListEntities)
            {
                MainForm.DataDisplay.ShowBook(listBox1, book.Authors.GetStringAuthors(), book.Name, book.Date.Year.ToString(), book.Pages);
            }
        }

        public void ShowListJournals(ListBox listBox1)
        {
            MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[1]);
            foreach (Journal journal in UnitOfWork.Journals.ListEntities)
            {
                foreach (Article article in journal.Articles)
                {
                    MainForm.DataDisplay.ShowJournal(listBox1, article.Author.GetStringAuthors(), article.Title, journal.Name, journal.Date.Year.ToString(), journal.NumberIssue, article.Location);
                }
            }
        }
        
        public void ShowListNewspapers(ListBox listBox1)
        {
            MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[2]);
            foreach (Newspaper newspaper in UnitOfWork.Newspapers.ListEntities)
            {
                foreach (Article article in newspaper.Articles)
                {
                    MainForm.DataDisplay.ShowNewspaper(listBox1, article.Author.GetStringAuthors(), article.Title, newspaper.Name, newspaper.Date.Year.ToString(), newspaper.Date.ToString("d.MMMM"), article.Location);
                }
            }
        }

        public void SaveBooks(string connectionString)
        {
            UnitOfWork.Books.SaveDB(connectionString);
        }

        public void SaveNewspapers()
        {
            UnitOfWork.Newspapers.SaveXML();
        }
        
        public void SaveJournals()
        {
            UnitOfWork.Journals.SaveTXT();
        }

        public void ShowAllArticles(ListBox listBox1)
        {
            MainForm.DataDisplay.Clear(listBox1);

            try
            {
                var queryJournal = from journal in UnitOfWork.Journals.ListEntities
                                   let articles = journal.Articles
                                   from article in articles
                                   select article;

                MainForm.DataDisplay.ShowArticle(listBox1, MainForm.Mapping.GetListArticle(queryJournal));

                var queryNewspapers = from newspapers in UnitOfWork.Newspapers.ListEntities
                                      let articles = newspapers.Articles
                                      from article in articles
                                      select article;

                MainForm.DataDisplay.ShowArticle(listBox1, MainForm.Mapping.GetListArticle(queryNewspapers));
            }
            catch (Exception ex)
            {
                MainForm.DataDisplay.ShowEmptyLine(listBox1);
                MainForm.DataDisplay.ShowText(listBox1, ex.Message);
            }
        }

        public void CreateDropDownListAuthor(ComboBox comboBox1, ListBox listBox1)
        {
            List<string> listAuthor = GetListAuthor(listBox1);

            foreach (string author in listAuthor)
            {
                comboBox1.Items.Add(author);
            }
        }

        private List<string> GetListAuthor(ListBox listBox1)
        {
            List<string> listAuthor = new List<string>();

            try
            {
                foreach (Book book in UnitOfWork.Books.ListEntities)
                {
                    AddListAuthor(ref listAuthor, book.Authors.GetStringAuthors());
                }

                foreach (Journal journal in UnitOfWork.Journals.ListEntities)
                {
                    foreach (Article article in journal.Articles)
                    {
                        AddListAuthor(ref listAuthor, article.Author.GetStringAuthors());
                    }
                }

                foreach (Newspaper newspaper in UnitOfWork.Newspapers.ListEntities)
                {
                    foreach (Article article in newspaper.Articles)
                    {
                        AddListAuthor(ref listAuthor, article.Author.GetStringAuthors());
                    }
                }
                foreach (Author author in UnitOfWork.AllLiterary.NewListAuthor)
                {
                    AddListAuthor(ref listAuthor, author.GetStringAuthor());
                }
            }
            catch (Exception ex)
            {
                MainForm.DataDisplay.ShowEmptyLine(listBox1);
                MainForm.DataDisplay.ShowText(listBox1, ex.Message);
            }
            return listAuthor;
        }

        private void AddListAuthor(ref List<string> listAuthor, string author)
        {
            if (listAuthor.IndexOf(author) == -1)
            {
                listAuthor.Add(author);
            }
        }

        public void CreateDropDownListPublications(ComboBox comboBox2)
        {
            foreach (string publication in ListPublications)
            {
                comboBox2.Items.Add(publication);
            }
        }

        public void ShowEdition(string author, ListBox listBox1)
        {
            try
            {
                MainForm.DataDisplay.Clear(listBox1);

                List<Book> books = UnitOfWork.Books.ListEntities;
                List<Book> findBooks = books.FindAll(x => x.Authors.GetStringAuthors() == author);
                if (findBooks.Count != 0)
                {
                    MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[0]);
                    foreach (Book book in findBooks)
                    {
                        MainForm.DataDisplay.ShowBook(listBox1, book.Authors.GetStringAuthors(), book.Name, book.Date.Year.ToString(), book.Pages);
                    }
                    MainForm.DataDisplay.ShowEmptyLine(listBox1);
                }

                List<Journal> journals = UnitOfWork.Journals.ListEntities;
                List<Journal> findJournals = FindJournals(journals, author);

                if (findJournals.Count != 0)
                {
                    MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[1]);
                    foreach (Journal journal in findJournals)
                    {
                        foreach (Article articl in journal.Articles)
                        {
                            MainForm.DataDisplay.ShowJournal(listBox1, articl.Author.GetStringAuthors(), articl.Title, journal.Name, journal.Date.Year.ToString(), journal.NumberIssue, articl.Location);
                        }
                    }
                    MainForm.DataDisplay.ShowEmptyLine(listBox1);
                }

                List<Newspaper> newspapers = UnitOfWork.Newspapers.ListEntities;
                List<Newspaper> findNewspapers = FindNewspapers(newspapers, author);

                if (findNewspapers.Count != 0)
                {
                    MainForm.DataDisplay.ShowTitle(listBox1, ListPublications[2]);
                    foreach (Newspaper newspaper in findNewspapers)
                    {
                        foreach (Article articl in newspaper.Articles)
                        {
                            MainForm.DataDisplay.ShowNewspaper(listBox1, articl.Author.GetStringAuthors(), articl.Title, newspaper.Name, newspaper.Date.Year.ToString(), newspaper.Date.ToString("d.MMMM"), articl.Location);
                        }
                    }
                    MainForm.DataDisplay.ShowEmptyLine(listBox1);
                }
            }
            catch (Exception ex)
            {
                MainForm.DataDisplay.ShowEmptyLine(listBox1);
                MainForm.DataDisplay.ShowText(listBox1, ex.Message);
            }
        }

        private List<Journal> FindJournals(List<Journal> journals, string author)
        {
            List<Journal> listJournals = new List<Journal>();

            foreach (var journal in journals)
            {
                foreach (Article article in journal.Articles)
                {
                    if (article.Author.GetStringAuthors() == author)
                    {
                        List<Article> tempArticle = new List<Article>();
                        tempArticle.Add(article);
                        listJournals.Add(new Journal { Articles = tempArticle, Name = journal.Name, Date = journal.Date, NumberIssue = journal.NumberIssue });
                    }
                }
            }
            return listJournals;
        }

        private List<Newspaper> FindNewspapers(List<Newspaper> newspapers, string author)
        {
            List<Newspaper> listNewspapers = new List<Newspaper>();

            foreach (var newspaper in newspapers)
            {
                foreach (Article article in newspaper.Articles)
                {
                    if (article.Author.GetStringAuthors() == author)
                    {
                        List<Article> tempArticle = new List<Article>();
                        tempArticle.Add(article);
                        listNewspapers.Add(new Newspaper { Articles = tempArticle, Name = newspaper.Name, Date = newspaper.Date });
                    }
                }
            }
            return listNewspapers;
        }
        
        public void GetDataChoicePublicationForm()
        {
            if (_choicePublicationForm.Text == ListPublications[0])
            {
                _choicePublicationForm.AdjustDisplayBooks();
            }

            if (_choicePublicationForm.Text == ListPublications[1])
            {
                _choicePublicationForm.AdjustDisplayJournals();
            }

            if (_choicePublicationForm.Text == ListPublications[2])
            {
                _choicePublicationForm.AdjustDisplayNewspapers();
            }
            _choicePublicationForm.FillDataMainListBox();
            CreateDropDownListAuthor(_choicePublicationForm.ComboBoxAuthor, _choicePublicationForm.ListBox1Form2);
            ListAuthorsForm2 = GetListListsAuthorForm2();
        }

        private List<List<Author>> GetListListsAuthorForm2()
        {
            List<List<Author>> tempList = new List<List<Author>>();
            foreach (Book book in UnitOfWork.Books.ListEntities)
            {
                tempList.Add(book.Authors);
            }

            foreach (Journal journal in UnitOfWork.Journals.ListEntities)
            {
                foreach (Article article in journal.Articles)
                {
                    tempList.Add(article.Author);
                }
            }

            foreach (Newspaper newspaper in UnitOfWork.Newspapers.ListEntities)
            {
                foreach (Article article in newspaper.Articles)
                {
                    tempList.Add(article.Author);
                }
            }

            foreach (Author author in UnitOfWork.AllLiterary.NewListAuthor)
            {
                tempList.Add(new List<Author>() { author });
            }
            return tempList;
        }
        
        public void UpdateDropDownListAuthor()
        {
            _choicePublicationForm.ClearComboBoxAuthor();
            CreateDropDownListAuthor(_choicePublicationForm.ComboBoxAuthor, _choicePublicationForm.ListBox1Form2);
            ListAuthorsForm2 = GetListListsAuthorForm2();
        }

        public bool ValidationData(string publication, string author, string name, string pages, string title, string location)
        {
            if (Validation.CheckEmptyField(ListPublications, publication, author, name, pages, title, location))
            {
                ShowWindowError(EnumErrors.EmptyField);
                return false;
            }

            if (Validation.CheckCorrectnessData(ListPublications, publication, author, name, pages, title, location))
            {
                ShowWindowError(EnumErrors.NotCorrectnessData);
                return false;
            }
            return true;
        }

        public bool ValidationDataAuthor(string firstName, string secondName, string lastName, string age)
        {
            if(Validation.NullField(new string[]{ firstName, secondName, lastName}))
            {
                ShowWindowError(EnumErrors.EmptyField);
                return false;
            }

            if (Validation.CheckCorrectnessDataAuthor(firstName, secondName, lastName, age))
            {
                ShowWindowError(EnumErrors.NotCorrectnessData);
                return false;
            }
            return true;
        }

        private void ShowWindowError(EnumErrors error)
        {

            if (error == EnumErrors.EmptyField)
            {
                _mainForm.CreateErrorForm((int)EnumErrors.EmptyField);
            }

            if (error == EnumErrors.NotCorrectnessData)
            {
                _mainForm.CreateErrorForm((int)EnumErrors.NotCorrectnessData);
            }
        }

        public bool AddDataReposotiry(string publication, int author, string namePublication, string pages, DateTimePicker dateTimePicker1, string title, string location)
        {
            if (ListPublications[0] == publication)
            {
                return AddBookReposotiry(author, namePublication, pages, dateTimePicker1);
            }

            if (ListPublications[1] == publication)
            {
                return AddJournalReposotiry(author, namePublication, pages, dateTimePicker1, title, location);
            }

            if (ListPublications[2] == publication)
            {
                return AddNewspaperReposotiry(author, namePublication, dateTimePicker1, title, location);
            }
            return false;
        }

        private bool AddBookReposotiry(int author, string namePublication, string pages, DateTimePicker dateTimePicker1)
        {
            foreach (Book book in UnitOfWork.Books.ListEntities)
            {
                if (book.Authors.GetStringAuthors() == ListAuthorsForm2[author].GetStringAuthors() && book.Name == namePublication && book.Pages == Int32.Parse(pages) && book.Date == DateTime.Parse(dateTimePicker1.Text))
                {
                    return false;
                }
            }
            UnitOfWork.Books.ListEntities.Add(new Book { Authors = ListAuthorsForm2[author], Name = namePublication, Date = DateTime.Parse(dateTimePicker1.Text), Pages = Int32.Parse(pages) });
            return true;
        }

        private bool AddJournalReposotiry(int author, string namePublication, string pages, DateTimePicker dateTimePicker1, string title, string location)
        {
            List<Journal> tempList = UnitOfWork.Journals.ListEntities;

            foreach (Journal journal in tempList)
            {
                if (journal.Name == namePublication && journal.NumberIssue == pages && journal.Date == DateTime.Parse(dateTimePicker1.Text))
                {
                    if (AddJournalArticleReposotiry(journal, tempList, author, title, location))
                    {
                        return true;
                    }
                    return false;
                }
            }
            Article tempArticle = new Article() { Author = ListAuthorsForm2[author], Title = title, Location = location };
            tempList.Add(new Journal() { Articles = new List<Article>() { tempArticle }, Name = namePublication, Date = DateTime.Parse(dateTimePicker1.Text), NumberIssue = pages });
            UnitOfWork.Journals.ListEntities = tempList;
            return true;
        }

        private bool AddJournalArticleReposotiry(Journal journal, List<Journal> tempList, int author, string title, string location)
        {
            foreach (Article article in journal.Articles)
            {
                if (article.Author.GetStringAuthors() == ListAuthorsForm2[author].GetStringAuthors() && article.Title == title && article.Location == location)
                {
                    return false;
                }
            }
            journal.Articles.Add(new Article() { Author = ListAuthorsForm2[author], Title = title, Location = location });
            UnitOfWork.Journals.ListEntities = tempList;
            return true;
        }

        private bool AddNewspaperReposotiry(int author, string namePublication, DateTimePicker dateTimePicker1, string title, string location)
        {
            List<Newspaper> tempList = UnitOfWork.Newspapers.ListEntities;

            foreach (Newspaper newspaper in tempList)
            {
                if (newspaper.Name == namePublication && newspaper.Date == DateTime.Parse(dateTimePicker1.Text))
                {
                    if (AddNewspaperArticleReposotiry(newspaper, tempList, author, title, location))
                    {
                        return true;
                    }
                    return false;
                }
            }
            Article tempArticle = new Article() { Author = ListAuthorsForm2[author], Title = title, Location = location };
            tempList.Add(new Newspaper() { Articles = new List<Article>() { tempArticle }, Name = namePublication, Date = DateTime.Parse(dateTimePicker1.Text)});
            UnitOfWork.Newspapers.ListEntities = tempList;
            return true;
        }

        private bool AddNewspaperArticleReposotiry(Newspaper newspaper, List<Newspaper> tempList, int author, string title, string location)
        {
            foreach (Article article in newspaper.Articles)
            {
                if (article.Author.GetStringAuthors() == ListAuthorsForm2[author].GetStringAuthors() && article.Title == title && article.Location == location)
                {
                    return false;
                }
            }
            newspaper.Articles.Add(new Article() { Author = ListAuthorsForm2[author], Title = title, Location = location });
            UnitOfWork.Newspapers.ListEntities = tempList;
            return true;
        }

        public Author FindAutor(string firstName, string secondName, string lastName, string age)
        {
            Author author = UnitOfWork.AllLiterary.ListAuthor.Find(x => x.FirstName == firstName & x.SecondName == secondName & x.LastName == lastName & x.Age == Int32.Parse(age));
            return author;
        }

        public void AddAuthor(string firstName, string secondName, string lastName, bool nationality, string age)
        {
            UnitOfWork.AllLiterary.NewListAuthor.Add(new Author() { FirstName = firstName, SecondName = secondName, LastName = lastName, InitialsOption = nationality, Age = Int32.Parse(age) });
        }

        public bool UpdateReposotiry(string publication, int selectedIndex, int author, string namePublication, string pages, DateTimePicker dateTimePicker1, string title, string location)
        {
            int count = 0;
            if (publication == ListPublications[0])
            {
                Book book = UnitOfWork.Books.ListEntities[selectedIndex];
                book.Authors = ListAuthorsForm2[author];
                book.Name = namePublication;
                book.Pages = Int32.Parse(pages);
                book.Date = DateTime.Parse(dateTimePicker1.Text);
                UnitOfWork.Books.ListEntities[selectedIndex] = book;
                return true;
            }

            if (publication == ListPublications[1])
            {
                var tempList = UnitOfWork.Journals.ListEntities;

                for (int journal = 0; journal < tempList.Count; journal++)
                {
                    for (int i = 0; i < tempList[journal].Articles.Count; i++)
                    {
                        count++;
                        if (selectedIndex == count)
                        {
                            tempList[journal].Articles[i].Author = ListAuthorsForm2[author];
                            tempList[journal].Articles[i].Title = title;
                            tempList[journal].Articles[i].Location = location;
                            tempList[journal].Name = namePublication;
                            tempList[journal].Date = DateTime.Parse(dateTimePicker1.Text);
                            tempList[journal].NumberIssue = pages;
                            return true;
                        }
                    }
                }
            }
            if (publication == ListPublications[2])
            {
                var tempList = UnitOfWork.Newspapers.ListEntities;

                for (int newspaper = 0; newspaper < tempList.Count; newspaper++)
                {
                    for (int i = 0; i < tempList[newspaper].Articles.Count; i++)
                    {
                        count++;
                        if (selectedIndex == count)
                        {
                            tempList[newspaper].Articles[i].Author = ListAuthorsForm2[author];
                            tempList[newspaper].Articles[i].Title = title;
                            tempList[newspaper].Articles[i].Location = location;
                            tempList[newspaper].Name = namePublication;
                            tempList[newspaper].Date = DateTime.Parse(dateTimePicker1.Text);
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        public void ClearListBox(ListBox listBox1Form2)
        {
            MainForm.DataDisplay.Clear(listBox1Form2);
        }

        public bool InsertSelectedValue(string publication, int selectedIndexListBox, dynamic selectedValue)
        {
            if (publication == ListPublications[0])
            {
                var tempList = UnitOfWork.Books.ListEntities;
                Book tempBook = tempList[--selectedIndexListBox];
                _choicePublicationForm.InsertFields(tempBook);

                selectedValue = tempBook; 
            }

            if (publication == ListPublications[1])
            {
                var tempList = UnitOfWork.Journals.ListEntities;

                int count = 0;
                for (int journal = 0; journal < tempList.Count; journal++)
                {
                    if(InsertSelectedValueJournal(journal, tempList, ref count, selectedIndexListBox, selectedValue/*, _choicePublicationForm*/))
                    {
                        return true;
                    }
                }
            }

            if (publication == ListPublications[2])
            {
                var tempList = UnitOfWork.Newspapers.ListEntities;

                int count = 0;
                for (int newspaper = 0; newspaper < tempList.Count; newspaper++)
                {
                    if (InsertSelectedValueNewspaper(newspaper, tempList, ref count, selectedIndexListBox, selectedValue/*, _choicePublicationForm*/))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool InsertSelectedValueJournal(int journal, List<Journal> tempList, ref int count, int selectedIndex, dynamic selectedValue/*, ChoicePublicationForm form*/)
        {
            for (int i = 0; i < tempList[journal].Articles.Count; i++)
            {
                count++;
                if (selectedIndex == count)
                {
                    Journal tempJournal = new Journal() { Articles = new List<Article>() { tempList[journal].Articles[i] }, Name = tempList[journal].Name, Date = tempList[journal].Date, NumberIssue = tempList[journal].NumberIssue };
                    _choicePublicationForm.InsertFields(tempJournal);

                    selectedValue = tempJournal;
                    return true;
                }
            }
            return false;
        }

        private bool InsertSelectedValueNewspaper(int newspaper, List<Newspaper> tempList, ref int count, int selectedIndex, dynamic selectedValue/*ChoicePublicationForm form*/)
        {
            for (int i = 0; i < tempList[newspaper].Articles.Count; i++)
            {
                count++;
                if (selectedIndex == count)
                {
                    Newspaper tempNewspaper = new Newspaper() { Articles = new List<Article>() { tempList[newspaper].Articles[i] }, Name = tempList[newspaper].Name, Date = tempList[newspaper].Date };
                    _choicePublicationForm.InsertFields(tempNewspaper);

                    selectedValue = tempNewspaper;
                    return true;
                }
            }
            return false;
        }
        public bool CompareInsertValueFields(string publication, dynamic selectedValue, string author, string name, string pages, string dateTimePicker1, string title, string location)        
        {
            return Validation.CompareInsertValueFields(publication, selectedValue, author, name, pages, dateTimePicker1, title, location, ListPublications);
        }

        public bool SelectedValueDelete(string publication, int selectedIndex)
        {
            int count = 0;
            if (publication == ListPublications[0])
            {
                var tempList = UnitOfWork.Books.ListEntities;
                tempList.RemoveAt(--selectedIndex);
                UnitOfWork.Books.ListEntities = tempList;
                return true;
            }

            if (publication == ListPublications[1])
            {
                GetPublicationsJournal(count, selectedIndex);
            }

            if (publication == ListPublications[2])
            {
                GetPublicationsNewspaper(count, selectedIndex);
            }
            return false;
        }

        private bool GetPublicationsJournal(int count, int selectedIndex)
        {
            var tempList = UnitOfWork.Journals.ListEntities;

            for (int journal = 0; journal < tempList.Count; journal++)
            {
                for (int i = 0; i < tempList[journal].Articles.Count; i++)
                {
                    count++;
                    if (DeleteJournal(journal, i, tempList, count, selectedIndex))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool GetPublicationsNewspaper(int count, int selectedIndex)
        {
            var tempList = UnitOfWork.Newspapers.ListEntities;

            for (int newspaper = 0; newspaper < tempList.Count; newspaper++)
            {
                for (int i = 0; i < tempList[newspaper].Articles.Count; i++)
                {
                    count++;
                    if (DeleteNewspaper(newspaper, i, tempList, count, selectedIndex))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DeleteJournal(int journal, int i, List<Journal> tempList, int count, int selectedIndex)
        {
            if (selectedIndex == count)
            {
                if (tempList[journal].Articles.Count > (int)EnumArticlesCount.One)
                {
                    tempList[journal].Articles.RemoveAt(i);
                    UnitOfWork.Journals.ListEntities = tempList;
                    return true;
                }
                else
                {
                    tempList.RemoveAt(journal);
                    UnitOfWork.Journals.ListEntities = tempList;
                    return true;
                }
            }
            return false;
        }

        private bool DeleteNewspaper(int newspaper, int i, List<Newspaper> tempList, int count, int selectedIndex)
        {
            if (selectedIndex == count)
            {
                if (tempList[newspaper].Articles.Count > (int)EnumArticlesCount.One)
                {
                    tempList[newspaper].Articles.RemoveAt(i);
                    UnitOfWork.Newspapers.ListEntities = tempList;
                    return true;
                }
                else
                {
                    tempList.RemoveAt(newspaper);
                    UnitOfWork.Newspapers.ListEntities = tempList;
                    return true;
                }
            }
            return false;
        }
    }
}
