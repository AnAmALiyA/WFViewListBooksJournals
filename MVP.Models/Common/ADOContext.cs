using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.Services
{
    public class ADOContext
    {
        private string ConnectionString { get; set; }
        private SqlConnection Connection { get; set; }
        private Dictionary<string, string> CommandString { get; set; }
        private Dictionary<string, string> TableFields { get; set; }

        public ADOContext(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);

            CommandString = new Dictionary<string, string> {
                {"SelectAuthors", "SELECT * FROM Authors" },
                {"SelectBooks", "SELECT * FROM Books" },
                {"SelectAuthorsBooks", "SELECT * FROM AuthorsBooks"},
                {"InsertAuthors", "INSERT Authors VALUES (@FirstName, @SecondName, @LastName, @Age, @InitialsOption, @BookId)" },
                {"InsertBooks", "INSERT Books VALUES (@Name, @Date, @Pages, @AuthorId)" },
                {"SelectIdAuthors", "SELECT Id FROM Authors WHERE FirstName = '{0}' AND SecondName = '{1}' AND LastName = '{2}' AND Age = {3} AND InitialsOption = {4}" },                
                {"SelectIdBooks", "SELECT Id FROM Books WHERE Name = '{0}' AND Date = '{1}' AND Pages = {2}" },
                {"InsertAuthorsBooks", "INSERT AuthorsBooks VALUES (@BookId, @AuthorId)" },
                {"DeleteAuthors", "DELETE Authors" },
                {"DeleteBooks", "DELETE Books" },
                {"DeleteAuthorsBooks", "DELETE AuthorsBooks" },
                {"SelectIdAuthorsNullFild", "SELECT Id FROM Authors WHERE FirstName = '{0}' AND SecondName = '{1}' AND Age = {2} AND InitialsOption = {3}" }
            };

            TableFields = new Dictionary<string, string> {
                {"Authors", "Authors" },
                {"Books", "Books"},
                {"Id", "Id" },
                {"FirstName", "FirstName"},
                {"SecondName", "SecondName"},
                {"LastName", "LastName"},
                {"Age", "Age"},
                {"InitialsOption", "InitialsOption"},
                {"BookId", "BookId"},
                {"Name", "Name"},
                {"Date", "Date"},
                {"Pages", "Pages"},
                {"AuthorId", "AuthorId"}
            };
        }
        
        public void SaveBooks(List<Book> books, List<Author> authors)
        {
            Connection.Open();
            string sqlCommandString = string.Format(CommandString["SelectBooks"] + ";" + CommandString["SelectAuthors"] + ";" + CommandString["SelectAuthorsBooks"]);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommandString, Connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            Connection.Close();

            DataTable booksTable = dataSet.Tables[(int)SystemVariablesModel.BooksTable];
            DataTable authorsTable = dataSet.Tables[(int)SystemVariablesModel.AuthorsTable];
            
            SaveAuthors(authors, authorsTable);

            Connection.Open();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(dataSet);

            dataSet.Clear();

            adapter.Fill(dataSet);
            Connection.Close();
            
            booksTable = dataSet.Tables[(int)SystemVariablesModel.BooksTable];
            authorsTable = dataSet.Tables[(int)SystemVariablesModel.AuthorsTable];
            DataTable authorsBooksTable = dataSet.Tables[(int)SystemVariablesModel.AuthorsBooksTable];

            InsertAuthorsBooks(books, authorsBooksTable, adapter, dataSet);
            Connection.Close();
        }
        
        private void FillAuthorsBooksDataTable(List<Book> books, DataTable booksTable)
        {
            foreach (Book book in books)
            {
                DataRow newRow = booksTable.NewRow();
                newRow[TableFields["Name"]] = book.Name;
                newRow[TableFields["Date"]] = book.Date;
                newRow[TableFields["Pages"]] = book.Pages;

                try
                {
                    booksTable.Rows.Add(newRow);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        
        private void InsertAuthorsBooks(List<Book> books, DataTable tableAuthorsBooks, SqlDataAdapter adapter, DataSet dataSet)
        {
            foreach (Book book in books)
            {
                int bookId = FindBookId(book);
                if (bookId == (int) SystemVariablesModel.NoExist)
                {
                    continue;
                }

                List<int> listAuthorId = new List<int>();
                foreach (Author author in book.Authors)
                {
                    int authorId = FindAuthorId(author);
                    listAuthorId.Add(authorId);
                }

                FillAuthorsBooksDataTable(tableAuthorsBooks, bookId, listAuthorId, adapter, dataSet);
            }
        }

        private void FillAuthorsBooksDataTable(DataTable tableAuthorsBooks, int bookId, List<int> listAuthorId, SqlDataAdapter adapter, DataSet dataSet)
        {
            DataRow newRow = tableAuthorsBooks.NewRow();
            foreach (int authorId in listAuthorId)
            {
                newRow[TableFields["BookId"]] = bookId;
                newRow[TableFields["AuthorId"]] = authorId;
            }

            try
            {
                tableAuthorsBooks.Rows.Add(newRow);
            }
            catch (System.Exception)
            {
                throw;
            }

            Connection.Open();
            adapter.Update(dataSet);
            Connection.Close();
        }

        private void SaveAuthors(List<Author> authors, DataTable tableAuthors)
        {
            foreach (Author author in authors)
            {
                int index = FindAuthorId(author);

                if (index != (int)SystemVariablesModel.NoExist)
                {
                    continue;
                }

                DataRow newRow = tableAuthors.NewRow();
                newRow[TableFields["FirstName"]] = author.FirstName;
                newRow[TableFields["SecondName"]] = author.SecondName;
                newRow[TableFields["LastName"]] = author.LastName;
                newRow[TableFields["Age"]] = author.Age;
                newRow[TableFields["InitialsOption"]] = author.InitialsOption;

                try
                {
                    tableAuthors.Rows.Add(newRow);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        
        public int FindBookId(Book book)
        {
            string commandString = String.Format(CommandString["SelectIdBooks"], book.Name, book.Date, book.Pages);

            Connection.Open();

            SqlCommand command = new SqlCommand(commandString, Connection);

            var temp = command.ExecuteScalar();
            Connection.Close();

            if (temp != null)
            {
                int index = Int32.Parse(temp.ToString());
                return index;
            }
            return (int)SystemVariablesModel.NoExist;
        }

        public int FindAuthorId(Author author)
        {
            string commandString = string.Empty;
            if (author.LastName != null)
            {
                commandString = String.Format(CommandString["SelectIdAuthors"], author.FirstName, author.SecondName, author.LastName, author.Age, GetIdentifyBit(author.InitialsOption));
            }

            if (author.LastName == null)
            {
                commandString = String.Format(CommandString["SelectIdAuthorsNullFild"], author.FirstName, author.SecondName, author.Age, GetIdentifyBit(author.InitialsOption));
            }

            Connection.Open();

            SqlCommand cmd = new SqlCommand(commandString, Connection);
            
            var temp = cmd.ExecuteScalar();
            Connection.Close();
            
            if (temp != null)
            {
                int index = Int32.Parse(temp.ToString());
                return index;
            }
            return (int)SystemVariablesModel.NoExist;
        }

        private int GetIdentifyBit(bool initialsOption)
        {
            if (initialsOption)
            {
                return (int)SystemVariablesModel.BitTrue;
            }
            return (int)SystemVariablesModel.BitFalse;
        }
        
        public void DeleteAuthors()
        {
            DeleteDataInTable(CommandString["DeleteAuthors"]);
        }

        public void DeleteBooks()
        {
            DeleteDataInTable(CommandString["DeleteBooks"]);
        }

        public void DeleteBooksAuthors()
        {
            DeleteDataInTable(CommandString["DeleteAuthorsBooks"]);
        }

        private void DeleteDataInTable(string commandString)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(commandString, Connection);

            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
