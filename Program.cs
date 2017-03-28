using System;
using System.Windows.Forms;
using WFViewListBooksJournals.Presenter.Infrastructure;
using WFViewListBooksJournals.View;

namespace WFViewListBooksJournals
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BussinesLogicPublications bussinesLogic = new BussinesLogicPublications();
            Application.Run(new MainForm(bussinesLogic));
        }
    }
}
