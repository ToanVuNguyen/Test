using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            
        }

        /// <summary>
        /// Enabled Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void EnabledMenu(string id)
        {
            
        }

        /// <summary>
        /// Disable Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void DisabledMenu(string id)
        {

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
