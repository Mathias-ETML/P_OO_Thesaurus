/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans un site web
 */
using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.AppBusiness.WIN32;
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
        #region Variables
        /// <summary>
        /// the view's controller
        /// </summary>
        public WebController Controller { get; set; }

        /// <summary>
        /// The filters currently used in the View
        /// </summary>
        private List<WebElementType> _filters;

        /// <summary>
        /// The searched Words curently written in the Search Bar
        /// </summary>
        private List<WebResearchElement> _searched;

        /// <summary>
        /// All the elements found in the current url, 
        /// </summary>
        private List<WebElement> _elements;
        #endregion

        #region Public Methods
        /// <summary>
        /// default Controller
        /// </summary>
        public WebNavigationView()
        {
            InitializeComponent();

            //Some Forms things
            _listView.AllowColumnReorder = true;
            _listView.FullRowSelect = true;
            _listView.MultiSelect = false;
            _listView.View = View.Details;

            //Variables initialization
            _filters = new List<WebElementType>();
            _elements = new List<WebElement>();
        }

        /// <summary>
        /// Initialize the Elements in the navigation
        /// </summary>
        /// <param name="datas">the links to display</param>
        public void InitializeElements(List<WebElement> datas, string url)
        {
            lblCurrentUrl.Text = "URL actuelle : " + url;

            _elements = datas;

            ActualizeListView();
        }
        #endregion

        #region Private Form Methods
        /// <summary>
        /// Load the new url when the selected index changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _listView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Get the WebElement From the ListViewItem, then send it to Ctrlr
            Controller.TestUrl(new WebElement() 
            { 
                Link = _listView.SelectedItems[0].SubItems[0].Text,
                Type = (WebElementType)Enum.Parse(typeof(WebElementType), _listView.SelectedItems[0].SubItems[1].Text)
            });
        }

        /// <summary>
        /// Shows or not the filters when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltreClick(object sender, EventArgs e)
        {
            filterChckdLstBox.Visible = !filterChckdLstBox.Visible;
        }

        /// <summary>
        /// Occure when the user click on a filter from the list of filters to check
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterChckdLstBoxItemCheck(object sender, EventArgs e)
        {
            // yes, this is stupid
            // but we need to compare to file type
            WebElementType name = (WebElementType)Enum.Parse(typeof(WebElementType), filterChckdLstBox.SelectedItem.ToString(), true);

            if (_filters.Contains(name))
            {
                _filters.Remove(name);
            }
            else
            {
                _filters.Add(name);
            }

            //Re-Write all the elements, but with the right filters
            ActualizeListView();
        }

        /// <summary>
        /// When the user changes the text in the search bar, updates the searching elements (_searched) then actualizes
        /// Supports the modifiers +, - and the space is by default a "or" modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbSearchTextChanged(object sender, EventArgs e)
        {
            _searched.Clear();

            if(txbSearch.Text != "")
            {
                string[] searchSpaceSplitted = txbSearch.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                SearchModifier next = SearchModifier.Or;
                    
                //adds first the first word because he probably don't have modifier
                _searched.Add(new WebResearchElement { Word = searchSpaceSplitted[0], Modifier = SearchModifier.Or });

                //extracts the search elements from list
                foreach(string word in searchSpaceSplitted)
                {
                    if(word == "+")
                    {

                    }
                    else if(word.StartsWith("+"))
                    {
                        _searched.Add(new WebResearchElement { Word = word.Substring(1), Modifier = SearchModifier.And });
                    }
                    
                }
            }

            ActualizeListView();
        }
        #endregion

        #region Private Business Methods
        private void AddElement(WebElement element)
        {
            _listView.Items.Add(new ListViewItem(new string[] { element.Link, element.Type.ToString() }));
        }

        private void ActualizeListView()
        {
            _listView.Items.Clear();

            foreach (WebElement element in _elements)
            {
                /*if (_filters.Contains(element.Type) && element.Link.)
                {
                    AddElement(element);
                }*/
            }
        }

        /*private bool Search(WebElement element)
        {

        }*/
        #endregion
    }
}
