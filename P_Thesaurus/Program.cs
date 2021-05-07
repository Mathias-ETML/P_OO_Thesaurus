using P_Thesaurus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.Controllers;

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

            /*
            History<FtpHistoryEntry> history = new History<FtpHistoryEntry>("./coucou.txt");

            history.AddEntry(new FtpHistoryEntry() {
                Content = "Bonjour",
                DateTime = DateTime.Now.Date,
                Username = "buz",
                Password = "12345"
            });

            history.Write();

            FtpHistoryEntry he = history.Read()[0];
            */

            MainController mainController = new MainController();
            
            mainController.Launch();
            mainController.Dispose();

            //MainController instance = (MainController)Activator.CreateInstance(typeof(MainController));

            //Application.Run(new FolderNavigation());
        }
    }
}
