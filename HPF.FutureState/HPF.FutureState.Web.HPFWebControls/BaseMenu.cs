using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Web.HPFWebControls
{
    [Serializable]
    public abstract class BaseMenu : IEnumerable<BaseMenu>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private readonly Dictionary<string, BaseMenu> _ChildMenus;

        protected BaseMenu()
        {
            _ChildMenus = new Dictionary<string, BaseMenu>();
        }

        protected void AddMenu(BaseMenu menu)
        {
            _ChildMenus.Add(menu.Id, menu);
        }

        public void RemoveMenu(string id)
        {
            _ChildMenus.Remove(id);
        }
        public int Count
        {
            get { return _ChildMenus.Count; }
        }
        /// <summary>
        /// Find a child menu by menuId
        /// </summary>
        /// <param name="id">Menu Id</param>
        /// <returns>A Menu</returns>
        public BaseMenu FindMenu(string id)
        {
            return _ChildMenus.ContainsKey(id) ? _ChildMenus[id] : null;
        }

        #region IEnumerable<BaseMenu> Members

        public IEnumerator<BaseMenu> GetEnumerator()
        {
            foreach (var menu in _ChildMenus)
            {
                yield return menu.Value;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var menu in _ChildMenus)
            {
                yield return menu.Value;
            }
        }

        #endregion
    }
}
