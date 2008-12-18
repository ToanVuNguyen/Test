using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Web
{
    public partial class Default : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
                        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var dto=new CallLogDTO {ExtCallNumber = string.Empty, StartDate = DateTime.Today.AddDays(-61)};
            //var results = HPFValidator.Validate<CallLogDTO>(dto);
            //foreach (var result in results)
            //{
            //    Response.Write(result.Message);
            //}

        }              
    }   
}
