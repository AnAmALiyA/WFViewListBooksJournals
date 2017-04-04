using System;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IPublicationForm
    {
        void InitializeComponentPublicationForm();
        void ClearComboBoxAuthors();
        void FillComboBoxAuthors(string[] arrayAuthor);
        void ClearListBoxMain();
        void FillListBoxMain(string publication);
        void FillListBoxMain(string author, string nameBook, string year, int pages);
        void FillListBoxMain(string author, string articleTitle, string publication, string year, string numberIssu, string locationArticle);
        void FillListBoxMain(string author, string title, string location);
        void FillFields(int idAuthor, string name, DateTime date, int pages);
        void FillFields(int idAuthor, string title, string location, string name, DateTime date, string numberIssue);
        void FillFields(int idAuthor, string title, string location, string name, DateTime date);
    }
}
