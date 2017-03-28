using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class AllLiterary
    {
        public List<Author> ListAuthor { get; set; }
        public List<Author> NewListAuthor { get; set; }

        public AllLiterary()
        {
            ListAuthor = FillTheListAuthor();
            NewListAuthor = new List<Author>();
        }

        private List<Author> FillTheListAuthor()
        {
            List<Author> author = new List<Author>();
            author.Add(new Author { FirstName = "Дж.", SecondName = "Рихтер", LastName = "", InitialsOption = true });

            author.Add(new Author { FirstName = "Эндрю", SecondName = "Троелсен", LastName = "", InitialsOption = true });

            author.Add(new Author { FirstName = "Э.", SecondName = "Гамма", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Р.", SecondName = "Хелм", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Р.", SecondName = "Джонсон", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Д.", SecondName = " Влиссидес", LastName = "", InitialsOption = true });

            author.Add(new Author { FirstName = "А.", SecondName = "Шевчук", LastName = "" });
            author.Add(new Author { FirstName = "Д.", SecondName = "Охрименко", LastName = "" });
            author.Add(new Author { FirstName = "А.", SecondName = "Касьянов", LastName = "" });

            author.Add(new Author { FirstName = "К.", SecondName = "Клайн", LastName = "",  InitialsOption = true });
            author.Add(new Author { FirstName = "Д.", SecondName = "Клайна", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Б.", SecondName = "Ханта", LastName = "", InitialsOption = true });

            author.Add(new Author { FirstName = "Антон", SecondName = "Шевчук", LastName = "" });
            author.Add(new Author { FirstName = "Дэвид", SecondName = "Флэнаган", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Николас", SecondName = "Закас", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Бен", SecondName = "Хеник", LastName = "", InitialsOption = true });
            author.Add(new Author { FirstName = "Адам", SecondName = "Фримен", LastName = "", InitialsOption = true });

            author.Add(new Author { FirstName = "М.", SecondName = "Холодная", LastName = "А." });

            author.Add(new Author { FirstName = "О.", SecondName = "Прусакова", LastName = "А." });
            author.Add(new Author { FirstName = "Е.", SecondName = "Сергиенко", LastName = "А." });

            author.Add(new Author { FirstName = "A.", SecondName = "D'Addato", LastName = "V.", InitialsOption = true });
            author.Add(new Author { FirstName = "D.", SecondName = "Barlow", LastName = "H.", InitialsOption = true });
            author.Add(new Author { FirstName = "Н.", SecondName = "Чуприкова", LastName = "И." });

            author.Add(new Author { FirstName = "В.", SecondName = "Дубовик.", LastName = "" });
            author.Add(new Author { FirstName = "С.", SecondName = "Николаева.", LastName = "" });
            author.Add(new Author { FirstName = "В.", SecondName = "Рысев.", LastName = "", Age = 2001 });

            author.Add(new Author { FirstName = "В.", SecondName = "Рысев.", LastName = "", Age = 2002 });
            return author;
        }

        public List<Book> FillTheListBook()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book { Name = "\"CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#\"", Authors = new List<Author>() { ListAuthor[0] }, Date = new DateTime(2013, 1, 1), Pages = 896 });
            books.Add(new Book { Name = "\"Язык программирования C#5.0 и платформа .NET 4.5\"", Authors = new List<Author>() { ListAuthor[1] }, Date = new DateTime(2015, 1, 1), Pages = 1534 });
            books.Add(new Book { Name = "\"Примеры объектно-ориентированного проектирования Паттерны проектирования\"", Authors = new List<Author>() { ListAuthor[2], ListAuthor[3], ListAuthor[4], ListAuthor[5] }, Date = new DateTime(2010, 1, 1), Pages = 368 });
            books.Add(new Book { Name = "\"Design Patterns via C#. Приемы объектно -ориентированного проектирования\"", Authors = new List<Author>() { ListAuthor[6], ListAuthor[7], ListAuthor[8] }, Date = new DateTime(2015, 1, 1), Pages = 288 });
            books.Add(new Book { Name = "\"SQL Справочник\"", Authors = new List<Author>() { ListAuthor[9], ListAuthor[10], ListAuthor[11] }, Date = new DateTime(2015, 10, 9), Pages = 119 });
            books.Add(new Book { Name = "\"jQuery Учебник для начинающих\"", Authors = new List<Author>() { ListAuthor[12] }, Date = new DateTime(2013, 1, 1), Pages = 149 });
            books.Add(new Book { Name = "\"JavaScript Подробное руководство\"", Authors = new List<Author>() { ListAuthor[13] }, Date = new DateTime(2008, 1, 1), Pages = 982 });
            books.Add(new Book { Name = "\"JavaScript для профессиональных веб - разработчиков\"", Authors = new List<Author>() { ListAuthor[14] }, Date = new DateTime(2015, 1, 1), Pages = 960 });
            books.Add(new Book { Name = "\"HTML и CSS. Путь к совершенству.\"", Authors = new List<Author>() { ListAuthor[15] }, Date = new DateTime(2011, 1, 1), Pages = 336 });
            books.Add(new Book { Name = "\"ASP.NET MVC 4 с примерами на C# 5.0 для профессионалов\"", Authors = new List<Author>() { ListAuthor[16] }, Date = new DateTime(2014, 1, 1), Pages = 666 });
            return books;
        }

        public List<Journal> FillTheListJournal()
        {
            List<Journal> journals = new List<Journal>();

            List<Article> article = new List<Article>();
            article.Add(new Article { Author = new List<Author>() { ListAuthor[17] }, Title = "Когнитивный стиль как квадриполярное измерение.", Location = "46–56" });
            article.Add(new Article { Author = new List<Author>() { ListAuthor[17] }, Title = "Когнитивный стиль как квадриполярное измерение. 2", Location = "46–56" });
            journals.Add(new Journal { Articles = article, Name = "Психологический журнал.", Date = new DateTime(2000, 1, 1), NumberIssue = "21(4)" });

            List<Article> article0 = new List<Article>();
            article0.Add(new Article { Author = new List<Author>() { ListAuthor[18], ListAuthor[19] }, Title = "Понимание эмоций детьми дошкольного возраста.", Location = "24–35" });
            article0.Add(new Article { Author = new List<Author>() { ListAuthor[18], ListAuthor[19] }, Title = "Понимание эмоций детьми дошкольного возраста. 2", Location = "24–35" });
            journals.Add(new Journal { Articles = article0, Name = "Вопросы психологии.", Date = new DateTime(2006, 1, 1), NumberIssue = "No. 4" });

            List<Article> article1 = new List<Article>();
            article1.Add(new Article { Author = new List<Author>() { ListAuthor[20] }, Title = "Secular trends in twinning rates.", Location = "147–151" });
            article1.Add(new Article { Author = new List<Author>() { ListAuthor[20] }, Title = "Secular trends in twinning rates. 2", Location = "147–151" });
            journals.Add(new Journal { Articles = article1, Name = "Journal of Biosocial Science.", Date = new DateTime(2007, 1, 1), NumberIssue = "39(1)" });

            List<Article> article2 = new List<Article>();
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[21] }, Title = "Diagnoses, dimensions, and DSM-IV[Special issue].", Location = "300–453" });
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[21] }, Title = "Diagnoses, dimensions, and DSM-IV[Special issue]. 2", Location = "300–453" });
            journals.Add(new Journal { Articles = article2, Name = "Journal of Abnormal Psychology", Date = new DateTime(1991, 1, 1), NumberIssue = "100(3)" });

            List<Article> article3 = new List<Article>();
            article3.Add(new Article { Author = new List<Author>() { ListAuthor[22] }, Title = "На пути к материалистическому решению психофизической проблемы.", Location = "110–121" });
            article3.Add(new Article { Author = new List<Author>() { ListAuthor[22] }, Title = "На пути к материалистическому решению психофизической проблемы. 2", Location = "110–121" });
            journals.Add(new Journal { Articles = article3, Name = "От дуализма Декарта к монизму Спинозы. Вопросы философии", Date = new DateTime(2010, 1, 1), NumberIssue = "No. 10" });

            return journals;
        }

        public List<Newspaper> FillTheListNewspaper()
        {
            List<Newspaper> newspapers = new List<Newspaper>();

            List<Article> article = new List<Article>();
            article.Add(new Article { Author = new List<Author>() { ListAuthor[23] }, Title = "Молодые леса зелены", Location = "8" });
            article.Add(new Article { Author = new List<Author>() { ListAuthor[23] }, Title = "Молодые леса зелены 2", Location = "8" });
            newspapers.Add(new Newspaper { Articles = article, Name = "Рэспубліка.", Date = new DateTime(2005, 2, 19) });

            List<Article> article1 = new List<Article>();
            article1.Add(new Article { Author = new List<Author>() { ListAuthor[24] }, Title = "Будем читать. Глядишь, и кризис пройдет…", Location = "9" });
            article1.Add(new Article { Author = new List<Author>() { ListAuthor[24] }, Title = "Будем читать. Глядишь, и кризис пройдет… 2", Location = "9" });
            newspapers.Add(new Newspaper { Articles = article1, Name = "Северный комсомолец.", Date = new DateTime(2009, 4, 13) });

            List<Article> article2 = new List<Article>();
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[25] }, Title = "Приоритет – экология", Location = "13-14" });
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[25] }, Title = "Приоритет – экология 2", Location = "13-14" });
            newspapers.Add(new Newspaper { Articles = article2, Name = "Волна.", Date = new DateTime(2004, 4, 4) });

            List<Article> article3 = new List<Article>();
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[26] }, Title = "Приоритет – экология 3", Location = "13-14" });
            article2.Add(new Article { Author = new List<Author>() { ListAuthor[26] }, Title = "Приоритет – экология 4", Location = "13-14" });
            newspapers.Add(new Newspaper { Articles = article2, Name = "Волна.", Date = new DateTime(2004, 4, 4) });

            return newspapers;
        }
    }
}