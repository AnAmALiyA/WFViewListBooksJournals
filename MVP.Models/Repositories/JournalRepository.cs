using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using System;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class JournalRepository
    {
        private static JournalRepository _instance;
        private MockDataProvider _dataBase;

        public JournalRepository()
        {
            _dataBase = MockDataProvider.Instance;
        }

        public static JournalRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JournalRepository();
                }
                return _instance;
            }
        }

        public List<Journal> GetAll()
        {
            List<Journal> tempListJornal = new List<Journal>();
            var journals = _dataBase.Journals;
            tempListJornal.AddRange(journals);
            return tempListJornal;
        }

        public void Save()
        {
            string fileText = "Journal.txt";

            ExistFile(fileText);
            CreateSaveFile(fileText);
        }

        private void ExistFile(string fileText)
        {
            FileInfo fileInfo = new FileInfo(fileText);

            if (fileInfo.Exists == true)
            {
                fileInfo.Delete();
            }
        }

        private void CreateSaveFile(string fileText)
        {
            using (FileStream file = new FileStream(fileText, FileMode.Create, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding(1251)))
            {
                foreach (Journal journal in _dataBase.Journals)
                {
                    writer.WriteLine("Name={0}, Date={1}, Number Issue={2}", journal.Name, journal.Date.ToString("D"), journal.NumberIssue);
                    foreach (Article article in journal.Articles)
                    {
                        string stringAuthors = GetStringAuthorList(article.Authors);
                        writer.WriteLine("\tAuthor={0}, Title={1}, Location={2}", stringAuthors, article.Title, article.Location);
                    }
                    writer.WriteLine();
                }
            }
        }
        public string GetStringAuthor(Author author)
        {
            string tempAuthor = string.Empty;
            tempAuthor += author.InitialsOption ? author.FirstName + author.LastName + " " + author.SecondName : author.SecondName + " " + author.FirstName + author.LastName;
            tempAuthor += author.Age == default(int) ? string.Empty : " " + author.Age.ToString();
            return tempAuthor;
        }

        public string GetStringAuthorList(List<Author> listAuthors)
        {
            string authors = string.Empty;
            foreach (Author author in listAuthors)
            {
                string stringAuthor = GetStringAuthor(author);
                authors += stringAuthor;
            }
            return authors;
        }
        public void Create(Author selectedAuthor, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            List<Author> collectionAuthor = new List<Author>();            
            collectionAuthor.Add(selectedAuthor);

            List<Article> collectionArticles = new List<Article>();
            Article itemArticle = new Article() { Authors = collectionAuthor, Title = title, Location = location };
            collectionArticles.Add(itemArticle);

            var journal = new Journal() { Articles = collectionArticles, Name = namePublication, Date = date, NumberIssue = numberIssue };
            _dataBase.Journals.Add(journal);
        }
        
        public void Update(Journal selectedJournal, Author selectedAuthor, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            var journalDB = _dataBase.Journals;
            var selectedJournalArticle = selectedJournal.Articles.First();

            foreach (var journal in journalDB)
            {
                if (journal.Name == selectedJournal.Name && journal.Date == selectedJournal.Date && journal.NumberIssue == selectedJournal.NumberIssue)
                {
                    UpdateArticle(selectedJournalArticle, journal, selectedAuthor, journalDB, title, location, namePublication, date, numberIssue);

                    journal.Name = namePublication;
                    journal.Date = date;
                    journal.NumberIssue = numberIssue;
                    break;
                }
            }
            _dataBase.Journals = journalDB;
        }

        private void UpdateArticle(Article selectedJournalArticle, Journal journal, Author selectedAuthor, List<Journal> journalDB, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            foreach (var article in journal.Articles)
            {
                if (article.Equals(selectedJournalArticle))
                {
                    article.Title = title;
                    article.Location = location;

                    Author exist = article.Authors.First(a=>a == selectedAuthor);
                    if (exist == null)
                    {
                        article.Authors.Add(selectedAuthor);
                    }
                    return;
                }                
            }
        }

        public void Delete(Journal journalDelete)
        {
            var journalDB = _dataBase.Journals;
            var articleDelete = journalDelete.Articles.First();

            foreach (var journal in journalDB)
            {
                if (journal.Name == journalDelete.Name && journal.Date == journalDelete.Date && journal.NumberIssue == journalDelete.NumberIssue)
                {
                    DeleteArticle(journal, articleDelete, journalDB);
                    break;
                }               
            }
            _dataBase.Journals = journalDB;
        }

        private void DeleteArticle(Journal journal, Article articleDelete, List<Journal> journalDB)
        {
            foreach (var article in journal.Articles)
            {
                if (article.Equals(articleDelete))
                {
                    journal.Articles.Remove(article);
                    int countArticle = journal.Articles.Count();
                    if (countArticle <= default(int))
                    {
                        journalDB.Remove(journal);                        
                    }
                    return;
                }
            }
        }
    }
}
