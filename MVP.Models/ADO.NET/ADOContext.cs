using System;
using System.Data;
//using System.Configuration;
using System.Data.SqlClient;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.ADO.NET
{
    public class ADOContext
    {
        private string ConnectionString { get; set; }
        private SqlConnection Connection { get; set; }
        private string[] CommandString { get; set; }
        private string[] TableFields { get; set; }
        SqlDataAdapter adapter;

        public ADOContext(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);
            CommandString = new string[] {
                "SELECT * FROM Authors",
                "SELECT * FROM Books",
                "INSERT Authors VALUES (@FirstName, @SecondName, @LastName, @Age, @InitialsOption, @BookId)",
                "INSERT Books VALUES (@Name, @Date, @Pages, @AuthorId)",
                "SELECT Id FROM Authors WHERE FirstName = '{0}' AND SecondName = '{1}' AND LastName = '{2}' AND Age = {3} AND InitialsOption = {4}",
                "SELECT Id FROM Books WHERE Name = '{0}' AND Date = '{1}' AND Pages = {2}",
                "INSERT ManyBooksToManyAuthors VALUES (@BookId, @AuthorId)",
                "DELETE Authors",
                "DELETE Books",
                "DELETE ManyBooksToManyAuthors"
            };
            TableFields = new string[] { "Authors", "Books", "Id" , "FirstName", "SecondName",
                                         "LastName", "Age", "InitialsOption", "BookId", "Name",
                                         "Date", "Pages", "AuthorId" };
            
    }

        public DataTable GetTableAuthors()
        {
            DataTable table = new DataTable(TableFields[0]);
            adapter = new SqlDataAdapter(CommandString[0], ConnectionString);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            adapter.FillSchema(table, SchemaType.Mapped);
            table.Columns[TableFields[2]].AutoIncrementSeed = (int)EnumSystem.AutoIncrementSeed;
            table.Columns[TableFields[2]].AutoIncrementStep = (int)EnumSystem.AutoIncrementStep;

            adapter.Fill(table);
            return table;
        }

        public void InsertAuthors(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    InsertAuthor(row);
                }
            }
            table.AcceptChanges();
        }

        private void InsertAuthor(DataRow row)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(CommandString[2], Connection);
            cmd.Parameters.AddWithValue(TableFields[3], row[1]);
            cmd.Parameters.AddWithValue(TableFields[4], row[2]);
            cmd.Parameters.AddWithValue(TableFields[5], row[3]);
            cmd.Parameters.AddWithValue(TableFields[6], row[4]);
            cmd.Parameters.AddWithValue(TableFields[7], row[5]);
            cmd.Parameters.AddWithValue(TableFields[8], DBNull.Value);

            cmd.ExecuteNonQuery();
            Connection.Close();
        }
                
        public int FindAuthor(string firstName, string secondName, string lastName, int age, bool initialsOption)
        {
            string commandString = String.Format(CommandString[4], firstName, secondName, lastName, age, GetIdentifyBit(initialsOption));

            Connection.Open();

            SqlCommand cmd = new SqlCommand(commandString, Connection);
            
            int temp = (int)cmd.ExecuteScalar();
            Connection.Close();

            return temp;            
        }

        private int GetIdentifyBit(bool initialsOption)
        {
            if (initialsOption)
            {
                return (int)EnumSystem.BitTrue;
            }
            return (int)EnumSystem.BitFalse;
        }

        public DataTable GetTableBooks()
        {
            DataTable table = new DataTable(TableFields[1]);
            adapter = new SqlDataAdapter(CommandString[1], ConnectionString);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            adapter.FillSchema(table, SchemaType.Mapped);
            table.Columns[TableFields[2]].AutoIncrementSeed = (int)EnumSystem.AutoIncrementSeed;
            table.Columns[TableFields[2]].AutoIncrementStep = (int)EnumSystem.AutoIncrementStep;

            adapter.Fill(table);
            return table;
        }

        public void InsertBooks(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    InsertBook(row);
                }
            }
            table.AcceptChanges();
        }

        private void InsertBook(DataRow row)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(CommandString[3], Connection);
            cmd.Parameters.AddWithValue(TableFields[9], row[1]);
            cmd.Parameters.AddWithValue(TableFields[10], row[2]);
            cmd.Parameters.AddWithValue(TableFields[11], row[3]);
            cmd.Parameters.AddWithValue(TableFields[12], DBNull.Value);

            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public int FindBook(string name, DateTime date,  int pages)
        {
            string commandString = String.Format(CommandString[5], name, date, pages);

            Connection.Open();

            SqlCommand cmd = new SqlCommand(commandString, Connection);
            int temp = (int)cmd.ExecuteScalar();
            
            Connection.Close();

            return temp;
        }

        public void InsertManyBooksToManyAuthors(int bookId, int authorId)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand(CommandString[6], Connection);
            cmd.Parameters.AddWithValue(TableFields[8], bookId);
            cmd.Parameters.AddWithValue(TableFields[12], authorId);
            
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public void DeleteAuthors()
        {
            DeleteTable(CommandString[7]);
        }

        public void DeleteBooks()
        {
            DeleteTable(CommandString[8]);
        }

        public void DeleteManyBooksToManyAuthors()
        {
            DeleteTable(CommandString[9]);
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
