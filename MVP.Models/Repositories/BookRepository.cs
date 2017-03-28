using System.Data;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.ADO.NET;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class BookRepository
    {
        public List<Book> ListEntities { get; set; }
        private AllLiterary AllLiterary { get; set; }
        private string[] TableFields { get; set; }

        public BookRepository(AllLiterary allLiterary)
        {
            ListEntities = allLiterary.FillTheListBook();
            AllLiterary = allLiterary;
            TableFields = new string[] { "Authors", "Books", "Id" , "FirstName", "SecondName", "LastName", "Age",
                "InitialsOption", "BookId", "AuthorId", "Name", "Date", "Pages" };
        }

        public void SaveDB(string connectionString)
        {
            ADOContext context = new ADOContext(connectionString);
            context.DeleteAuthors();
            context.DeleteBooks();
            context.DeleteManyBooksToManyAuthors();

            DataTable tableAuthors = context.GetTableAuthors();
            DataTable tableBooks = context.GetTableBooks();

            SaveListAuthor(context, tableAuthors, AllLiterary.ListAuthor, AllLiterary.NewListAuthor);
            SaveListBook(context, tableBooks, ListEntities);
        }

        private void SaveListAuthor(ADOContext context, DataTable table, List<Author> authors, List<Author> newAuthors)
        {
             foreach (Author author in authors)
            {
                AddRow(ref table, author);
            }

            foreach (Author author in newAuthors)
            {
                AddRow(ref table, author);
            }

            context.InsertAuthors(table);
        }

        private void AddRow(ref DataTable table, Author author)
        {
            DataRow newRow = table.NewRow();
            newRow[TableFields[3]] = author.FirstName;
            newRow[TableFields[4]] = author.SecondName;
            newRow[TableFields[5]] = author.LastName;
            newRow[TableFields[6]] = author.Age;
            newRow[TableFields[7]] = author.InitialsOption;

            try
            {
                table.Rows.Add(newRow);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        private void SaveListBook(ADOContext context, DataTable table, List<Book> books)
        {
            foreach (Book book in books)
            {
                DataRow newRow = table.NewRow();
                newRow[TableFields[10]] = book.Name;
                newRow[TableFields[11]] = book.Date;
                newRow[TableFields[12]] = book.Pages;

                try
                {
                    table.Rows.Add(newRow);
                }
                catch (System.Exception)
                {
                    throw;
                }

                context.InsertBooks(table);

                int bookId = context.FindBook(book.Name, book.Date, book.Pages);
                List<int> listAuthorId = GetListAuthor(context, book);

                foreach (int authorId in listAuthorId)
                {
                    context.InsertManyBooksToManyAuthors(bookId, authorId);
                }
            }
        }

        private List<int> GetListAuthor(ADOContext context, Book book)
        {
            List<int> listAuthor = new List<int>();

            int temp = (int)EnumSystem.NullAuthor;
            foreach (Author author in book.Authors)
            {
                temp = context.FindAuthor(author.FirstName, author.SecondName, author.LastName, author.Age, author.InitialsOption);
                if (temp != (int)EnumSystem.NullAuthor)
                {
                    listAuthor.Add(temp);
                }
            }
            return listAuthor;
        }
    }
}
