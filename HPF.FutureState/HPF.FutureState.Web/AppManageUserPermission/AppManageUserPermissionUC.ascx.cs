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
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.AppManageUserPermission
{
    public partial class AppManageUserPermissionUC : System.Web.UI.UserControl
    {
        private MenuSecurityDTOCollection menuSecurityCollection;
        private UserDTO user
        {
            get { return (UserDTO)ViewState["user"]; }
            set { ViewState["user"] = value; }
        }
        private int userId
        {
            get { return (int)ViewState["userId"]; }
            set { ViewState["userId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindUserTypeDropDownList();
                    BindAgencyDropDownList();
                    if (Request.QueryString["userId"]!=null)
                    {
                        userId = int.Parse(Request.QueryString["userId"].ToString());
                        BindUserInfo();
                        btnAddNew.Visible = false;
                    }
                    else
                    {
                        userId = -1;
                        btnUpdate.Visible = false;
                    }
                }
                BindMenuSecurity();
                ClearErrorMessages();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindUserInfo()
        {
            user = MenuSecurityBL.Instance.RetriveUserInfoById(userId);
            //Show user info
            txtUserLoginId.Text = user.UserName;
            txtPassword.Attributes.Add("value",user.Password);
            txtFirstName.Text = user.FirstName;
            txtLastName.Text =user.LastName;
            txtEmail.Text = user.Email;
            txtPhone.Text = (string.IsNullOrEmpty(user.Phone) ? "" : user.Phone);
            chkActive.Checked = (user.IsActivate == Constant.INDICATOR_YES[0] ? true : false);
            if (!string.IsNullOrEmpty(user.UserType))
            {
                ddlUserType.ClearSelection();
                ddlUserType.Items.FindByValue(user.UserType).Selected = true;
            }
            if (user.AgencyId != null)
            {
                ddlAgency.ClearSelection();
                ddlAgency.Items.FindByValue(user.AgencyId.ToString()).Selected = true;
            }
        }
        private void BindMenuSecurity()
        {
            menuSecurityCollection = MenuSecurityBL.Instance.RetriveAllMenuSecurityByUser(userId);
            //Show menu permission
            placeHolder.Controls.Clear();
            foreach (MenuSecurityDTO menuSecurity in menuSecurityCollection)
                placeHolder.Controls.Add(RenderMenuPermissionRow(menuSecurity));
        }
        private void BindUserTypeDropDownList()
        {
            ddlUserType.Items.Add(new ListItem("None", "-1"));
            ddlUserType.Items.Add(new ListItem(Constant.USER_TYPE_HPF, Constant.USER_TYPE_HPF));
            ddlUserType.Items.Add(new ListItem(Constant.USER_TYPE_AGENCY, Constant.USER_TYPE_AGENCY));
        }
        private void BindAgencyDropDownList()
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
            ddlAgency.Items.Insert(0, new ListItem("None", "-1"));
        }
        private TableRow RenderMenuPermissionRow(MenuSecurityDTO menuSecurity)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            //Add check box
            tc.Attributes.Add("Align", "Center");
            CheckBox chkBox = new CheckBox();
            chkBox.Checked = (menuSecurity.MenuSecurityId!=-1 ? true : false);
            chkBox.ID = "chk" + menuSecurity.MenuItemId.ToString() ;
            tc.Controls.Add(chkBox);
            tr.Controls.Add(tc);
            //Add menu name
            tc = new TableCell();
            tc.Attributes.Add("align", "left");
            tc.Attributes.Add("class", "Text");
            Label lbl = new Label();
            lbl.Text = menuSecurity.MenuName;
            tc.Controls.Add(lbl);
            tr.Controls.Add(tc);
            //Add permission drop down list box
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            DropDownList ddl = new DropDownList();
            ddl.ID = "ddl" + menuSecurity.MenuItemId.ToString();
            ddl.Items.Add(new ListItem("Read-Only", "R"));
            ddl.Items.Add(new ListItem("Update", "U"));
            ddl.ClearSelection();
            ddl.Items.FindByValue(menuSecurity.Permission.ToString()).Selected = true;
            tc.Controls.Add(ddl);
            tr.Controls.Add(tc);
            return tr;
        }
        private MenuSecurityDTOCollection DraftInfo(MenuSecurityDTOCollection items,UserDTO user)
        {
            //Get user info
            user.UserName = txtUserLoginId.Text;
            user.Password = txtPassword.Text;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Email = txtEmail.Text;
            user.Phone = txtPhone.Text;
            user.UserType = (ddlUserType.SelectedValue == "-1" ? null : ddlUserType.SelectedValue);
            user.AgencyId =ConvertToInt((ddlAgency.SelectedValue == "-1" ? null : ddlAgency.SelectedValue));
            user.IsActivate = (chkActive.Checked==true?Constant.INDICATOR_YES[0]:Constant.INDICATOR_NO[0]);

            //Get menu permission info
            foreach (MenuSecurityDTO item in items)
            {
                CheckBox chkBox = placeHolder.FindControl("chk" + item.MenuItemId) as CheckBox;
                DropDownList ddl = placeHolder.FindControl("ddl" + item.MenuItemId) as DropDownList;
                item.Permission = ddl.SelectedValue[0];
                if ((chkBox.Checked) && (item.MenuSecurityId == -1))
                    item.StatusChanged = (byte)MenuSecurityBL.StatusChanged.Insert;
                else
                    item.StatusChanged = (byte)(chkBox.Checked ? MenuSecurityBL.StatusChanged.Update : MenuSecurityBL.StatusChanged.Remove);
            }
            return items;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DraftInfo(menuSecurityCollection, user);
                user.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                MenuSecurityBL.Instance.UpdateUserSecurity(menuSecurityCollection, user);
                txtPassword.Attributes.Add("value", user.Password);
                BindMenuSecurity();
                lblErrorMessage.Items.Add(new ListItem("Update successfully!"));
            }
            catch (DataValidationException ex1)
            {
                lblErrorMessage.DataSource = ex1.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex1, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private int? ConvertToInt(object obj)
        {
            int returnValue = 0;

            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUser.aspx");
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                user = new UserDTO();
                DraftInfo(menuSecurityCollection, user);
                user.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                userId = MenuSecurityBL.Instance.InsertUserSecurity(menuSecurityCollection, user);
                txtPassword.Attributes.Add("value", user.Password);
                BindMenuSecurity();
                lblErrorMessage.Items.Add(new ListItem("Insert successfully!"));
                btnAddNew.Visible = false;
                btnUpdate.Visible = true;
            }
            catch (DataValidationException ex1)
            {
                lblErrorMessage.DataSource = ex1.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex1, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}