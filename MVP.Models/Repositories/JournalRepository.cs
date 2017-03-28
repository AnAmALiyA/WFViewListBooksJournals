using System.IO;
using System.Text;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;
using WFViewListBooksJournals.Models.Infrastructure;

namespace WFViewListBooksJournals.Models.Repositories
{
    public class JournalRepository
    {
        public List<Journal> ListEntities { get; set; }

        public JournalRepository(AllLiterary allLiterary)
        {
            ListEntities = allLiterary.FillTheListJournal();
        }

        public void SaveTXT()
        {
            FileInfo fileInfo = new FileInfo("Test.txt");

            if (fileInfo.Exists == true)
            {
                fileInfo.Delete();
            }

            FileStream file = new FileStream("Test.txt", FileMode.Create, FileAccess.ReadWrite);

            StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding(1251));

            foreach (Journal journal in ListEntities)
            {
                writer.WriteLine("Name={0}, Date={1}, Number Issue={2}", journal.Name, journal.Date.ToString("D"), journal.NumberIssue);
                foreach (Article article in journal.Articles)
                {
                    writer.WriteLine("\tAuthor={0}, Title={1}, Location={2}", article.Author.GetStringAuthors(), article.Title, article.Location);
                }
                writer.WriteLine();
            }
            writer.Close();
        }
    }
}
