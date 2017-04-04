namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IMainForm
    {
        void InitializeComponentMainForm();
        void ClearListBoxMain();
        void FillListBoxMain(string publication);
        void FillListBoxMain(string author, string nameBook, string year, int pages);
        void FillListBoxMain(string author, string articleTitle, string publication, string year, string numberIssu, string locationArticle);
        void FillListBoxMain(string author, string title, string location);
        void ClearComboBoxAuthors();
        void ClearComboBoxPublications();
        void FillComboBoxAuthors(string[] authors);
        void FillComboBoxPublications(string[] publications);
    }
}
