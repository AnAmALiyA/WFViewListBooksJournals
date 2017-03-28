using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Infrastructure
{
    public static class AdditionalMethods
    {
        public static string GetStringAuthors(this List<Author> listAuthors)
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
            tempAuthor += author.Age == (int)EnumErrors.EmptyField ? "" : " " + author.Age.ToString();
            return tempAuthor;
        }
    }
}
