using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Services;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class NewspaperRepository
    {
        private static NewspaperRepository _instance;
        private MockDataProvider _dataBase;

        public NewspaperRepository()
        {
            _dataBase = MockDataProvider.Instance;
        }

        public static NewspaperRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NewspaperRepository();
                }
                return _instance;
            }
        }

        public List<Newspaper> GetAll()
        {
            List<Newspaper> tempListNewspaper = new List<Newspaper>();
            var newspapers = _dataBase.Newspapers;
            tempListNewspaper.AddRange(newspapers);
            return tempListNewspaper;
        }

        public void Save()
        {
            string fileXML = "Newspapers.xml";

            ExistFile(fileXML);
            CreateSaveFile(fileXML);
        }

        private void ExistFile(string fileXML)
        {
            FileInfo fileInfo = new FileInfo(fileXML);

            if (fileInfo.Exists == true)
            {
                fileInfo.Delete();
            }
        }

        private void CreateSaveFile(string fileXML)
        {
            List<Newspaper> newspapers = _dataBase.Newspapers;
            
            XmlSerializer serializer = new XmlSerializer(typeof(List<Newspaper>));            
            using (StreamWriter streamWriter = new StreamWriter(fileXML))
            {
                serializer.Serialize(streamWriter, newspapers);
            }
        }
        
        public void Create(Author selectedAuthor, string title, string location, string namePublication, DateTime date)
        {
            List<Author> collectionAuthor = new List<Author>();
            collectionAuthor.Add(selectedAuthor);

            List<Article> collectionArticles = new List<Article>();
            Article itemArticle = new Article() { Authors = collectionAuthor, Title = title, Location = location };
            collectionArticles.Add(itemArticle);

            var newspaper = new Newspaper() { Articles = collectionArticles, Name = namePublication, Date = date };
            _dataBase.Newspapers.Add(newspaper);
        }
        
        public void Update(Newspaper selectedNewspaper, Author selectedAuthor, string title, string location, string namePublication, DateTime date)
        {
            var newspaperDB = _dataBase.Newspapers;
            var selectedNewspaperArticle = selectedNewspaper.Articles.First();

            foreach (var newspaper in newspaperDB)
            {
                if (newspaper.Name == selectedNewspaper.Name && newspaper.Date == selectedNewspaper.Date)
                {
                    UpdateArticle(selectedNewspaperArticle, newspaper, selectedAuthor, newspaperDB, title, location, namePublication, date);

                    newspaper.Name = namePublication;
                    newspaper.Date = date;
                    break;
                }
            }
            _dataBase.Newspapers = newspaperDB;
        }

        private void UpdateArticle(Article selectedJournalArticle, Newspaper journal, Author selectedAuthor, List<Newspaper> journalDB, string title, string location, string namePublication, DateTime date)
        {
            foreach (var article in journal.Articles)
            {
                if (article.Equals(selectedJournalArticle))
                {
                    article.Title = title;
                    article.Location = location;

                    Author exist = article.Authors.First(a => a == selectedAuthor);
                    if (exist == null)
                    {
                        article.Authors.Add(selectedAuthor);
                    }
                    return;
                }
            }
        }

        public void Delete(Newspaper newspaperDelete)
        {
            var newspaperDB = _dataBase.Newspapers;
            var articleDelete = newspaperDelete.Articles.First();           

            foreach (var newspaper in newspaperDB)
            {
                if (newspaper.Name == newspaperDelete.Name && newspaper.Date == newspaperDelete.Date)
                {
                    DeleteArticle(newspaper, articleDelete, newspaperDB);
                    break;
                }
            }
            _dataBase.Newspapers = newspaperDB;
        }

        private void DeleteArticle(Newspaper newspaper, Article articleDelete, List<Newspaper> newspaperDB)
        {
            foreach (var article in newspaper.Articles)
            {
                if (article.Equals(articleDelete))
                {
                    newspaper.Articles.Remove(article);
                    int countArticle = newspaper.Articles.Count();
                    if (countArticle <= default(int))
                    {
                        newspaperDB.Remove(newspaper);
                    }
                    return;
                }                
            }
        }        
    }
}
