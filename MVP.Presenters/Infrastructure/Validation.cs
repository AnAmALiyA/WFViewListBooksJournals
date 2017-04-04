using System;
using System.Text.RegularExpressions;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class Validation
    {
        public bool CheckEmptyFields(string author, string namePublication, string pages)
        {
            if (author == string.Empty || namePublication == string.Empty || pages == string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmptyFields(string author, string namePublication, string numberIssue, string title, string location)
        {
            if (author == string.Empty || namePublication == string.Empty || numberIssue == string.Empty || title == string.Empty || location == string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmptyFields(string author, string namePublication, string title, string location)
        {
            if (author == string.Empty || namePublication == string.Empty || title == string.Empty || location == string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool NullField(string firstName, string secondName, string lastName, string nationality)
        {
            if (firstName == string.Empty || secondName == string.Empty || lastName == string.Empty || nationality == string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool ValidationData(string namePublication, DateTime date, string pages)
        {
            if (!CheckCorrectnessDate(date) || !CheckCorrectnessPages(pages) || !ExistNumberText(namePublication))
            {
                return true;
            }
            return false;
        }

        public bool ValidationData(string namePublication, string numberIssue, DateTime date, string title, string location)
        {
            if (!CheckCorrectnessDate(date) || !ExistNumberText(namePublication) || !ExistNumberText(numberIssue) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
            {
                return true;
            }
            return false;
        }

        public bool ValidationData(string namePublication, DateTime date, string title, string location)
        {
            if (!CheckCorrectnessDate(date) || !ExistNumberText(namePublication) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
            {
                return true;
            }
            return false;
        }

        public bool CheckCorrectnessDataAuthor(string firstName, string secondName, string lastName, string age)
        {
            if (!CheckCorrectnessAuthor(firstName) || !CheckCorrectnessAuthor(secondName) || !CheckCorrectnessAuthor(lastName) || !CheckCorrectnessAge(age))
            {
                return true;
            }
            return false;
        }
        public bool CheckCorrectnessDataAuthor(string firstName, string secondName, string lastName)
        {
            if (!CheckCorrectnessAuthor(firstName) || !CheckCorrectnessAuthor(secondName) || !CheckCorrectnessAuthor(lastName))
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