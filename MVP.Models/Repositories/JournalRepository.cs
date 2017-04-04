using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Services;
using System;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class JournalRepository
    {
        private static IEnumerable<Journal> ListEntities { get; set; }
        private UnitOfWork _unitOfWork;

        public JournalRepository(AllLiterary allLiterary, UnitOfWork unitOfWork)
        {
            ListEntities = allLiterary.Journals as IEnumerable<Journal>;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Journal> GetAll()
        {
            return ListEntities;
        }

        public void Save()
        {
            string fileTxt = "Journal.txt";

            FileInfo fileInfo = new FileInfo(fileTxt);

            if (fileInfo.Exists == true)
            {
                fileInfo.Delete();
            }

            using (FileStream file = new FileStream(fileTxt, FileMode.Create, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding(1251)))
            {
                foreach (Journal journal in ListEntities)
                {
                    writer.WriteLine("Name={0}, Date={1}, Number Issue={2}", journal.Name, journal.Date.ToString("D"), journal.NumberIssue);
                    foreach (Article article in journal.Articles)
                    {
                        writer.WriteLine("\tAuthor={0}, Title={1}, Location={2}", article.Authors.GetStringAuthors(), article.Title, article.Location);
                    }
                    writer.WriteLine();
                }
            }
        }

        public bool Find(string author, string namePublication, string numberIssue, DateTime date, string title, string location)
        {
            foreach (var journal in ListEntities)
            {
                if (journal.Name != namePublication & journal.Date != date & journal.NumberIssue != numberIssue)
                {
                    continue;
                }
                bool flag = FindArticleAuthor(journal.Articles, author, title, location);
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        private bool FindArticleAuthor(ICollection<Article> articles, string author, string title, string location)
        {
            foreach (var article in articles)
            {
                if (article.Title != title & article.Location != location)
                {
                    continue;
                }
                foreach (var item in article.Authors)
                {
                    if (item.GetStringAuthor() != author)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Create(string author, string name, string numberIssue, DateTime date, string title, string location)
        {
            ICollection<Author> collectionAuthor = new List<Author>();
            Author itemAuthor = _unitOfWork.Author.GetAll().Find(x => x.GetStringAuthor() == author);
            collectionAuthor.Add(itemAuthor);            

            ICollection<Article> collectionArticles = new List<Article>();
            Article itemArticle = new Article() { Authors = collectionAuthor, Title = title, Location = location };
            collectionArticles.Add(itemArticle);

            ICollection<Journal> journals = ListEntities as ICollection<Journal>;
            journals.Add(new Journal() { Articles = collectionArticles, Name = name, Date = date, NumberIssue = numberIssue });
            ListEntities = journals;
        }

        public void Udate(Journal changeableJournal, string author, string name, string numberIssue, DateTime date, string title, string location, int selected)
        {
            var journals = GetAll().ToList();
            for (int i = 0; i < journals.Count; i++)
            {
                if (journals[i].Name != changeableJournal.Name || journals[i].Date != changeableJournal.Date || journals[i].NumberIssue != changeableJournal.NumberIssue)
                {
                    continue;
                }

                journals[i] = UdateArticle(journals[i], changeableJournal, author, name, numberIssue, date, title, location);
                break;
            }
            ListEntities = journals as ICollection<Journal>;
        }

        private Journal UdateArticle(Journal journal, Journal changeableJournal, string author, string name, string numberIssue, DateTime date, string title, string location)
        {
            List<Article> udateArticles = journal.Articles.ToList();
            var changeableArticles = changeableJournal.Articles.ToList();

            for (int j = 0; j < journal.Articles.Count; j++)
            {
                if (udateArticles[j].Title != changeableArticles[default(int)].Title || udateArticles[j].Location != changeableArticles[default(int)].Location)
                {
                    continue;
                }

                bool existAuthor = false;
                foreach (Author itemAuthor in changeableArticles[default(int)].Authors)
                {
                    if (itemAuthor.GetStringAuthor() == author)
                    {
                        existAuthor = true;
                    }
                }

                if (!existAuthor)
                {
                    Author newAuthor = _unitOfWork.Author.GetAll().Find(a => a.GetStringAuthor() == author);
                    udateArticles[j].Authors.Add(newAuthor);
                }

                udateArticles[j].Title = title;
                udateArticles[j].Location = location;

                journal.Name = name;
                journal.Date = date;
                journal.NumberIssue = numberIssue;
                return journal;
            }
            return journal;
        }

        public void Delete(Journal journal)
        {
            var journals = GetAll().ToList();
            for (int i = 0; i < journals.Count; i++)
            {
                if (journals[i].Name == journal.Name & journals[i].Date == journal.Date & journals[i].NumberIssue == journal.NumberIssue)
                {
                    var articles = journals[i].Articles.ToList();
                    var baseArticles = journal.Articles.ToList();

                    journals = DeleteArticle(journals, journal, articles, baseArticles, i);
                    ListEntities = journals as ICollection<Journal>;
                    return;
                }
            }
        }

        private List<Journal> DeleteArticle(List<Journal> journals, Journal journal, List<Article> articles, List<Article> baseArticles, int i)
        {
            for (int j = 0; j < articles.Count; j++)
            {
                for (int z = 0; z < baseArticles.Count; z++)
                {
                    DeleteArticleInFor(journals, articles, baseArticles, j, z, i);                    
                }
            }
            return journals;
        }

        private List<Journal> DeleteArticleInFor(List<Journal> journals, List<Article> articles, List<Article> baseArticles, int j, int z, int i)
        {
            if (articles[j].Title == baseArticles[z].Title & articles[j].Location == baseArticles[z].Location)
            {
                articles.RemoveAt(j);
                if (articles.Count == default(int))
                {
                    journals.RemoveAt(i);
                    return journals;
                }
            }
            return journals;
        }
    }
}
