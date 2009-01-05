using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HPF.FutureState.Web.HPFWebControls
{
    public class MenuBar: IEnumerable<Menu>
    {
        private readonly Dictionary<string, Menu> _MenuList;

        public MenuBar()
        {
            _MenuList = new Dictionary<string, Menu>();
        }

        public void AddMenu(Menu menu)
        {
            _MenuList.Add(menu.Id, menu);
        }

        public void RemoveMenu(string id)
        {
            _MenuList.Remove(id);
        }        

        /// <summary>
        /// Load menu structure from XmlFile
        /// </summary>
        /// <param name="xmlFile"></param>
        public void Load(string xmlFile)
        {
            //XDocument doc = XDocument.Load("MenuExample.xml");
            //var menubar = doc.Root;
            //foreach (var i in menubar)
            //{ 
                
            //}
            Menu Home = new Menu { Id = "Home", Title = "Home", Url = "", Enabled = true };
            AddMenu(Home);
            Menu Accounting = new Menu { Id = "Accounting", Title = "Accounting", Url = "#", Enabled = true };
            Accounting.AddMenuItem(new MenuItem { Id = "Siny", Title = "Siny", Url = "#", Enabled = true });
            Accounting.AddMenuItem(new MenuItem { Id = "Siny1", Title = "Siny1", Url = "#", Enabled = false });
            AddMenu(Accounting);
        }

        /// <summary>
        /// Enabled Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void EnabledMenu(string id)
        {
            foreach (var m in _MenuList)
                if (m.Key == id)
                    m.Value.Enabled = true;
        }

        /// <summary>
        /// Disable Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void DisabledMenu(string id)
        {
            foreach(var m in _MenuList)
                if(m.Key==id)
                    m.Value.Enabled=false;
        }

        #region IEnumerable<Menu> Members

        public IEnumerator<Menu> GetEnumerator()
        {
            foreach (var menu in _MenuList)
            {
                yield return menu.Value;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var menu in _MenuList)
            {
                yield return menu.Value;
            }
        }

        #endregion
    }
}
