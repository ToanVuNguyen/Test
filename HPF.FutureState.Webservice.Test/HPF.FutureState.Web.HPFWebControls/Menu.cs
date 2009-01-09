using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Web.HPFWebControls
{
    [Serializable]
    public class Menu : BaseMenu
    {
        public void AddMenuItem(MenuItem item)
        {
            AddMenu(item);
        }

        public void AddMenuItem(string id, string title, string url)
        {
            AddMenu(new MenuItem {Id = Id, Title = title, Url = url});
        }
    }
}
