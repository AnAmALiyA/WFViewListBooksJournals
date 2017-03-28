using System;
using System.Collections.Generic;

namespace WFViewListBooksJournals.Views.ModelView
{
    public class JournalView
    {
        public List<ArticleView> Articles { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string NumberIssue { get; set; }
    }
}