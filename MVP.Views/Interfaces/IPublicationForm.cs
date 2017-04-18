using System;
using System.Collections.Generic;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IPublicationForm
    {
        void InitializeComponentPublicationForm();
        void ClearComboBoxAuthors();
        void FillOnlyComboBoxAuthors();
        void FillComboBoxAuthors(List<Author> authorList);
        void ClearListBoxMain();
        void FillListBoxMain(string publication);        
        void FillListBoxMain(List<Book> bookList);
        void FillListBoxMain(List<Journal> journalList);
        void FillListBoxMain(List<Newspaper> newspaperList);
    }
}
