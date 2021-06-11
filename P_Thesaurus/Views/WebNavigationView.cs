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
        /// The searched Words curently written in the Search Bar.
        /// Used in the Search Predicate
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

            filterChckdLstBox.Items.AddRange(Enum.GetNames(typeof(WebElementType)));

            //Variables initialization
            _filters = new List<WebElementType>();
            _searched = new List<WebResearchElement>();
            _elements = new List<WebElement>();
        }

        /// <summary>
        /// Initialize the Elements in the navigation
        /// </summary>
        /// <param name="datas">the links to display</param>
        public void InitializeElements(List<WebElement> datas, string url)
        {
            txbUrl.Text = url;

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
        private void ListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(_listView.SelectedItems.Count > 0)
            {
                //Get the WebElement From the ListViewItem, then send it to Ctrlr
                Controller.TestUrl(new WebElement()
                {
                    Link = _listView.SelectedItems[0].SubItems[0].Text,
                    Type = (WebElementType)Enum.Parse(typeof(WebElementType), _listView.SelectedItems[0].SubItems[1].Text)
                });
            }
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

                //the default modifier is "Or", for the spaces and the first word
                SearchModifier next = SearchModifier.Or;

                //extracts the search elements from list
                foreach(string word in searchSpaceSplitted)
                {
                    if(word == "+")
                    {
                        next = SearchModifier.And;
                    }
                    else if(word.StartsWith("+"))
                    {
                        _searched.Add(new WebResearchElement { Word = word.Substring(1), Modifier = SearchModifier.And });
                    }
                    else if(word == "-")
                    {
                        next = SearchModifier.Not;
                    }
                    else if(word.StartsWith("-"))
                    {
                        _searched.Add(new WebResearchElement { Word = word.Substring(1), Modifier = SearchModifier.Not });
                    }
                    else
                    {
                        _searched.Add(new WebResearchElement { Word = word, Modifier = next });
                        next = SearchModifier.Or;
                    }
                    
                }
            }

            ActualizeListView();
        }

        /// <summary>
        /// Just send the url entered in the url textbox when enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbUrlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Controller.TestUrl(txbUrl.Text);
            }
        }

        /// <summary>
        /// Sends the closing to the Controller because he needs to know it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebNavigationViewFormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.NotifyNavigationClosing();
        }
        #endregion

        #region Private Business Methods
        /// <summary>
        /// Actualize the list view items with the current elements
        /// </summary>
        private void ActualizeListView()
        {
            _listView.Items.Clear();

            List<WebElement> searchedElements = _elements.FindAll(Search);

            foreach (WebElement element in searchedElements)
            {
                if (_filters.Count > 0 && ! _filters.Contains(element.Type))
                {
                    continue;
                }
                _listView.Items.Add(new ListViewItem(new string[] { element.Link, element.Type.ToString() }));
            }
        }

        /// <summary>
        /// Predicate that 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool Search(WebElement element)
        {
            //if there is no search
            if(_searched.Count == 0)
            {
                return true;
            }

            bool orValue = false;

            //Checks the compatibility to all the parameters in the list
            foreach(WebResearchElement searchElement in _searched)
            {
                switch(searchElement.Modifier)
                {
                    case SearchModifier.Or:
                        {
                            if(element.Link.Contains(searchElement.Word))
                            {
                                orValue = true;
                            }
                            break;
                        }
                    case SearchModifier.And:
                        {
                            if ( ! element.Link.Contains(searchElement.Word))
                            {
                                return false;
                            }
                            break;
                        }
                    case SearchModifier.Not:
                        {
                            if (element.Link.Contains(searchElement.Word))
                            {
                                return false;
                            }
                            break;
                        }
                }
            }

            return orValue;
        }

        #endregion
    }
}
