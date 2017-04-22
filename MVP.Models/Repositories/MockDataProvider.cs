using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class MockDataProvider
    {
        private static MockDataProvider _instace;
        public Dictionary<string, Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<Journal> Journals { get; set; }
        public List<Newspaper> Newspapers { get; set; }

        private MockDataProvider()
        {
            Authors = FillAuthors();
            Books = FillBooks();
            Journals = FillJournals();
            Newspapers = FillNewspapers();
        }
        
        public static MockDataProvider Instance
        {
            get
            {
                if (_instace == null)
                {
                    _instace = new MockDataProvider();
                }
                return _instace;
            }
        }

        private Dictionary<string, Author> FillAuthors()
        {
            Dictionary<string, Author> authors = new Dictionary<string, Author>();

            authors.Add("Rihter", new Author { FirstName = "Дж.", SecondName = "Рихтер", InitialsOption = true });

            authors.Add("Trolsen", new Author { FirstName = "Эндрю", SecondName = "Троелсен", InitialsOption = true });

            authors.Add("Gamma", new Author { FirstName = "Э.", SecondName = "Гамма", InitialsOption = true });
            authors.Add("Helm", new Author { FirstName = "Р.", SecondName = "Хелм", InitialsOption = true });
            authors.Add("Jonson", new Author { FirstName = "Р.", SecondName = "Джонсон", InitialsOption = true });
            authors.Add("Vlissides", new Author { FirstName = "Д.", SecondName = "Влиссидес", InitialsOption = true });

            authors.Add("ShevchukA", new Author { FirstName = "А.", SecondName = "Шевчук" });
            authors.Add("Ohrimenko", new Author { FirstName = "Д.", SecondName = "Охрименко" });
            authors.Add("Kasyanov", new Author { FirstName = "А.", SecondName = "Касьянов" });

            authors.Add("Klayn", new Author { FirstName = "К.", SecondName = "Клайн", InitialsOption = true });
            authors.Add("Klayna", new Author { FirstName = "Д.", SecondName = "Клайна", InitialsOption = true });
            authors.Add("Hanta", new Author { FirstName = "Б.", SecondName = "Ханта", InitialsOption = true });

            authors.Add("Shevchuk", new Author { FirstName = "Антон", SecondName = "Шевчук" });
            authors.Add("Flanagun", new Author { FirstName = "Дэвид", SecondName = "Флэнаган", InitialsOption = true });
            authors.Add("Zakas", new Author { FirstName = "Николас", SecondName = "Закас", InitialsOption = true });
            authors.Add("Henik", new Author { FirstName = "Бен", SecondName = "Хеник", InitialsOption = true });
            authors.Add("Freeman", new Author { FirstName = "Адам", SecondName = "Фримен", InitialsOption = true });

            authors.Add("Holodnaya", new Author { FirstName = "М.", SecondName = "Холодная", LastName = "А." });

            authors.Add("Prusakova", new Author { FirstName = "О.", SecondName = "Прусакова", LastName = "А." });
            authors.Add("Sergienko", new Author { FirstName = "Е.", SecondName = "Сергиенко", LastName = "А." });

            authors.Add("DAddato", new Author { FirstName = "A.", SecondName = "D Addato", LastName = "V.", InitialsOption = true });
            authors.Add("Barlow", new Author { FirstName = "D.", SecondName = "Barlow", LastName = "H.", InitialsOption = true });
            authors.Add("Chuprikova", new Author { FirstName = "Н.", SecondName = "Чуприкова", LastName = "И." });

            authors.Add("Dubovik", new Author { FirstName = "В.", SecondName = "Дубовик." });
            authors.Add("Nikolaeva", new Author { FirstName = "С.", SecondName = "Николаева." });
            authors.Add("Risev", new Author { FirstName = "В.", SecondName = "Рысев.", Age = 2001 });

            authors.Add("RisevB", new Author { FirstName = "В.", SecondName = "Рысев.", Age = 2002 });

            return authors;
        }

        private List<Book> FillBooks()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book { Name = "\"CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#\"", Authors = new List<Author>() { Authors["Rihter"] }, Date = new DateTime(2013, 1, 1), Pages = 896 });
            books.Add(new Book { Name = "\"Язык программирования C#5.0 и платформа .NET 4.5\"", Authors = new List<Author>() { Authors["Trolsen"] }, Date = new DateTime(2015, 1, 1), Pages = 1534 });
            books.Add(new Book { Name = "\"Примеры объектно-ориентированного проектирования Паттерны проектирования\"", Authors = new List<Author>() { Authors["Gamma"], Authors["Helm"], Authors["Jonson"], Authors["Vlissides"] }, Date = new DateTime(2010, 1, 1), Pages = 368 });
            books.Add(new Book { Name = "\"Design Patterns via C#. Приемы объектно - ориентированного проектирования\"", Authors = new List<Author>() { Authors["ShevchukA"], Authors["Ohrimenko"], Authors["Kasyanov"] }, Date = new DateTime(2015, 1, 1), Pages = 288 });
            books.Add(new Book { Name = "\"SQL Справочник\"", Authors = new List<Author>() { Authors["Klayn"], Authors["Klayna"], Authors["Hanta"] }, Date = new DateTime(2015, 10, 9), Pages = 119 });
            books.Add(new Book { Name = "\"jQuery Учебник для начинающих\"", Authors = new List<Author>() { Authors["Shevchuk"] }, Date = new DateTime(2013, 1, 1), Pages = 149 });
            books.Add(new Book { Name = "\"JavaScript Подробное руководство\"", Authors = new List<Author>() { Authors["Flanagun"] }, Date = new DateTime(2008, 1, 1), Pages = 982 });
            books.Add(new Book { Name = "\"JavaScript для профессиональных веб - разработчиков\"", Authors = new List<Author>() { Authors["Zakas"] }, Date = new DateTime(2015, 1, 1), Pages = 960 });
            books.Add(new Book { Name = "\"HTML и CSS. Путь к совершенству.\"", Authors = new List<Author>() { Authors["Henik"] }, Date = new DateTime(2011, 1, 1), Pages = 336 });
            books.Add(new Book { Name = "\"ASP.NET MVC 4 с примерами на C# 5.0 для профессионалов\"", Authors = new List<Author>() { Authors["Freeman"] }, Date = new DateTime(2014, 1, 1), Pages = 666 });
            return books;
        }

        private List<Journal> FillJournals()
        {
            List<Journal> journals = new List<Journal>();

            List<Article> article = new List<Article>();
            article.Add(new Article { Authors = new List<Author>() { Authors["Holodnaya"] }, Title = "Когнитивный стиль как квадриполярное измерение.", Location = "46–56" });
            article.Add(new Article { Authors = new List<Author>() { Authors["Holodnaya"] }, Title = "Когнитивный стиль как квадриполярное измерение. 2", Location = "46–56" });
            journals.Add(new Journal { Articles = article, Name = "Психологический журнал.", Date = new DateTime(2000, 1, 1), NumberIssue = "21(4)" });

            List<Article> articleZero = new List<Article>();
            articleZero.Add(new Article { Authors = new List<Author>() { Authors["Prusakova"], Authors["Sergienko"] }, Title = "Понимание эмоций детьми дошкольного возраста.", Location = "24–35" });
            articleZero.Add(new Article { Authors = new List<Author>() { Authors["Prusakova"], Authors["Sergienko"] }, Title = "Понимание эмоций детьми дошкольного возраста. 2", Location = "24–35" });
            journals.Add(new Journal { Articles = articleZero, Name = "Вопросы психологии.", Date = new DateTime(2006, 1, 1), NumberIssue = "No. 4" });

            List<Article> articleOne = new List<Article>();
            articleOne.Add(new Article { Authors = new List<Author>() { Authors["DAddato"] }, Title = "Secular trends in twinning rates.", Location = "147–151" });
            articleOne.Add(new Article { Authors = new List<Author>() { Authors["DAddato"] }, Title = "Secular trends in twinning rates. 2", Location = "147–151" });
            journals.Add(new Journal { Articles = articleOne, Name = "Journal of Biosocial Science.", Date = new DateTime(2007, 1, 1), NumberIssue = "39(1)" });

            List<Article> articleTwo = new List<Article>();
            articleTwo.Add(new Article { Authors = new List<Author>() { Authors["Barlow"] }, Title = "Diagnoses, dimensions, and DSM-IV[Special issue].", Location = "300–453" });
            articleTwo.Add(new Article { Authors = new List<Author>() { Authors["Barlow"] }, Title = "Diagnoses, dimensions, and DSM-IV[Special issue]. 2", Location = "300–453" });
            journals.Add(new Journal { Articles = articleTwo, Name = "Journal of Abnormal Psychology", Date = new DateTime(1991, 1, 1), NumberIssue = "100(3)" });

            List<Article> articleThree = new List<Article>();
            articleThree.Add(new Article { Authors = new List<Author>() { Authors["Chuprikova"] }, Title = "На пути к материалистическому решению психофизической проблемы.", Location = "110–121" });
            articleThree.Add(new Article { Authors = new List<Author>() { Authors["Chuprikova"] }, Title = "На пути к материалистическому решению психофизической проблемы. 2", Location = "110–121" });
            journals.Add(new Journal { Articles = articleThree, Name = "От дуализма Декарта к монизму Спинозы. Вопросы философии", Date = new DateTime(2010, 1, 1), NumberIssue = "No. 10" });

            return journals;
        }

        private List<Newspaper> FillNewspapers()
        {
            List<Newspaper> newspapers = new List<Newspaper>();

            List<Article> article = new List<Article>();
            article.Add(new Article { Authors = new List<Author>() { Authors["Dubovik"] }, Title = "Молодые леса зелены", Location = "8" });
            article.Add(new Article { Authors = new List<Author>() { Authors["Dubovik"] }, Title = "Молодые леса зелены 2", Location = "8" });
            newspapers.Add(new Newspaper { Articles = article, Name = "Рэспубліка.", Date = new DateTime(2005, 2, 19) });

            List<Article> articleOne = new List<Article>();
            articleOne.Add(new Article { Authors = new List<Author>() { Authors["Nikolaeva"] }, Title = "Будем читать. Глядишь, и кризис пройдет…", Location = "9" });
            articleOne.Add(new Article { Authors = new List<Author>() { Authors["Nikolaeva"] }, Title = "Будем читать. Глядишь, и кризис пройдет… 2", Location = "9" });
            newspapers.Add(new Newspaper { Articles = articleOne, Name = "Северный комсомолец.", Date = new DateTime(2009, 4, 13) });

            List<Article> articleTwo = new List<Article>();
            articleTwo.Add(new Article { Authors = new List<Author>() { Authors["Risev"] }, Title = "Приоритет – экология", Location = "13-14" });
            articleTwo.Add(new Article { Authors = new List<Author>() { Authors["Risev"] }, Title = "Приоритет – экология 2", Location = "13-14" });
            newspapers.Add(new Newspaper { Articles = articleTwo, Name = "Волна.", Date = new DateTime(2004, 4, 4) });

            List<Article> articleThree = new List<Article>();
            articleThree.Add(new Article { Authors = new List<Author>() { Authors["RisevB"] }, Title = "Приоритет – экология 3", Location = "13-14" });
            articleThree.Add(new Article { Authors = new List<Author>() { Authors["RisevB"] }, Title = "Приоритет – экология 4", Location = "13-14" });
            newspapers.Add(new Newspaper { Articles = articleThree, Name = "Волна.", Date = new DateTime(2004, 4, 4) });

            return newspapers;
        }
    }
}