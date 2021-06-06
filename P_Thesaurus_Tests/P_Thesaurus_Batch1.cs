using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Controllers;
using P_Thesaurus.Views;

namespace P_Thesaurus_Tests
{
    [TestClass]
    public class P_Thesaurus_Batch1
    {
        /// <summary>
        /// First test method
        /// </summary>
        [TestMethod]
        public void TestJsonReader()
        {
            FolderController flc = new FolderController();

            const string EXCEPTED = "exceptedResult";

            // write in history
            flc.WriteInHistory(EXCEPTED);

            // get first entry
            HistoryEntry entry = flc.GetHistory()[0];

            // check if the writed string is the same
            Assert.AreEqual(EXCEPTED, entry.Content);
        }

        /// <summary>
        /// Second test method
        /// </summary>
        [TestMethod]
        public void TestMainController()
        {
            MainController mc = new MainController();

            // setting up the flags for getting the private array in the list
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            // get the child controller
            IController ctrl = (IController)typeof(MainController).GetField("_childController", bindFlags).GetValue(mc);

            // check if the view in the first child controller is the launching view
            Assert.AreEqual(ctrl.View.GetType(), typeof(LaunchingView));
        }

        /// <summary>
        /// Third test method
        /// </summary>
        [TestMethod]
        public void TestScan()
        {
            FolderController fc = new FolderController();

            Folder root = fc.GetFolder("c");

            // scan
            fc.StartScan(ref root);

            // check if the C drive root got a windows folder, wich is always true
            Assert.IsTrue(root.Folders.Find(item => item.Name == "Windows") != null);

            // check if the root is a root folder, because we got the C drive as a starting point
            Assert.IsTrue(root.IsRootFolder);
        }
    }
}
