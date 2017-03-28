using WFViewListBooksJournals.Views.Infrastructure;
using WFViewListBooksJournals.Views.Infrastructure.Mapping;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IMainForm
    {
        DataDisplay DataDisplay { get; set; }
        MappingEntity Mapping { get; set; }

        void CreateErrorForm(int error);
    }
}
