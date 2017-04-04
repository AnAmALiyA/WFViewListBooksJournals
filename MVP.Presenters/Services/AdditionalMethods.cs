using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Presenters.Services
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
        
        public static string[] GetListStringAuthors(this ICollection<Author> listAuthors)
        {
            string[] tempArray = new string[listAuthors.Count];

            int index = default(int);
            foreach (Author author in listAuthors)
            {
                tempArray[index]= author.GetStringAuthor();
                index++;
            }
            return tempArray;
        }
    }
}
