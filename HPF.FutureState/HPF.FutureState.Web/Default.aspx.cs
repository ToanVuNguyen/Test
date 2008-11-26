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
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private string errors;

        protected void Page_Load(object sender, EventArgs e)
        {
                        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            errors = Label1.Text;
            var dto = new CallLogDTO {ExtCallNumber = "11111111111"};
            Validator<CallLogDTO> validator = ValidationFactory.CreateValidator<CallLogDTO>("Default Rule Set");
            var result = validator.Validate(dto);
            foreach (var validationResult in result)
            {                
                errors += validationResult.Message + "<br/>";
            }
        }
    }
}
