using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomEditWebpart
{
    public partial class EditListFormCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {                   
        }

        public void setListId(Guid id)
        {
            myList.ListId = id;
        }
    }
}