using P_Thesaurus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.Controllers;
using P_Thesaurus.Models;

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
            History<HistoryEntry> history = new History<HistoryEntry>(FolderModel.DEFAULT_FOLDER_HISTORY_PATH);

            history.AddEntry(new HistoryEntry() {
                Content = @"F:\Projets\P_Theau\P_Thesaurus\P_Thesaurus",
                DateTime = DateTime.Now.Date,
            });

            history.Write();
            */
            

            /*
            History<FtpHistoryEntry> history = new History<FtpHistoryEntry>(FolderModel.DEFAULT_FOLDER_HISTORY_PATH);

            history.AddEntry(new FtpHistoryEntry()
            {
                Content = "C:\\",
                DateTime = DateTime.Now.Date,
                Password = "12345",
                Username = "Mathias"
            });

            history.Write();
            */

            MainController mainController = new MainController();
            
            mainController.Launch();
            mainController.Dispose();

            //MainController instance = (MainController)Activator.CreateInstance(typeof(MainController));

            //Application.Run(new FolderNavigation());
        }
    }
}
