using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Web.CodeMaintenance
{
    public partial class CodeMaintenance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (!IsPostBack)
            {
                SearchRefCodeItems(new RefCodeSearchCriteriaDTO());
                dropCodeSet.DataSource = LookupDataBL.Instance.GetRefCodeSet();
                dropCodeSet.DataTextField = "RefCodeSetName";
                dropCodeSet.DataBind();
                dropCodeSet.Items.Insert(0, "");                
            }
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_CODE_MAINTENTANCE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_CODE_MAINTENTANCE))
            {
                btnNewCode.Enabled = false;
            }
        }

        private void LoadRefCodesData(RefCodeItemDTOCollection data)
        {
            grvCodeMaintenance.DataSource = data;
            grvCodeMaintenance.DataBind();                
        }
        public void SearchRefCodeItems(RefCodeSearchCriteriaDTO criteria)
        {
            try
            {
                lblErrorMessage.Items.Clear();
                RefCodeItemDTOCollection results = RefCodeItemBL.Instance.GetRefCodeItems(criteria);
                LoadRefCodesData(results);
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                LoadRefCodesData(new RefCodeItemDTOCollection());
            }
            catch (Exception e)
            {
                lblErrorMessage.Items.Add(e.Message);
                LoadRefCodesData(new RefCodeItemDTOCollection());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RefCodeSearchCriteriaDTO critera = new RefCodeSearchCriteriaDTO();
            critera.IncludedInActive = bool.Parse(dropIncludeInactive.SelectedValue);
            critera.CodeSetName = (dropCodeSet.SelectedIndex <= 0) ? null : dropCodeSet.Text;
            SearchRefCodeItems(critera);
        }

        protected void btnCode_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Items.Clear();
            if (grvCodeMaintenance.SelectedValue == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1102));
                return;
            }
            int refCodeItemId = (int)grvCodeMaintenance.SelectedValue;
            Response.Redirect("CodeMaintenanceEdit.aspx?refCodeItem=" + refCodeItemId.ToString());
        }

        protected void btnNewCode_Click(object sender, EventArgs e)
        {                        
            Response.Redirect("CodeMaintenanceEdit.aspx");
        }
    }
}