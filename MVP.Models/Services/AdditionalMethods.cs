using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Services
{
    public class AdditionalMethods
    {
        private static AdditionalMethods _instance;

        private AdditionalMethods() { }

        public static AdditionalMethods Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AdditionalMethods();
                }
                return _instance;
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
