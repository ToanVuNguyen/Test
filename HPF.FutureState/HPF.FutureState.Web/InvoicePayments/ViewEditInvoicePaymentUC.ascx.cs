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
            
        private void UcInit()
        {
            COLUMN_NAME.Add("HPF Internal Case ID");
            COLUMN_NAME.Add("Loan Number");
            COLUMN_NAME.Add("Invoice Case ID");
            COLUMN_NAME.Add("Payment Amount");
            COLUMN_NAME.Add("Payment Reject Reason Code");
            COLUMN_NAME.Add("Freddie Mac Loan Number");
            COLUMN_NAME.Add("Investor Number");
            COLUMN_NAME.Add("Investor Name");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Request.QueryString["id"] != null)
                    paymentId = int.Parse(Request.QueryString["id"].ToString());
                UcInit();
                if (!IsPostBack)
                {
                    BindPaymentTypeDropDownList();
                    BindFundingSourceDropDownList();
                    if(paymentId!=-1)
                        BindViewEditInvoicePayment();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                btnSave.Enabled = false;
                return;
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
            var invoicePaymentCol = LookupDataBL.Instance.GetRefCode("payment code");
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
            lblErrorMessage.Items.Clear();
            try
            {
                //Validate the data input 
                
                //remember to remove this line 
                
                ControlValidation();
                //Validate Excel file
                ExcelProcessing();
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
            //else
            //{
            //    try
            //    {
            //        int.Parse(txtPaymentNum.Text);
            //    }
            //    catch
            //    {
            //        ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0651));
            //    }
            //}
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
                    int.Parse(txtPaymentAmt.Text);
                }
                catch
                {
                    ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0654));
                }
            }
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
                dataSet = ExcelFileReader.Read(fileContents);
            }
            catch 
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0656));
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
            Response.Redirect("InvoicePaymentInfo.aspx?id="+paymentId.ToString());
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
            
            NullValidate(fileContent);
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
                item.ForeclosureCaseId = int.Parse(row[COLUMN_NAME[0]].ToString());
                item.LoanNumber = row[COLUMN_NAME[1]].ToString();
                item.InvoiceCaseId = int.Parse(row[COLUMN_NAME[2]].ToString());
                item.PaymentAmount = double.Parse(row[COLUMN_NAME[3]].ToString());
                item.PaymentRejectReasonCode = row[COLUMN_NAME[4]].ToString();
                item.FreddieMacLoanNumber = row[COLUMN_NAME[5]].ToString();
                item.InvestorNumber = row[COLUMN_NAME[6]].ToString();
                item.InvestorName = row[COLUMN_NAME[7]].ToString();
                result.Add(item);
            }
            return result;
        }
        private static void NullValidate(DataTable fileContent)
        {
            if (fileContent == null)
                throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0655));
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
                    int.Parse(row[COLUMN_NAME[2]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0662, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                //Payment Amounts
                try
                {
                    sumOfPaymentAmount += double.Parse(row[COLUMN_NAME[3]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0659, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                //Reject Reaon Code
                if (row[COLUMN_NAME[4]].ToString() != string.Empty)
                    if (paymentRejectReasonCollection.IndexOf(row[COLUMN_NAME[4]].ToString()) == -1)
                    {
                        var exMes = GetExceptionMessage(ErrorMessages.ERR0660, rowIndex);
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
            List<string> columnName = new List<string>();
            foreach (string name in COLUMN_NAME)
                columnName.Add(name);
            if (fileContent.Columns.Count != columnName.Count)
                throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0657));
            foreach (DataColumn col in fileContent.Columns)
            {
                int index = columnName.IndexOf(col.ColumnName);
                if (index == -1)
                {
                    throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0657));
                }
                columnName.RemoveAt(index);
            }
        }
        #endregion
        #region Get Methods
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
            var rejectReason = LookupDataBL.Instance.GetRefCode("payment reject reason code");
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