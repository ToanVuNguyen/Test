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

using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Web.CodeMaintenance
{
    public partial class CodeMaintenanceEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (!IsPostBack)            
                InitializeData();                
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_CODE_MAINTENTANCE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }            
        }

        private void InitializeData()
        {
            string refCodeItemId = Request.QueryString["refCodeItem"];

            drpCodeSet.DataSource = LookupDataBL.Instance.GetRefCodeSet();
            drpCodeSet.DataTextField = "RefCodeSetName";
            drpCodeSet.DataValueField = "RefCodeSetName";
            drpCodeSet.DataBind();
            drpCodeSet.Items.Insert(0, "");
            drpActiveInd.SelectedIndex = drpActiveInd.Items.IndexOf(drpActiveInd.Items.FindByValue(Constant.INDICATOR_YES));
            if (!string.IsNullOrEmpty(refCodeItemId))
            {
                RefCodeSetDTOCollection codeSet = LookupDataBL.Instance.GetRefCodeSet();
                RefCodeItemDTO codeItem = RefCodeItemBL.Instance.GetRefCodeItem(int.Parse(refCodeItemId));

                txtRefCodeItemId.Value = refCodeItemId;
                drpCodeSet.SelectedIndex = drpCodeSet.Items.IndexOf(drpCodeSet.Items.FindByValue(codeItem.RefCodeSetName));
                txtCode.Text = codeItem.CodeValue;
                txtCodeDescription.Text = codeItem.CodeDescription;
                txtCodeComment.Text = codeItem.CodeComment;
                txtSortOrder.Text = (codeItem.SortOrder.HasValue) ? codeItem.SortOrder.Value.ToString() : "";
                if (!string.IsNullOrEmpty(codeItem.ActiveInd))
                    drpActiveInd.SelectedIndex = drpActiveInd.Items.IndexOf(drpActiveInd.Items.FindByValue(codeItem.ActiveInd));
            }
        }
        protected void bntSave_Click(object sender, EventArgs e)
        {
            bool saveSucces = false;
            try
            {
                int sortOrder = 0;
                RefCodeItemDTO refCode = new RefCodeItemDTO();
                refCode.ActiveInd = (drpActiveInd.SelectedIndex >= 0) ? drpActiveInd.SelectedValue : null;
                refCode.CodeComment = string.IsNullOrEmpty(txtCodeComment.Text.Trim()) ? null : txtCodeComment.Text.Trim();
                refCode.CodeDescription = string.IsNullOrEmpty(txtCodeDescription.Text.Trim()) ? null : txtCodeDescription.Text.Trim();
                refCode.CodeValue = string.IsNullOrEmpty(txtCode.Text.Trim()) ? null : txtCode.Text.Trim().ToUpper();
                if (!string.IsNullOrEmpty(txtRefCodeItemId.Value))
                    refCode.RefCodeItemId = int.Parse(txtRefCodeItemId.Value);
                refCode.RefCodeSetName = (drpCodeSet.SelectedIndex > 0) ? drpCodeSet.SelectedValue: null;
                refCode.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                if (int.TryParse(txtSortOrder.Text.Trim(), out sortOrder))
                    refCode.SortOrder = sortOrder;

                RefCodeItemBL.Instance.SaveRefCodeItem(refCode);
                saveSucces = true;
            }
            catch (DataValidationException dataEx)
            {
                lblErrorMessage.DataSource = dataEx.ExceptionMessages;
                lblErrorMessage.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
            }
            if (saveSucces)
                Response.Redirect("CodeMaintenance.aspx");
        }

        protected void bntCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodeMaintenance.aspx");
        }
    }
}