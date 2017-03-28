using System.Xml;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class NewspaperRepository
    {
        public List<Newspaper> ListEntities { get; set; }

        public NewspaperRepository(AllLiterary allLiterary)
        {
            ListEntities = allLiterary.FillTheListNewspaper();
        }

        public void SaveXML()
        {
            var xmlWriter = new XmlTextWriter("Newspapers.xml", null)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''
            };

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ListOfNewspaper");

            foreach (Newspaper newspaper in ListEntities)
            {
                xmlWriter.WriteStartElement("Newspaper");
                foreach (Article article in newspaper.Articles)
                {
                    xmlWriter.WriteStartElement("Article");
                    xmlWriter.WriteStartElement("Author");
                    xmlWriter.WriteString(article.Author.GetStringAuthors());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("ArticleTitle");
                    xmlWriter.WriteString(article.Title);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("LocationArticle");
                    xmlWriter.WriteString(article.Location);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteStartElement("NameNewspaper");
                xmlWriter.WriteString(newspaper.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Date");
                xmlWriter.WriteString(newspaper.Date.ToString("dd.MM.yyyy"));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
            xmlWriter.Close();
        }
    }
}
