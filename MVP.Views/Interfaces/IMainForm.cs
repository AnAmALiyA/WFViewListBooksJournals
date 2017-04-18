using System.Collections;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IMainForm
    {
        void InitializeComponentMainForm();
        ArrayList DataListBoxMain { get; set; }
        void ClearListBoxMain();
        void FillListBoxMain(string text);
        void FillListBoxMain(IEnumerable<Article> articles);
        void FillListBoxMain(List<string> publication, List<Book> bookList, List<Journal> journalList, List<Newspaper> newspaperList);
        void FillListBoxMainBooks(List<string> publication, IEnumerable<Book> bookList);
        void FillListBoxMainJournals(List<string> publication, IEnumerable<Journal> journalList);
        void FillListBoxMainNewspapers(List<string> publication, IEnumerable<Newspaper> newspaperList);
        void ClearComboBoxAuthors();
        void ClearComboBoxPublications();
        void FillComboBoxAuthors(List<Author> authorList);
        void FillComboBoxPublications(List<string> publications);
    }
}
