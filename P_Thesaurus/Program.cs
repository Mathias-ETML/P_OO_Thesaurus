using P_Thesaurus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.HistoryReader;

namespace P_Thesaurus
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            History<HistoryEntry> history = new History<HistoryEntry>("./coucou.txt");

            history.AddEntry(new HistoryEntry() {
                Content = "Bonjour",
                DateTime = DateTime.Now
            });

            history.AddEntry(new HistoryEntry()
            {
                Content = "Clément",
                DateTime = DateTime.Now
            });

            history.Write();

            HistoryEntry he = history.Read()[0];

            while (true)
            {

            }

            //Application.Run(new FolderNavigation());
        }
    }
}
