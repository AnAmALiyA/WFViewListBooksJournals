using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class AuthorRepository
    {
        public static List<Author> ListAuthor { get; set; }
        public AuthorRepository(AllLiterary allLiterary)
        {
            ListAuthor = GetListAuthor(allLiterary.Authors);
        }
        private List<Author> GetListAuthor(Dictionary<string, Author> entities)
        {
            List<Author> tempList = new List<Author>();
            foreach (Author item in entities.Values)
            {
                tempList.Add(item);
            }
            return tempList;
        }
        public List<Author> GetAll()
        {
            return ListAuthor;
        }

        public bool Find(string firstName, string secondName, string lastName, string age, bool nationality)
        {
            if (age == string.Empty)
            {
                age = default(int).ToString();
            }

            foreach (Author author in ListAuthor)
            {
                if (author.FirstName == firstName & author.SecondName == secondName & author.LastName == lastName & author.Age == Int32.Parse(age) & author.InitialsOption == nationality)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add(string firstName, string secondName, string lastName, string age, bool initialsOption)
        {
            if (age==string.Empty)
            {
                age = default(int).ToString();
            }
            Author author = new Author() { FirstName = firstName, SecondName = secondName, LastName = lastName, Age = Int32.Parse(age), InitialsOption = initialsOption };
            ListAuthor.Add(author);
        }
    }
}