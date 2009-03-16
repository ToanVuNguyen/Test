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
        private  List<string> COLUMN_NAME = new List<string>();
        int paymentId = -1;
        bool isCancel = false;
            
        private void UcInit()
        {
            COLUMN_NAME.Add("HPF Internal Case ID");
            COLUMN_NAME.Add("HPF Invoice Case ID");
            COLUMN_NAME.Add("Payment Amount");
            COLUMN_NAME.Add("Payment Reject Reason Code");
            COLUMN_NAME.Add("Freddie Mac Servicer Number");
            COLUMN_NAME.Add("Freddie Mac Loan Number");
            COLUMN_NAME.Add("Investor Number");
            COLUMN_NAME.Add("Investor Name");
            COLUMN_NAME.Add("Servicer Name");
            COLUMN_NAME.Add("Loan Number");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            try
            {
                if (Request.QueryString["id"] != null)
                    paymentId = int.Parse(Request.QueryString["id"].ToString());
                UcInit();
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
            ddlFundingSource.SelectedValue = invoicePaymentInfo.FundingSourceID;
            txtPaymentNum.Text = invoicePaymentInfo.PaymentNum;
            txtPaymentDt.Text = invoicePaymentInfo.PaymentDate==null?"":invoicePaymentInfo.PaymentDate.Value.ToShortDateString();
            ddlPaymentType.SelectedValue = invoicePaymentInfo.PaymentType;
            txtPaymentAmt.Text = invoicePaymentInfo.PaymentAmount.ToString();
            txtComment.Text = invoicePaymentInfo.Comments;
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
            if (hiddenIsSave.Value == "true")
            {
                isCancel = true;
                SaveInvoicePayment();
            }

            Response.Redirect("InvoicePayment.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInvoicePayment();
            
        }

        private void SaveInvoicePayment()
        {
            lblErrorMessage.Items.Clear();
            try
            {
                //Validate the data input 

                //remember to remove this line 

                ControlValidation();
                if (fileUpload.FileName != string.Empty)
                    //Validate Excel file
                    ExcelProcessing();
                else
                    UpdateInvoicePaymentOnly();

            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                {
                    lblErrorMessage.Items.Add(mes.Message);
                    ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        

        private void ControlValidation()
        {
            DataValidationException ex = new DataValidationException();
            //Require
            if (ddlFundingSource.SelectedValue == "-1")
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0650));
            if (txtPaymentNum.Text == string.Empty)
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0651));
            if (txtPaymentDt.Text == string.Empty)
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0652));
            else
            {
                try
                {
                    DateTime.Parse(txtPaymentDt.Text);
                }
                catch
                {
                    ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0652));
                }
            }
            if (ddlPaymentType.SelectedValue == "-1")
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0653));
            if (txtPaymentAmt.Text == string.Empty)
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0654));
            else
            {
                try
                {
                    double.Parse(txtPaymentAmt.Text);
                }
                catch
                {
                    ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0654));
                }
            }
            if (txtComment.Text.Length > Constant.PAYMENT_COMMENT_MAX_LENGTH)
                ex.ExceptionMessages.Add(GetExceptionMessageWithoutCode(ErrorMessages.ERR0981));
            if (txtPaymentNum.Text.Length > Constant.PAYMENT_NUMBER_MAX_LENGTH)
                ex.ExceptionMessages.Add(GetExceptionMessageWithoutCode(ErrorMessages.ERR0980));
            if (ex.ExceptionMessages.Count > 0)
                throw (ex);
        }
        /// <summary>
        /// Processing Excel file, which have FrontEndPreProcessing and BackEndPreProcessing
        /// </summary>
        private void ExcelProcessing()
        {
            Stream fileContents = fileUpload.FileContent;
            if (fileContents == null)
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0655));
                throw ex;
            }
            DataSet dataSet = null;
            try
            {
                dataSet = ExcelFileReader.Read(fileContents, "Reconciliation");
            }
            catch 
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0656));
                throw ex;
            }
            if (dataSet == null||dataSet.Tables.Count==0)
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0673));
                throw ex;
            }
            //FrontEndPreProcessing on the Presentation Layer
            ReconciliationDTOCollection reconciliationCollection = FrontEndPreProcessing(dataSet);
            //BackEndPreProcessing on Business Layer
            InvoiceBL.Instance.BackEndPreProcessing(reconciliationCollection);
            //Update Invoice CAses
            InvoicePaymentDTO invoicePayment = GetInvoicePayment();
            invoicePayment.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
            paymentId= InvoiceBL.Instance.UpdateInvoicePayment(reconciliationCollection,invoicePayment);
            //if(!isCancel)
            //    Response.Redirect("InvoicePaymentInfo.aspx?id="+paymentId.ToString());  
            Response.Redirect("InvoicePayment.aspx");
        }
        private void UpdateInvoicePaymentOnly()
        {
            InvoicePaymentDTO invoicePayment = GetInvoicePayment();
            invoicePayment.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
            paymentId = InvoiceBL.Instance.UpdateInvoicePaymentOnly(invoicePayment);
            //if(!isCancel)
            //    Response.Redirect("InvoicePaymentInfo.aspx?id=" + paymentId.ToString());
            Response.Redirect("InvoicePayment.aspx");
        }
        #region FrontEnd PreProcessing
        /// <summary>
        /// Vaidate the excel file format
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        ReconciliationDTOCollection FrontEndPreProcessing(DataSet dataSet)
        {
            DataTable fileContent = dataSet.Tables[0];
            
            double paymentAmount = double.Parse(txtPaymentAmt.Text);
            ColumnsValidate(fileContent);
            RowsValidate(fileContent,paymentAmount);
            return ConvertToObjectReconciliationDTO(fileContent);
        }
        ReconciliationDTOCollection ConvertToObjectReconciliationDTO(DataTable fileContent)
        {
            ReconciliationDTOCollection result = new ReconciliationDTOCollection();
            foreach(DataRow row in fileContent.Rows)
            {
                ReconciliationDTO item = new ReconciliationDTO();
                if (row[COLUMN_NAME[0]].ToString() != string.Empty)
                    item.ForeclosureCaseId = int.Parse(row[COLUMN_NAME[0]].ToString());
                else
                    item.ForeclosureCaseId = -1;
                if (row[COLUMN_NAME[1]].ToString() != string.Empty)
                    item.InvoiceCaseId = int.Parse(row[COLUMN_NAME[1]].ToString());
                else
                    item.InvoiceCaseId = -1;
                item.PaymentAmount = double.Parse(row[COLUMN_NAME[2]].ToString());
                item.PaymentRejectReasonCode = row[COLUMN_NAME[3]].ToString();
                item.FreddieMacServicerNumber = row[COLUMN_NAME[4]].ToString();
                item.FreddieMacLoanNumber = row[COLUMN_NAME[5]].ToString();
                item.InvestorNumber = row[COLUMN_NAME[6]].ToString();
                item.InvestorName = row[COLUMN_NAME[7]].ToString();
                item.LoanNumber = row[COLUMN_NAME[9]].ToString();
                result.Add(item);
            }
            result.FundingSourceId = ddlFundingSource.SelectedValue;
            return result;
        }
        private void RowsValidate(DataTable fileContent,double paymentAmount)
        {
            DataValidationException ex = new DataValidationException();
            int rowIndex = 0;
            List<string> paymentRejectReasonCollection = GetRejectReasonCode();
            double sumOfPaymentAmount = 0;
            foreach (DataRow row in fileContent.Rows)
            {
                //Internal Case IDs
                try
                {
                    if(row[COLUMN_NAME[0]].ToString()!=string.Empty)
                        int.Parse(row[COLUMN_NAME[0]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0658,rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                //Invoice Case ID
                try
                {
                    if(row[COLUMN_NAME[1]].ToString()!=string.Empty)
                        int.Parse(row[COLUMN_NAME[1]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0662, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }
                //Internal Case ID and Invoice Case ID = null
                if (row[COLUMN_NAME[0]].ToString() == string.Empty && row[COLUMN_NAME[1]].ToString() == string.Empty)
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0678, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }
                //Payment Amounts
                try
                {
                    double pmtAmt = double.Parse(row[COLUMN_NAME[2]].ToString());
                    if (pmtAmt < 0)
                        throw (new Exception());
                    //if the row doesnt have a payment reject reason code, add it to the payment amount total for the file.
                    if(row[COLUMN_NAME[3]].ToString() == string.Empty)
                        sumOfPaymentAmount += pmtAmt;
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0659, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                //Reject Reaon Code
                if (row[COLUMN_NAME[3]].ToString() != string.Empty)
                    if (paymentRejectReasonCollection.IndexOf(row[COLUMN_NAME[3]].ToString()) == -1)
                    {
                        var exMes = GetExceptionMessage(ErrorMessages.ERR0660, rowIndex);
                        ex.ExceptionMessages.Add(exMes);
                    }
                //Neither a payment amount nor payment reject reason code exist for the record
                if (row[COLUMN_NAME[3]].ToString() == string.Empty && row[COLUMN_NAME[2]].ToString() == "0")
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0679, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                rowIndex++;
            }
            //sumOfpaymentAmount must equal total paymentAmount
            if(sumOfPaymentAmount!=paymentAmount)
            {
                var exMes = new ExceptionMessage();
                exMes.ErrorCode = ErrorMessages.ERR0661;
                exMes.Message = ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0661,rowIndex,sumOfPaymentAmount.ToString("C"),paymentAmount.ToString("C"));
                ex.ExceptionMessages.Add(exMes);
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
        }
        private void ColumnsValidate(DataTable fileContent)
        {
            DataValidationException ex = new DataValidationException();
            List<string> columnName = new List<string>();
            foreach (string name in COLUMN_NAME)
                columnName.Add(name);
            //if (fileContent.Columns.Count != columnName.Count)
            //{
            //    ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0657));
            //    throw ex;
            //}
            int columnIndex = 0;
            foreach (DataColumn col in fileContent.Columns)
            {
                int index = columnName.IndexOf(col.ColumnName);
                if (index == -1)
                {
                    ExceptionMessage exMes = GetColumnExceptionMessage(columnIndex);
                    if (exMes != null)
                        ex.ExceptionMessages.Add(exMes);
                }
                else
                    columnName.RemoveAt(index);
                columnIndex++;
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
        }
        #endregion

        #region Get Methods
        private ExceptionMessage GetColumnExceptionMessage(int columnIndex)
        {
            if (columnIndex == 0)
                return GetExceptionMessage(ErrorMessages.ERR0662);
            if (columnIndex == 1)
                return GetExceptionMessage(ErrorMessages.ERR0663);
            if (columnIndex == 2)
                return GetExceptionMessage(ErrorMessages.ERR0664);
            if (columnIndex == 3)
                return GetExceptionMessage(ErrorMessages.ERR0665);
            if (columnIndex == 4)
                return GetExceptionMessage(ErrorMessages.ERR0666);
            if (columnIndex == 5)
                return GetExceptionMessage(ErrorMessages.ERR0667);
            if (columnIndex == 6)
                return GetExceptionMessage(ErrorMessages.ERR0668);
            if (columnIndex == 7)
                return GetExceptionMessage(ErrorMessages.ERR0669);
            return null;
        }

        /// <summary>
        /// Get ExceptionMessage from ErrorMessages Class
        /// </summary>
        /// <param name="errorCode">ErrorMessages Code</param>
        /// <param name="rowIndex">Index of current row</param>
        /// <returns></returns>
        private  ExceptionMessage GetExceptionMessage(string errorCode, int rowIndex)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessageCombined(errorCode, rowIndex);
            return exMes;
        }

        private ExceptionMessage GetExceptionMessageWithoutCode(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode);
            return exMes;
        }
        private ExceptionMessage GetExceptionMessage(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessageCombined(errorCode);
            return exMes;
        }
        /// <summary>
        /// Get the List<string> contants all Reject Code
        /// </summary>
        /// <returns>List<string></returns>

        private List<string> GetRejectReasonCode()
        {
            List<string> result = new List<string>();
            var rejectReason = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_PAYMENT_REJECT_REASON_CODE);
            foreach (var i in rejectReason)
                result.Add(i.Code);
            return result;
        }
        private InvoicePaymentDTO GetInvoicePayment()
        {
            InvoicePaymentDTO result = new InvoicePaymentDTO();
            result.Comments = txtComment.Text;
            result.FundingSourceID = ddlFundingSource.SelectedValue;
            result.PaymentAmount= double.Parse(txtPaymentAmt.Text);
            result.PaymentType = ddlPaymentType.SelectedValue;
            result.PaymentNum = txtPaymentNum.Text;
            result.PaymentDate = DateTime.Parse(txtPaymentDt.Text);
            result.InvoicePaymentID = paymentId;
            return result;
        }
        #endregion
    }
}