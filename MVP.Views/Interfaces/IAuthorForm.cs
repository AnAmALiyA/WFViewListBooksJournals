using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFViewListBooksJournals.Views.Interfaces
{
    public interface IAuthorForm
    {
        void InitializeComponentAuthorForm();
        void InitializeComponentAuthorForm(Dictionary<string, bool> nationality);
    }
}
