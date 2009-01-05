﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF.FutureState.Web.HPFWebControls
{
    [ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    public class TabControl : Control
    {
        public TabControl()
        { 
            _Tabs= new Collection<Tab>();
            ViewState["Tabs"] = _Tabs;
        }
        private Collection<Tab> _Tabs;
        /// <summary>
        /// Tab controls 
        /// </summary>
        
        public Collection<Tab> Tabs
        {
            get
            {
                Collection<Tab> tabs = (Collection<Tab>)ViewState["Tabs"];
                return tabs;
            }
        }
        public string SelectedTab
        {
            get { return (string)ViewState["Selected"]; }
            set { ViewState["Selected"]= value; }
        }
        /// <summary>
        /// Occurs when User click a tab 
        /// </summary>
        public event TabControlEventHandler TabClick;

        protected virtual void OnTabClick(TabControlEventArgs e)
        {
            if (TabClick != null)
                TabClick(this, e);
        }
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
                return;
            this.Controls.Clear();
            foreach (Tab i in Tabs)
            {
                LinkButton link = GetTab(i.ID, i.Title);
                if (ViewState["Selected"] != null)
                {
                    string selectedID = ViewState["Selected"] as string;
                    if(selectedID==i.ID)
                        link.Attributes.Add("class", "TabSelected");
                }
                this.Controls.Add(link);
            }
            //Add CSS to client page
            string includeTemplate ="<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";
            string includeLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "HPF.FutureState.Web.HPFWebControls.TabControl.css");
            LiteralControl include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);

            
        }
        
        /// <summary>
        /// Add a tab to TabControl 
        /// </summary>
        /// <param name="title">Tab Title</param>
        /// <param name="url">Tab URL</param>
        /// <param name="tabID">Tab ID</param>
        public void AddTab(string tabID,string title)
        {
            Tab tab = new Tab { Title = title, ID = tabID };
            LinkButton linkBtn = GetTab(tabID, title);
            _Tabs.Add(tab);
            this.Controls.Add(linkBtn);
            ViewState["Tabs"] = _Tabs;
        }

        private LinkButton GetTab(string tabID, string title)
        {
            LinkButton linkBtn = new LinkButton();
            linkBtn.Text = title;
            linkBtn.PostBackUrl = "#";
            linkBtn.Click += new EventHandler(linkBtn_Click);
            linkBtn.ID = tabID;
            linkBtn.Attributes.Add("class", "Tab");
            return linkBtn;
        }

        void linkBtn_Click(object sender, EventArgs e)
        {
            LinkButton selectedTab = (LinkButton)sender;
            selectedTab.Attributes.Add("class", "TabSelected");
            ViewState["Selected"] = selectedTab.ID;
            foreach (LinkButton t in this.Controls)
                if (t.ID != selectedTab.ID)
                    t.Attributes.Add("class", "Tab");
            //raise the event
            OnTabClick(new TabControlEventArgs { SelectedTabID = selectedTab.ID });
        }
        protected override void Render(HtmlTextWriter writer)
        {

            writer.Write("<table id='Container'>");
                writer.Write("<tr>");
                    foreach (var i in this.Controls)
                        if(i is LinkButton)
                        {
                            writer.Write("<td align='center'>");
                               ((LinkButton)i).RenderControl(writer);
                            writer.Write("</td>");
                        }
                writer.Write("</tr>");
                writer.Write("</table>");
        }
    }
    /// <summary>
    /// Represents the method that will handle any TabControl event.
    /// </summary>
    [Serializable]
    public delegate void TabControlEventHandler(object sender, TabControlEventArgs e);

    public class TabControlEventArgs : EventArgs
    {
        /// <summary>
        /// Get Selected Tab ID
        /// </summary>
        public string SelectedTabID { get; set; }
    }
    [Serializable]
    public class Tab
    {
        public string Title { get; set; }
        public string ID { get; set; }
    }
    
}
