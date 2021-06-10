/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans un site web
 */
using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.Controllers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// Form permettant d'afficher la navigation dans un site web
    /// </summary>
    public partial class WebNavigationView : NavigationView
    {
        /// <summary>
        /// the view's controller
        /// </summary>
        public WebController Controller { get; set; }

        /// <summary>
        /// default Controller
        /// </summary>
        public WebNavigationView()
        {
            InitializeComponent();

            _listView.AllowColumnReorder = true;
            _listView.FullRowSelect = true;
            _listView.MultiSelect = false;
            _listView.View = View.Details;
        }

        /// <summary>
        /// Initialize the Elements in the navigation
        /// </summary>
        /// <param name="datas">the links to display</param>
        public void InitializeElements(List<WebElement> datas)
        {
            _listView.Items.Clear();

            foreach(WebElement element in datas)
            {
                _listView.Items.Add(new ListViewItem( new string[] { element.link, element.type.ToString() }));

                System.Diagnostics.Debug.WriteLine(element.type.ToString());
            }
        }

        private void _listView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Controller.TestUrl(new WebElement() 
            { 
                link = _listView.SelectedItems[0].SubItems[0].Text, type = (WebElementType)Enum.Parse(typeof(WebElementType), 
                _listView.SelectedItems[0].SubItems[1].Text)
            });
        }
    }
}
