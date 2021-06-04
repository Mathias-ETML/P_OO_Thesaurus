/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans un site web
 */
using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.Controllers;
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
        }

        public void InitializeElements(List<WebElement> datas)
        {
            foreach(WebElement element in datas)
            {
                _listView.Items.Add(new ListViewItem(element.link, element.type.ToString()));
            }
        }
    }
}
