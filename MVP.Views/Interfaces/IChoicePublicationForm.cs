using System.Windows.Forms;
using WFViewListBooksJournals.Views.Infrastructure;
using WFViewListBooksJournals.Views.Infrastructure.Mapping;
using WFViewListBooksJournals.Entities;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IChoicePublicationForm
    {
        dynamic SelectedValue { get; set; }
        int SelectedValueDelete { get; set; }
        int SelectedIndexListBox { get; set; }

        string Text { get; set; }
        ComboBox ComboBoxAuthor { get; set; }
        ListBox ListBox1Form2 { get; set; }

        DataDisplay DataDisplay { get; set; }
        MappingEntity Mapping { get; set; }

        void AdjustDisplayBooks();
        void AdjustDisplayJournals();
        void AdjustDisplayNewspapers();

        void FillDataMainListBox();

        void ShowForm();

        void ClearFields();
        void InsertFields(Book book);
        void InsertFields(Journal book);
        void InsertFields(Newspaper book);

        void ClearComboBoxAuthor();
    }
}
