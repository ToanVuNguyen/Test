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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils;
using System.IO;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;
using System.Collections.Generic;
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web.InvoicePayments
{
    public partial class ViewEditInvoicePayment : System.Web.UI.UserControl
    {
        
        int paymentId = -1;
        bool isCancel = false;
            
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            ClearErrorMessage();
          
            try
            {
                if (Request.QueryString["id"] != null)
                    paymentId = int.Parse(Request.QueryString["id"].ToString());
                if (!IsPostBack)
                {
                    BindPaymentTypeDropDownList();
                    BindFundingSourceDropDownList();
                    if (paymentId != -1)
                    {
                        BindViewEditInvoicePayment();
                        lblTitle.Text = "View/Edit Invoice Payment";
                    }
                }
                btnCancel.Attributes.Add("onclick", "return ConfirmToCancel();");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                btnSave.Enabled = false;
                return;
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_INVOICE_PAYMENT))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_INVOICE_PAYMENT))
            {
                btnSave.Enabled = false;
                txtComment.Enabled = false;
                txtPaymentAmt.Enabled = false;
                txtPaymentDt.Enabled = false;
                txtPaymentNum.Enabled = false;
                fileUpload.Enabled = false;
                ddlFundingSource.Enabled = false;
                ddlPaymentType.Enabled = false;
            }
        }
        #region Bind Data
        protected void BindViewEditInvoicePayment()
        {
            InvoicePaymentDTO invoicePaymentInfo = InvoicePaymentBL.Instance.InvoicePaymentGet(paymentId);
            Session["InvoicePayment"] = invoicePaymentInfo;
            lblPaymentID.Text = invoicePaymentInfo.InvoicePaymentID.ToString();
            ddlFundingSource.SelectedValue = invoicePaymentInfo.FundingSourceID.ToString();
            txtPaymentNum.Text = invoicePaymentInfo.PaymentNum;
            txtPaymentDt.Text = invoicePaymentInfo.PaymentDate==null?"":invoicePaymentInfo.PaymentDate.Value.ToShortDateString();
            ddlPaymentType.SelectedValue = invoicePaymentInfo.PaymentType;
            txtPaymentAmt.Text = invoicePaymentInfo.PaymentAmount.ToString();
            txtComment.Text = invoicePaymentInfo.Comments;
            txtPaymentFile.Text = invoicePaymentInfo.PaymentFile;
        }
        protected void BindPaymentTypeDropDownList()
        {
            var invoicePaymentCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_PAYMENT_CODE);
            ddlPaymentType.DataSource = invoicePaymentCol;
            ddlPaymentType.DataTextField = "CodeDesc";
            ddlPaymentType.DataValueField = "Code";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0,new ListItem("","-1"));
        }

        protected void BindFundingSourceDropDownList()
        {
            FundingSourceDTOCollection fundingSourceCol = LookupDataBL.Instance.GetFundingSource();
            ddlFundingSource.DataSource = fundingSourceCol;
            ddlFundingSource.DataTextField = "FundingSourceName";
            ddlFundingSource.DataValueField = "FundingSourceID";
            ddlFundingSource.DataBind();
            //Remove "All" item
            ddlFundingSource.Items.FindByValue("-1").Text="";
        }
        #endregion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoicePayment.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInvoicePayment();
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        private void SaveInvoicePayment()
        {
            ClearErrorMessage();
            try
            {
                //Validate the data input 

                //remember to remove this line 
                InvoicePaymentDTO invoicePayment = GetInvoicePayment();
                invoicePayment.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                InvoiceBL.Instance.ValidateInvoicePayment(invoicePayment);
                if (fileUpload.HasFile)
                    //Validate Excel file
                    InvoiceBL.Instance.ExcelProcessing(fileUpload.FileContent,invoicePayment);
                else
                    if(string.IsNullOrEmpty(hidFileName.Value))
                        UpdateInvoicePaymentOnly();
                    else
                    {
                        DataValidationException ex = new DataValidationException();
                        ex.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0685, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0685));
                        throw ex;
                    }
                Response.Redirect("InvoicePayment.aspx");
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        

        
        /// <summary>
        /// Processing Excel file, which have FrontEndPreProcessing and BackEndPreProcessing
        /// </summary>
        
        private void UpdateInvoicePaymentOnly()
        {

            InvoicePaymentDTO invoicePayment = GetInvoicePayment();
            invoicePayment.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
            //Update and cleared old data.
            paymentId = InvoiceBL.Instance.UpdateInvoicePaymentOnly(invoicePayment);
            Response.Redirect("InvoicePayment.aspx");
        }
        

        #region Get Methods
        private InvoicePaymentDTO GetInvoicePayment()
        {
            InvoicePaymentDTO result = new InvoicePaymentDTO();
            result.Comments = txtComment.Text;
            result.FundingSourceID = ConvertToInt(ddlFundingSource.SelectedValue);
            result.PaymentAmount= ConvertToDouble(txtPaymentAmt.Text);
            result.PaymentType = ddlPaymentType.SelectedValue=="-1"?null:ddlPaymentType.SelectedValue;
            result.PaymentNum = txtPaymentNum.Text;
            result.PaymentDate = ConvertToDateTime(txtPaymentDt.Text);
            result.InvoicePaymentID = paymentId;
            if(fileUpload.HasFile)
                result.PaymentFile = fileUpload.PostedFile.FileName;
            return result;
        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
        private double ConvertToDouble(object obj)
        {
            double value;
            if (double.TryParse(obj.ToString().Trim(), out value))
                return value;
            return double.MinValue;
        }
        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString().Trim(), out dt))
                return dt;
            return DateTime.MinValue;
        }
        #endregion

        protected void btnYes_Click(object sender, EventArgs e)
        {
            isCancel = true;
            SaveInvoicePayment();
            
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoicePayment.aspx");
        }

        protected void saveYes_Click(object sender, EventArgs e)
        {
            isCancel = true;
            SaveInvoicePayment();
        }
    }
}