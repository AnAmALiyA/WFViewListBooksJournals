using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Services
{
    public static class AdditionalMethods
    {
        public static string GetStringAuthors(this ICollection<Author> listAuthors)
        {
            string authors = string.Empty;
            foreach (Author author in listAuthors)
            {
                authors += author.GetStringAuthor();
            }
            return authors;
        }

        public static string GetStringAuthor(this Author author)
        {
            string tempAuthor = string.Empty;
            tempAuthor += author.InitialsOption ? author.FirstName + author.LastName + " " + author.SecondName : author.SecondName + " " + author.FirstName + author.LastName;
            tempAuthor += author.Age == default(int) ? string.Empty : " " + author.Age.ToString();
            return tempAuthor;
        }
    }
}
