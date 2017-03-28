using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Presenters.Infrastructure
{
    public class Validation
    {
        public bool CheckEmptyField(List<string> listPublications, string publication, string author, string namePublication, string pages, string title, string location)
        {
            if (listPublications[0] == publication)
            {
                if (author == "" || namePublication == "" || pages == "")
                {
                    return true;
                }
            }

            if (listPublications[1] == publication)
            {
                if (author == "" || namePublication == "" || pages == "" || title == "" || location == "")
                {
                    return true;
                }
            }

            if (listPublications[2] == publication)
            {
                if (author == "" || namePublication == "" || title == "" || location == "")
                {
                    return true;
                }
            }
            return false;
        }

        public bool NullField(params string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] =="")
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckCorrectnessData(List<string> listPublications, string publication, string author, string namePublication, string pages, string title, string location)
        {
            if (listPublications[0] == publication)
            {
                if(!CheckCorrectnessAuthors(author) || !CheckCorrectnessPages(pages) || !ExistNumberText(namePublication))
                {
                    return true;
                }
            }

            if (listPublications[1] == publication)
            {
                if (!CheckCorrectnessAuthors(author) || !ExistNumberText(namePublication) || !ExistNumberText(pages) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
                {
                    return true;
                }
            }

            if (listPublications[2] == publication)
            {
                if (!CheckCorrectnessAuthors(author) || !ExistNumberText(namePublication) || !ExistNumberText(title) || !CheckCorrectnessLocation(location))
                {
                    return true;
                }
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
            string patternName = @"^[\d\-]+$";
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
        public bool CompareInsertValueFields(string publication, dynamic selectedValue, string author, string name, string pages, string dateTimePicker1, string title, string location, List<string> ListPublications)        
        {
            if (selectedValue == null)
            {
                return false;
            }

            if (ListPublications[0] == publication)
            {
                if (author == AdditionalMethods.GetStringAuthors(selectedValue.Author) & name == (string)selectedValue.Name & pages == (string)selectedValue.Pages.ToString() & dateTimePicker1 == (string)selectedValue.Date.ToString("D"))
                {
                    return false;
                }
            }

            if (ListPublications[1] == publication)
            {
                if (author == AdditionalMethods.GetStringAuthors(selectedValue.Articles[0].Author) & name == (string)selectedValue.Name & pages == (string)selectedValue.NumberIssue & dateTimePicker1 == (string)selectedValue.Date.ToString("D") & title == (string)selectedValue.Articles[0].Title & location == (string)selectedValue.Articles[0].Location)
                {
                    return false;
                }
            }

            if (ListPublications[2] == publication)
            {
                if (author == AdditionalMethods.GetStringAuthors(selectedValue.Articles[0].Author) & name == (string)selectedValue.Name & dateTimePicker1 == (string)selectedValue.Date.ToString("D") & title == (string)selectedValue.Articles[0].Title & location == (string)selectedValue.Articles[0].Location)
                {
                    return false;
                }
            }
            return true;
        }
    }
}