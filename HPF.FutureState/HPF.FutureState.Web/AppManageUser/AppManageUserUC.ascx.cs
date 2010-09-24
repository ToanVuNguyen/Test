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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web.AppManageUser
{
    public partial class AppManageUserUC : System.Web.UI.UserControl
    {
        private const string SessionUserCollection = "UserCollection";
        private const string ActivateCommandText = "Activate";
        private const string DeactivateCommandText = "Deactivate";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session[SessionUserCollection] = HPFUserBL.Instance.RetrieveHpfUsers();
                    grdvHPFUserBinding();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName); 
            }
        }
        private void grdvHPFUserBinding()
        {
            HPFUserDTOCollection userCollection;
            if (Session[SessionUserCollection] != null)
            {
                userCollection = (HPFUserDTOCollection)Session[SessionUserCollection];
                grdvHPFUser.DataSource = userCollection;
                grdvHPFUser.DataBind();
            }
            else
            {
                userCollection = new HPFUserDTOCollection();
                grdvHPFUser.DataSource = userCollection;
                grdvHPFUser.DataBind();

                int totalColumns = grdvHPFUser.Rows[0].Cells.Count;
                grdvHPFUser.Rows[0].Cells.Clear();
                grdvHPFUser.Rows[0].Cells.Add(new TableCell());
                grdvHPFUser.Rows[0].Cells[0].ColumnSpan = totalColumns;
                grdvHPFUser.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void grdvHPFUser_RowCreated(object sender, GridViewRowEventArgs e)
        {
            LinkButton lbtnActivate = e.Row.FindControl("lbtnActivate") as LinkButton;
            HPFUserDTO userItem = e.Row.DataItem as HPFUserDTO;
            if (userItem == null) return;
            lbtnActivate.Text = (userItem.ActiveInd.ToUpper() == Constant.INDICATOR_YES ? DeactivateCommandText : ActivateCommandText);
        }

        protected void grdvHPFUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvHPFUser.EditIndex = e.NewEditIndex;
            grdvHPFUserBinding();
        }

        protected void grdvHPFUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvHPFUser.EditIndex = -1;
            grdvHPFUserBinding();
        }

        protected void grdvHPFUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                
                HPFUserDTO userItem = RowToHPFUserDTO(grdvHPFUser.Rows[e.RowIndex]);
                if (Session[SessionUserCollection] != null)
                {
                    HPFUserDTOCollection userItems = Session[SessionUserCollection] as HPFUserDTOCollection;
                    int index = userItems.ToList().FindIndex(item => item.HpfUserId == userItem.HpfUserId);
                    if (index >= 0)
                    {
                        userItem.ActiveInd = userItems[index].ActiveInd;
                        userItems[index] = userItem;
                        userItems[index].SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                        HPFUserBL.Instance.UpdateHpfUser(userItems[index]);
                        Session[SessionUserCollection] = userItems;
                        grdvHPFUser.EditIndex = -1;
                        grdvHPFUserBinding();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void grdvHPFUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Activate"))
                {
                    LinkButton lbtnActivate = e.CommandSource as LinkButton;
                    Label lblHpfUserId = lbtnActivate.Parent.FindControl("lblHpfUserId") as Label;
                    Label lblActiveInd = lbtnActivate.Parent.FindControl("lblActiveInd") as Label;
                    HPFUserDTOCollection userItems = Session[SessionUserCollection] as HPFUserDTOCollection;
                    int index = userItems.ToList().FindIndex(item => item.HpfUserId == ConvertToInt(lblHpfUserId.Text));
                    userItems[index].ActiveInd = (lblActiveInd.Text.ToUpper() == Constant.INDICATOR_NO ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    userItems[index].SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    HPFUserBL.Instance.UpdateHpfUser(userItems[index]);
                    Session[SessionUserCollection] = userItems;
                    grdvHPFUserBinding();
                }
                else if (e.CommandName.Equals("AddNew"))
                {
                    HPFUserDTO userItem = RowToHPFUserDTO(grdvHPFUser.FooterRow);
                    userItem.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    userItem.ActiveInd = Constant.INDICATOR_YES;
                    HPFUserBL.Instance.InsertHpfUser(userItem);
                    if (Session[SessionUserCollection] != null)
                    {
                        HPFUserDTOCollection userItems = Session[SessionUserCollection] as HPFUserDTOCollection;
                        userItems.Add(userItem);
                        Session[SessionUserCollection] = userItems;
                    }
                    else
                    {
                        HPFUserDTOCollection userItems = new HPFUserDTOCollection();
                        userItems.Add(userItem);
                        Session[SessionUserCollection] = userItems;
                    }
                    grdvHPFUserBinding();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private HPFUserDTO RowToHPFUserDTO(GridViewRow row)
        {
            HPFUserDTO result = new HPFUserDTO();
            #region Retrive controls from gridview row
            Label lblHpfUserId = row.FindControl("lblHpfUserId") as Label;
            TextBox txtUserLoginId = row.FindControl("txtUserLoginId") as TextBox;
            TextBox txtPassword = row.FindControl("txtPassword") as TextBox;
            TextBox txtFirstName = row.FindControl("txtFirstName") as TextBox;
            TextBox txtLastName = row.FindControl("txtLastName") as TextBox;
            TextBox txtEmail = row.FindControl("txtEmail") as TextBox;
            #endregion
            result.HpfUserId = ConvertToInt(lblHpfUserId.Text);
            result.UserLoginId = txtUserLoginId.Text;
            result.Password = txtPassword.Text;
            result.FirstName = txtFirstName.Text;
            result.LastName = txtLastName.Text;
            result.Email = txtEmail.Text;
            return result;
        }
        private int? ConvertToInt(object obj)
        {
            int returnValue = 0;

            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
    }
}