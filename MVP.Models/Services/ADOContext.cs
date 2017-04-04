using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.Services
{
    public class ADOContext
    {
        private string ConnectionString { get; set; }
        private SqlConnection Connection { get; set; }
        private Dictionary<Enum, string> CommandString { get; set; }
        private Dictionary<Enum, string> TableFields { get; set; }
        SqlDataAdapter adapter;

        public ADOContext(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);

            CommandString = new Dictionary<Enum, string> {
                {EnumCommandString.SelectAuthors, "SELECT * FROM Authors" },
                {EnumCommandString.SelectBooks, "SELECT * FROM Books" },
                {EnumCommandString.InsertAuthors, "INSERT Authors VALUES (@FirstName, @SecondName, @LastName, @Age, @InitialsOption, @BookId)" },
                {EnumCommandString.InsertBooks, "INSERT Books VALUES (@Name, @Date, @Pages, @AuthorId)" },
                {EnumCommandString.SelectIdAuthors, "SELECT Id FROM Authors WHERE FirstName = '{0}' AND SecondName = '{1}' AND LastName = '{2}' AND Age = {3} AND InitialsOption = {4}" },                
                {EnumCommandString.SelectIdBooks, "SELECT Id FROM Books WHERE Name = '{0}' AND Date = '{1}' AND Pages = {2}" },
                {EnumCommandString.InsertAuthorsBooks, "INSERT AuthorsBooks VALUES (@BookId, @AuthorId)" },
                {EnumCommandString.DeleteAuthors, "DELETE Authors" },
                {EnumCommandString.DeleteBooks, "DELETE Books" },
                {EnumCommandString.DeleteAuthorsBooks, "DELETE AuthorsBooks" },
                {EnumCommandString.SelectIdAuthorsNullFild, "SELECT Id FROM Authors WHERE FirstName = '{0}' AND SecondName = '{1}' AND Age = {2} AND InitialsOption = {3}" }
            };

            TableFields = new Dictionary<Enum, string> {
                {EnumTableFields.Authors, "Authors" },
                {EnumTableFields.Books, "Books"},
                {EnumTableFields.Id, "Id" },
                {EnumTableFields.FirstName, "FirstName"},
                {EnumTableFields.SecondName, "SecondName"},
                {EnumTableFields.LastName, "LastName"},
                {EnumTableFields.Age, "Age"},
                {EnumTableFields.InitialsOption, "InitialsOption"},
                {EnumTableFields.BookId, "BookId"},
                {EnumTableFields.Name, "Name"},
                {EnumTableFields.Date, "Date"},
                {EnumTableFields.Pages, "Pages"},
                {EnumTableFields.AuthorId, "AuthorId"}
            };
        }
                
        public void SaveBooks(IEnumerable<Book> books)
        {
            DataTable tableBooks = GetTable(TableFields[EnumTableFields.Books], CommandString[EnumCommandString.SelectBooks]);
            DataTable tableAuthors = GetTable(TableFields[EnumTableFields.Authors], CommandString[EnumCommandString.SelectAuthors]);

            foreach (Book book in books)
            {
                DataRow newRow = tableBooks.NewRow();
                newRow[TableFields[EnumTableFields.Name]] = book.Name;
                newRow[TableFields[EnumTableFields.Date]] = book.Date;
                newRow[TableFields[EnumTableFields.Pages]] = book.Pages;
                
                SaveAuthors(book.Authors, tableAuthors);

                try
                {
                    tableBooks.Rows.Add(newRow);
                }
                catch (System.Exception)
                {
                    throw;
                }                
            }
            
            InsertBooks(tableBooks);
            InsertAuthors(tableAuthors);
            InsertAuthorsBooks(books);
        }
        private DataTable GetTable(string tableName, string selectCommandText)
        {
            DataTable table = new DataTable(tableName);
            adapter = new SqlDataAdapter(selectCommandText, ConnectionString);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            adapter.FillSchema(table, SchemaType.Mapped);
            table.Columns[TableFields[EnumTableFields.Id]].AutoIncrementSeed = (int)EnumSystem.AutoIncrementSeed;
            table.Columns[TableFields[EnumTableFields.Id]].AutoIncrementStep = (int)EnumSystem.AutoIncrementStep;

            adapter.Fill(table);
            return table;
        }

        private void SaveAuthors(ICollection<Author> authors, DataTable tableAuthors)
        {
            foreach (Author author in authors)
            {
                int index = FindAuthorId(author);

                if (index != (int)EnumSystem.NoExist)
                {
                    continue;
                }

                DataRow newRow = tableAuthors.NewRow();
                newRow[TableFields[EnumTableFields.FirstName]] = author.FirstName;
                newRow[TableFields[EnumTableFields.SecondName]] = author.SecondName;
                newRow[TableFields[EnumTableFields.LastName]] = author.LastName;
                newRow[TableFields[EnumTableFields.Age]] = author.Age;
                newRow[TableFields[EnumTableFields.InitialsOption]] = author.InitialsOption;

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

        public void InsertBooks(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    ExecuteQueryBook(row);
                }
            }
            table.AcceptChanges();
        }

        private void ExecuteQueryBook(DataRow row)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(CommandString[EnumCommandString.InsertBooks], Connection);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.Name], row[(int)EnumDataRow.Name]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.Date], row[(int)EnumDataRow.Date]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.Pages], row[(int)EnumDataRow.Pages]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.AuthorId], DBNull.Value);

            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public void InsertAuthors(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    ExecuteQueryAuthor(row);
                }
            }
            table.AcceptChanges();
        }

        private void ExecuteQueryAuthor(DataRow row)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(CommandString[EnumCommandString.InsertAuthors], Connection);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.FirstName], row[(int)EnumDataRow.FirstName]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.SecondName], row[(int)EnumDataRow.SecondName]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.LastName], row[(int)EnumDataRow.LastName]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.Age], row[(int)EnumDataRow.Age]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.InitialsOption], row[(int)EnumDataRow.InitialsOption]);
            cmd.Parameters.AddWithValue(TableFields[EnumTableFields.BookId], DBNull.Value);

            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        private void InsertAuthorsBooks(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                int bookId = FindBookId(book);

                List<int> listAuthorId = new List<int>();
                foreach (Author author in book.Authors)
                {
                    int authorId = FindAuthorId(author);
                    listAuthorId.Add(authorId);
                }
                InsertAuthorsBooks(bookId, listAuthorId);
            }
        }

        public int FindBookId(Book book)
        {
            string commandString = String.Format(CommandString[EnumCommandString.SelectIdBooks], book.Name, book.Date, book.Pages);

            Connection.Open();

            SqlCommand cmd = new SqlCommand(commandString, Connection);

            var temp = cmd.ExecuteScalar();
            Connection.Close();

            if (temp != null)
            {
                int index = Int32.Parse(temp.ToString());
                return index;
            }
            return (int)EnumSystem.NoExist;
        }

        public int FindAuthorId(Author author)
        {
            string commandString = string.Empty;
            if (author.LastName != null)
            {
                commandString = String.Format(CommandString[EnumCommandString.SelectIdAuthors], author.FirstName, author.SecondName, author.LastName, author.Age, GetIdentifyBit(author.InitialsOption));
            }

            if (author.LastName == null)
            {
                commandString = String.Format(CommandString[EnumCommandString.SelectIdAuthorsNullFild], author.FirstName, author.SecondName, author.Age, GetIdentifyBit(author.InitialsOption));
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
            return (int)EnumSystem.NoExist;
        }

        private int GetIdentifyBit(bool initialsOption)
        {
            if (initialsOption)
            {
                return (int)EnumSystem.BitTrue;
            }
            return (int)EnumSystem.BitFalse;
        }
        
        public void InsertAuthorsBooks(int bookId, List<int> listAuthorId)
        {
            foreach (int authorId in listAuthorId)
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand(CommandString[EnumCommandString.InsertAuthorsBooks], Connection);
                cmd.Parameters.AddWithValue(TableFields[EnumTableFields.BookId], bookId);
                cmd.Parameters.AddWithValue(TableFields[EnumTableFields.AuthorId], authorId);

                cmd.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public void DeleteAuthors()
        {
            DeleteTable(CommandString[EnumCommandString.DeleteAuthors]);
        }

        public void DeleteBooks()
        {
            DeleteTable(CommandString[EnumCommandString.DeleteBooks]);
        }

        public void DeleteBooksAuthors()
        {
            DeleteTable(CommandString[EnumCommandString.DeleteAuthorsBooks]);
        }

        private void DeleteTable(string commandString)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(commandString, Connection);

            cmd.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
