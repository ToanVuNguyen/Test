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


using System.Collections.ObjectModel;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;

using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class Outcome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                ApplySecurity();
                grdvOutcomeItemsBinding();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }

        }

        private void grdvOutcomeItemsBinding()
        {
            int caseID = int.Parse(Request.QueryString["CaseID"].ToString());
            OutcomeItemDTOCollection outcomeItems = RetrieveOutcomeItems(caseID);
            if (outcomeItems != null)
            {
                grdvOutcomeItems.DataSource = outcomeItems;
                grdvOutcomeItems.DataBind();
            }
        }

        protected void grvForeClosureCaseSearch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int idxIdColumn = 0;
            e.Row.Cells[idxIdColumn].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onclick", "this.className='SelectedRowStyle'");
                //if (e.Row.RowState == DataControlRowState.Alternate)
                //{
                //    e.Row.Attributes.Add("ondblclick", "this.className='AlternatingRowStyle'");
                //}
                //else
                //{
                //    e.Row.Attributes.Add("ondblclick", "this.className='RowStyle'");
                //}
            }       
        }

        protected void grdvOutcomeItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOutcomeDeletedDt = (Label)e.Row.FindControl("lblOutcomeDeletedDt");
                DateTime dt;
                DateTime.TryParse(lblOutcomeDeletedDt.Text, out dt);
                if (dt == null || dt == DateTime.MinValue)
                    lblOutcomeDeletedDt.Text = "";
                

            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedIdx = grdvOutcomeItems.SelectedIndex;            
            if (selectedIdx > -1)
            {
                string s = ((Label)grdvOutcomeItems.Rows[selectedIdx].FindControl("lblOutcomeDeletedDt")).Text;
                if (s == null || s == string.Empty)
                {
                    int outcomeId = 0;
                    int.TryParse(grdvOutcomeItems.SelectedDataKey.Value.ToString(), out outcomeId);
                    OutcomeItemBL.Instance.DeleteOutcomeItem(outcomeId, HPFWebSecurity.CurrentIdentity.LoginName);
                    grdvOutcomeItemsBinding();                    
                }
                else
                    errorList.Items.Add(ErrorMessages.ERR600);               
            }            
        }

        protected void btnReinstate_Click(object sender, EventArgs e)
        {
            int selectedIdx = grdvOutcomeItems.SelectedIndex;
            if (selectedIdx > -1)
            {
                string s = ((Label)grdvOutcomeItems.Rows[selectedIdx].FindControl("lblOutcomeDeletedDt")).Text;
                if (s == null || s == string.Empty)
                {
                    errorList.Items.Add(ErrorMessages.ERR601);
                }
                else
                {
                    int outcomeId = 0;
                    int.TryParse(grdvOutcomeItems.SelectedDataKey.Value.ToString(), out outcomeId);
                    OutcomeItemBL.Instance.InstateOutcomeItem( outcomeId, HPFWebSecurity.CurrentIdentity.LoginName);
                    grdvOutcomeItemsBinding();
                }
                    
            }   
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                btnDelete.Enabled = false;
                btnReinstate.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnReinstate.Enabled = true;
            }
        }
        private OutcomeItemDTOCollection RetrieveOutcomeItems(int fcid)
        {
            return OutcomeItemBL.Instance.RetrieveOutcomeItems(fcid);
        }

        
       
       

    }
}