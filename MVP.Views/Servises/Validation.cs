using System;
using System.Linq;
using System.Text.RegularExpressions;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Views.Servises
{
    public class Validation
    {
        private static Validation _instance;
        private DisplayOfData _displayOfData;

        public Validation() {
            _displayOfData = DisplayOfData.Instance;
        }

        public static Validation Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Validation();
                }
                return _instance;
            }
        }

        public bool ValidationDataPublication(Book selectedBook, Author selectedAuthor, string namePublication, DateTime date, string pages)
        {
            bool notValid = true;

            notValid = NotChangedPublication(selectedBook, selectedAuthor, namePublication, date, pages);
            if (notValid)
            {
                return false;
            }

            notValid = CheckEmptyFieldsPublication(namePublication, pages);
            if (notValid)
            {
                return false;
            }

            notValid = CorrectDataPublication(namePublication, date, pages);
            if (notValid)
            {
                return false;
            }
            return true;
        }

        public bool ValidationDataPublication(Journal selectedJournal, Author selectedAuthor, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            bool notValid = true;

            notValid = NotChangedPublication(selectedJournal, selectedAuthor, title, location, namePublication, date, numberIssue);
            if (notValid)
            {
                return false;
            }

            notValid = CheckEmptyFieldsPublication(title, location, namePublication, numberIssue);
            if (notValid)
            {
                return false;
            }

            notValid = CorrectDataPublication(title, location, namePublication, date, numberIssue);
            if (notValid)
            {
                return false;
            }
            return true;
        }

        public bool ValidationDataPublication(Newspaper selectedNewspaper, Author selectedAuthor, string title, string location, string namePublication, DateTime date)
        {
            bool notValid = true;

            notValid = NotChangedPublication(selectedNewspaper, selectedAuthor, title, location, namePublication, date);
            if (notValid)
            {
                return false;
            }

            notValid = CheckEmptyFieldsPublication(title, location, namePublication);
            if (notValid)
            {
                return false;
            }

            notValid = CorrectDataPublication(title, location, namePublication, date);
            if (notValid)
            {
                return false;
            }
            return true;
        }
        
        public bool ValidationDataAuthor(string firstName, string secondName, string lastName, string age, string nationality)
        {
            bool notValid = true;
            
            notValid = CheckEmptyFieldsAuthor(firstName, secondName, lastName, age, nationality);
            if (notValid)
            {
                return false;
            }

            notValid = CorrectDataAuthor(firstName, secondName, lastName, age);
            if (notValid)
            {
                return false;
            }
            return true;
        }
        
        private bool NotChangedPublication(Book selectedBook, Author selectedAuthor, string namePublication, DateTime date, string pages)
        {
            string selectedBookPages = selectedBook.Pages.ToString();
            if (selectedBook.Name == namePublication && selectedBook.Date == date && selectedBookPages == pages)
            {
                var exist = selectedBook.Authors.Where(x=>x.Equals(selectedAuthor));
                if (exist != null)
                {
                    return true;
                }
                //foreach (Author author in selectedBook.Authors)
                //{
                //    string stringAuthor = _displayOfData.GetStringAuthor(author);
                //    if (selectedAuthor == stringAuthor)
                //    {
                //        return true;
                //    }
                //}
            }
            return false;
        }

        private bool NotChangedPublication(Journal selectedJournal, Author selectedAuthor, string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            if (selectedJournal.Name == namePublication && selectedJournal.Date == date && selectedJournal.NumberIssue == numberIssue)
            {
                var article = selectedJournal.Articles.First();
                if (article.Title == title && article.Location == location)
                {
                    var exist = article.Authors.Where(x => x.Equals(selectedAuthor));
                    if (exist != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool NotChangedPublication(Newspaper selectedNewspaper, Author selectedAuthor, string title, string location, string namePublication, DateTime date)
        {
            if (selectedNewspaper.Name == namePublication && selectedNewspaper.Date == date)
            {
                var article = selectedNewspaper.Articles.First();
                if (article.Title == title && article.Location == location)
                {
                    var exist = article.Authors.Where(x => x.Equals(selectedAuthor));
                    if (exist != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckEmptyFieldsPublication(string namePublication, string pages)
        {
            if (namePublication == string.Empty || pages == string.Empty)
            {
                return true;
            }
            return false;
        }        

        private bool CheckEmptyFieldsPublication(string title, string location, string namePublication, string numberIssue)
        {
            if (title == string.Empty || location == string.Empty || namePublication == string.Empty || numberIssue == string.Empty)
            {
                return true;
            }
            return false;
        }

        private bool CheckEmptyFieldsPublication(string title, string location, string namePublication)
        {
            if (title == string.Empty || location == string.Empty || namePublication == string.Empty)
            {
                return true;
            }
            return false;
        }
        
        private bool CheckEmptyFieldsAuthor(string firstName, string secondName, string lastName, string age, string nationality)
        {
            if (firstName == string.Empty || secondName == string.Empty || lastName == string.Empty || age == string.Empty || nationality == string.Empty)
            {
                return true;
            }
            return false;
        }

        private bool CorrectDataPublication(string namePublication, DateTime date, string pages)
        {
            if (!CheckCorrectnessDate(date) || !CheckCorrectnessPages(pages) || !ExistNumberText(namePublication))
            {
                return true;
            }
            return false;
        }

        private bool CorrectDataPublication(string title, string location, string namePublication, DateTime date, string numberIssue)
        {
            if (!CheckCorrectnessDate(date) || !ExistNumberText(namePublication) || !ExistNumberText(numberIssue) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
            {
                return true;
            }
            return false;
        }

        private bool CorrectDataPublication(string title, string location, string namePublication, DateTime date)
        {
            if (!CheckCorrectnessDate(date) || !ExistNumberText(namePublication) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
            {
                return true;
            }
            return false;
        }        

        public bool CorrectDataAuthor(string firstName, string secondName, string lastName, string age)
        {
            if (!CheckCorrectnessAuthor(firstName) || !CheckCorrectnessAuthor(secondName) || !CheckCorrectnessAuthor(lastName) || !CheckCorrectnessAge(age))
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessAuthors(string author)
        {
            string patternName = @"^[a-zA-Zа-яА-Я0-9\.\ ]+$";
            Regex regexName = new Regex(patternName);

            bool success = false;
            success = regexName.IsMatch(author);
            if (success)
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessAuthor(string author)
        {
            string patternName = @"^[a-zA-Zа-яА-Я\.\ ]+$";
            Regex regexName = new Regex(patternName);

            bool success = false;
            success = regexName.IsMatch(author);
            if (success)
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessDate(DateTime date)
        {
            if (date <= DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessPages(string pages)
        {
            int tempPages;
            if (Int32.TryParse(pages, out tempPages))
            {
                return true;
            }
            return false;
        }

        private bool ExistNumberText(string numberText)
        {
            string patternName = @"[\w]";
            Regex regexName = new Regex(patternName);

            bool success = false;
            success = regexName.IsMatch(numberText);
            if (success)
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessLocation(string location)
        {
            string patternName = @"^[\d\s–-]+$";
            Regex regexName = new Regex(patternName);

            bool success = false;
            success = regexName.IsMatch(location);
            if (success)
            {
                return true;
            }
            return false;
        }

        private bool CheckCorrectnessAge(string age)
        {
            string patternName = @"^[\d]+$";
            Regex regexName = new Regex(patternName);

            bool success = false;
            success = regexName.IsMatch(age);
            if (success & Int32.Parse(age) > 0 & Int32.Parse(age) < DateTime.Now.Year)
            {
                return true;
            }
            return false;
        }
    }
}