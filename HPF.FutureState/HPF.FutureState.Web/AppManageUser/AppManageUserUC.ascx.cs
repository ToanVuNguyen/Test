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
        #region Properties
        private const string ActivateCommandText = "Activate";
        private const string DeactivateCommandText = "Deactivate";
        private HPFUserDTOCollection userCollection
        {
            get { return (HPFUserDTOCollection)ViewState["userCollection"]; }
            set { ViewState["userCollection"] = value; }
        }
        //total records in one page, get this info from web config
        protected int PageSize
        {
            //get { return (int.Parse(HPFConfigurationSettings.APP_EVALUATIONCASE_PAGE_SIZE)); }
            get { return 13; }
        }
        //total rows of search data
        protected double TotalRowNum
        {
            get { return Convert.ToDouble(ViewState["totalrownum"]); }
            set { ViewState["totalrownum"] = value; }
        }
        //current page
        protected int PageNum
        {
            get { return grdvHPFUser.PageIndex; }
            set { grdvHPFUser.PageIndex = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                grdvHPFUser.PageSize = this.PageSize;
                if (!IsPostBack)
                {
                    userCollection = HPFUserBL.Instance.RetriveHpfUsersFromDatabase();
                    grdvHPFUserBinding();
                    CalculatePaging(userCollection.Count);
                }
                else
                {
                    double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
                    GeneratePages(totalpage);
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
            if (userCollection != null)
            {
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
            HyperLink lnkUserLoginId = e.Row.FindControl("lnkUserLoginId") as HyperLink;
            HPFUserDTO userItem = e.Row.DataItem as HPFUserDTO;
            if (userItem == null) return;
            lbtnActivate.Text = (userItem.ActiveInd.ToUpper() == Constant.INDICATOR_YES ? DeactivateCommandText : ActivateCommandText);
            lnkUserLoginId.NavigateUrl = "../ManageUserPermission.aspx?userId=" + userItem.HpfUserId;

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
                    int index = userCollection.ToList().FindIndex(item => item.HpfUserId == ConvertToInt(lblHpfUserId.Text));
                    userCollection[index].ActiveInd = (lblActiveInd.Text.ToUpper() == Constant.INDICATOR_NO ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    userCollection[index].SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    HPFUserBL.Instance.UpdateHpfUser(userCollection[index]);
                    grdvHPFUserBinding();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        #region Paging
        private void CalculatePaging(double searchResultCount)
        {
            this.TotalRowNum = searchResultCount;
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            if (totalpage > 1)
            {
                GeneratePages(totalpage);
                lblTemp.Text = "1";
                ShowHidePagingControl(true);
                int MinRow = this.PageSize * PageNum + 1;
                int MaxRow = (PageNum + 1) * this.PageSize;
                lblTotalRowNum.Text = this.TotalRowNum.ToString();
                lblMinRow.Text = MinRow.ToString();
                lblMaxRow.Text = MaxRow.ToString();
                if (MaxRow > this.TotalRowNum)
                    lblMaxRow.Text = this.TotalRowNum.ToString();
                else lblMaxRow.Text = MaxRow.ToString();
            }
        }

        private void GeneratePages(double totalpage)
        {
            phPages.Controls.Clear();
            for (int i = 1; i <= totalpage; i++)
            {
                LinkButton myLinkBtn = new LinkButton();
                myLinkBtn.ID = i.ToString();
                myLinkBtn.Text = i.ToString();
                //the first time you click searh button or choosen page. disable this page.
                if (i == this.PageNum + 1)
                {
                    myLinkBtn.CssClass = "PageChoose";
                    myLinkBtn.Enabled = false;
                }
                else
                {
                    myLinkBtn.CssClass = "UnderLine";
                }
                myLinkBtn.CommandName = i.ToString();
                myLinkBtn.Command += new CommandEventHandler(myLinkBtn_Command);
                myLinkBtn.Attributes.Add("onclick", "ShowWaitPanel();");
                phPages.Controls.Add(myLinkBtn);
                //add spaces beetween pages link button.
                Literal lit = new Literal();
                lit.Text = "&nbsp;&nbsp;";
                phPages.Controls.Add(lit);
            }

            if (totalpage == 1)
            {
                lbtnLast.Enabled = false;
                lbtnNext.Enabled = false;
            }
            else
            {
                if (this.PageNum < totalpage - 1)
                {
                    lbtnLast.Enabled = true;
                    lbtnNext.Enabled = true;
                }
            }
        }

        protected void ShowHidePagingControl(bool isEnable)
        {
            lbl1.Visible = isEnable;
            lbl2.Visible = isEnable;
            lblMaxRow.Visible = isEnable;
            lblMinRow.Visible = isEnable;
            lblTotalRowNum.Visible = isEnable;
            lbtnFirst.Visible = isEnable;
            lbtnLast.Visible = isEnable;
            lbtnNext.Visible = isEnable;
            lbtnPrev.Visible = isEnable;
            phPages.Visible = isEnable;
        }
        /// <summary>
        /// when click on button:  << < > >>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnNavigate_Click(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            switch (e.CommandName)
            {
                // button: <<
                case "First":
                    this.PageNum = 0;
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    break;
                // button: >>
                case "Last":
                    lbtnLast.Enabled = false;
                    lbtnNext.Enabled = false;
                    lbtnFirst.Enabled = true;
                    lbtnPrev.Enabled = true;
                    this.PageNum = (int)totalpage - 1;
                    break;
                // button: >
                case "Next":
                    this.PageNum++;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnPrev.Enabled = true;

                    if (this.PageNum == totalpage - 1)
                    {
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }

                    break;
                // button: <
                case "Prev":
                    this.PageNum--;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnNext.Enabled = true;
                    if (this.PageNum == 0)
                    {
                        lbtnPrev.Enabled = false;
                        lbtnFirst.Enabled = false;
                    }
                    break;
            }

            ShowHidePagingControl(true);

            grdvHPFUser.DataSource = userCollection;
            grdvHPFUser.DataBind();
            CalculatePaging(this.TotalRowNum);
        }

        void myLinkBtn_Command(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            int pagenum = int.Parse(e.CommandName);
            this.PageNum = pagenum - 1;

            lbtnFirst.Enabled = true;
            lbtnLast.Enabled = true;
            lbtnNext.Enabled = true;
            lbtnPrev.Enabled = true;

            if (pagenum == 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
            }
            if (pagenum == totalpage)
            {
                lbtnLast.Enabled = false;
                lbtnNext.Enabled = false;
            }

            ShowHidePagingControl(true);

            grdvHPFUser.DataSource = userCollection;
            grdvHPFUser.DataBind();
            CalculatePaging(this.TotalRowNum);
        }
        #endregion
        private int? ConvertToInt(object obj)
        {
            int returnValue = 0;

            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUserPermission.aspx");
        }
    }
    
}