using System.Linq;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class AuthorRepository
    {
        private static AuthorRepository _instance;
        private DataBase _dataBase;
        
        public static AuthorRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthorRepository();
                }
                return _instance;
            }
        }

        private AuthorRepository()
        {
            _dataBase = DataBase.Instance;
        }

        public List<Author> GetAll()
        {
            var tempIEnumerableAuthor = _dataBase.Authors.Select(x=>x.Value);

            List<Author> tempListAuthor = new List<Author>();
            tempListAuthor.AddRange(tempIEnumerableAuthor);

            return tempListAuthor;
        }
        
        public void Add(Author author)
        {
            string name = GenerateName(author.SecondName, default(int));
            _dataBase.Authors.Add(name, author);
        }

        public bool ExistAuthor(Author author)
        {
            var tempListAuthor = GetAll();
            Author findAuthor = tempListAuthor.Find(a => a == author);
            if (findAuthor == null)
            {
                return false;
            }
            return true;
        }

        private string GenerateName(string name, int number)
        {
            if (number == default(int))
            {
                bool exist = _dataBase.Authors.ContainsKey(name);
                if (!exist)
                {
                    return name;
                }
            }

            string tempName = name + number.ToString();
            Author findAuthor = _dataBase.Authors[tempName];
            if (findAuthor == null)
            {
                return name;
            }

            number++;
            return GenerateName(name, number);
        }

    }
}