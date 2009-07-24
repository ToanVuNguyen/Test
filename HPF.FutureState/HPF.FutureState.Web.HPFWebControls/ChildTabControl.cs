using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF.FutureState.Web.HPFWebControls
{
    public class ChildTabControl: UserControl
    {
        public virtual bool IsModified
        {
            get
            {
                return false;
            }           
        }

        public virtual void SaveData()
        {
            throw new NotImplementedException("Not Implement yet.");
        }
    }
}
