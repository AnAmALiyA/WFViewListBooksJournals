using System.Xml;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using System.Linq;
using System;
using WFViewListBooksJournals.Models.Services;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class NewspaperRepository
    {
        private static IEnumerable<Newspaper> ListEntities { get; set; }
        private UnitOfWork _unitOfWork;

        public NewspaperRepository(AllLiterary allLiterary, UnitOfWork unitOfWork)
        {
            ListEntities = allLiterary.Newspapers as IEnumerable<Newspaper>;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Newspaper> GetAll()
        {
            return ListEntities;
        }

        public void Save()
        {
            string file = "Newspapers.xml";
            XmlTextWriter writer = new XmlTextWriter(file, null)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''
            };

            writer.WriteStartDocument();
            writer.WriteStartElement("ListOfNewspaper");

            foreach (Newspaper newspaper in ListEntities)
            {
                writer.WriteStartElement("Newspaper");
                foreach (Article article in newspaper.Articles)
                {
                    writer.WriteStartElement("Article");
                    writer.WriteStartElement("Author");
                    writer.WriteString(article.Authors.GetStringAuthors());
                    writer.WriteEndElement();
                    writer.WriteStartElement("ArticleTitle");
                    writer.WriteString(article.Title);
                    writer.WriteEndElement();
                    writer.WriteStartElement("LocationArticle");
                    writer.WriteString(article.Location);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteStartElement("NameNewspaper");
                writer.WriteString(newspaper.Name);
                writer.WriteEndElement();
                writer.WriteStartElement("Date");
                writer.WriteString(newspaper.Date.ToString("dd.MM.yyyy"));
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.Close();
        }

        public bool Find(string author, string namePublication, DateTime date, string title, string location)
        {

            foreach (var newspaper in ListEntities)
            {
                if (newspaper.Name != namePublication & newspaper.Date != date)
                {
                    continue;
                }
                bool flag = FindArticleAuthor(newspaper.Articles, author, title, location);
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

        public void Create(string author, string name, DateTime date, string title, string location)
        {
            ICollection<Author> collectionAuthor = new List<Author>();
            Author itemAuthor = _unitOfWork.Author.GetAll().Find(x => x.GetStringAuthor() == author);
            collectionAuthor.Add(itemAuthor);

            ICollection<Article> collectionArticle = new List<Article>();
            Article itemArticle = new Article() { Authors = collectionAuthor, Title = title, Location = location };
            collectionArticle.Add(itemArticle);

            ICollection<Newspaper> newspapers = ListEntities as ICollection<Newspaper>;
            newspapers.Add(new Newspaper() { Articles = collectionArticle, Name = name, Date = date });
            ListEntities = newspapers;
        }

        public void Udate(Newspaper changeableNewspaper, string author, string name, DateTime date, string title, string location, int selected)
        {
            var newspapers = GetAll().ToList();
            for (int i = 0; i < newspapers.Count; i++)
            {
                if (newspapers[i].Name != changeableNewspaper.Name || newspapers[i].Date != changeableNewspaper.Date)
                {
                    continue;
                }
                newspapers[i] = UdateArticle(newspapers[i], changeableNewspaper, author, name, date, title, location);
                break;
            }
            ListEntities = newspapers as ICollection<Newspaper>;
        }

        private Newspaper UdateArticle(Newspaper newspaper, Newspaper changeableJournal, string author, string name, DateTime date, string title, string location)
        {
            var udateArticles = newspaper.Articles.ToList();
            var changeableArticles = changeableJournal.Articles.ToList();

            for (int j = 0; j < newspaper.Articles.Count; j++)
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

                newspaper.Name = name;
                newspaper.Date = date;
                return newspaper;
            }
            return newspaper;
        }

        public void Delete(Newspaper newspaper)
        {
            var newspapers = GetAll().ToList();
            for (int i = 0; i < newspapers.Count; i++)
            {
                if (newspapers[i].Name == newspaper.Name & newspapers[i].Date == newspaper.Date)
                {
                    var articles = newspapers[i].Articles as List<Article>;
                    var baseArticles = newspaper.Articles as List<Article>;

                    newspapers = DeleteArticle(newspapers, newspaper, articles, baseArticles, i);
                    ListEntities = newspapers as ICollection<Newspaper>;
                    return;
                }
            }
        }

        private List<Newspaper> DeleteArticle(List<Newspaper> newspapers, Newspaper newspaper, List<Article> articles, List<Article> baseArticles, int i)
        {
            for (int j = 0; j < articles.Count; j++)
            {
                for (int z = 0; z < baseArticles.Count; z++)
                {
                    DeleteArticleInFor(newspapers, articles, baseArticles, j, z, i);                    
                }
            }
            return newspapers;
        }

        private List<Newspaper> DeleteArticleInFor(List<Newspaper> newspapers, List<Article> articles, List<Article> baseArticles, int j, int z, int i)
        {
            if (articles[j].Title == baseArticles[z].Title & articles[j].Location == baseArticles[z].Location)
            {
                articles.RemoveAt(j);
                if (articles.Count == default(int))
                {
                    newspapers.RemoveAt(i);
                    return newspapers;
                }
            }
            return newspapers;
        }
    }
}
