using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Views.ModelView;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Views.Infrastructure.Mapping
{
    public class MappingEntity
    {
        public BookView GetBook(Book book)
        {
            BookView tempBook = new BookView();

            tempBook.Author = book.Authors.GetStringAuthors();
            tempBook.Name = book.Name;
            tempBook.Date = book.Date;
            tempBook.Pages = book.Pages;

            return tempBook;
        }

        public List<AuthorView> GetListAuthor(List<Author> authors)
        {
            List<AuthorView> listAuthor = new List<AuthorView>();
            foreach (Author author in authors)
            {
                listAuthor.Add(new AuthorView() { FirstName = author.FirstName, SecondName = author.SecondName, LastName = author.LastName, Age = author.Age , InitialsOption = author.InitialsOption});
            }
            return listAuthor;
        }

        public ArticleView GetArticle(Article article)
        {
            ArticleView tempArticle = new ArticleView();

            tempArticle.Author = article.Author.GetStringAuthors();
            tempArticle.Title = article.Title;
            tempArticle.Location = article.Location;

            return tempArticle;
        }
        
        public List<ArticleView> GetListArticle(IEnumerable<Article> articles)
        {
            List<ArticleView> list = new List<ArticleView>();
            foreach (Article article in articles)
            {
               list.Add(GetArticle(article));
            }
            return list;
        }

        public JournalView GetJournal(Journal journal)
        {
            JournalView tempJournal = new JournalView();
            
            tempJournal.Articles = GetListArticle(journal.Articles);
            tempJournal.Name = journal.Name;
            tempJournal.NumberIssue = journal.NumberIssue;
            tempJournal.Date = journal.Date;

            return tempJournal;
        }

        public NewspaperView GetNewspaper(Newspaper newspaper)
        {
            NewspaperView tempNewspaper = new NewspaperView();
            
            tempNewspaper.Articles = GetListArticle(newspaper.Articles);
            tempNewspaper.Name = newspaper.Name;
            tempNewspaper.Date = newspaper.Date;

            return tempNewspaper;
        }
    }
}
