using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

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
            XDocument doc = XDocument.Load(xmlFile);
            var menus = from menu in doc.Descendants("Menu")
                        select new Menu
                        {
                            Id = menu.Attribute("Id").Value,
                            Title = menu.Attribute("Title").Value,
                            Url = menu.Attribute("Url").Value,
                            Enabled = bool.Parse(menu.Attribute("Enabled").Value)
                        };
            foreach (var menu in menus)
            {
                var childs = from child in doc.Descendants("MenuItem")
                             where child.Parent.Attribute("Id").Value == menu.Id
                             select new MenuItem
                             {
                                 Id = child.Attribute("Id").Value,
                                 Title = child.Attribute("Title").Value,
                                 Url = child.Attribute("Url").Value,
                                 Enabled = bool.Parse(child.Attribute("Enabled").Value)
                             };
                foreach (MenuItem menuItem in childs)
                    menu.AddMenuItem(menuItem);
                AddMenu(menu);
            }
            
            
        }

        /// <summary>
        /// Load menu from database
        /// </summary>
        public void LoadFromDatabase()
        {
            
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
