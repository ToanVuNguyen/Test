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

namespace HPF.FutureState.Web.InvoicePayments
{
    public partial class ViewEditInvoicePayment : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindViewEditInvoicePayment();
                //BindPaymentTypeDropDownList();
                BindFundingSourceDropDownList();
            }
        }
        protected void BindViewEditInvoicePayment()
        {
            if (Session["invoicePaymentInfo"] != null)
            {
                InvoicePaymentDTO invoicePaymentInfo = new InvoicePaymentDTO();
                invoicePaymentInfo = (InvoicePaymentDTO)Session["invoicePaymentInfo"];
                lblPaymentID.Text = invoicePaymentInfo.InvoicePaymentID.ToString();
                ddlFundingSource.SelectedValue = invoicePaymentInfo.FundingSourceID;
                txtPaymentNum.Text = invoicePaymentInfo.PaymentNum;
                txtPaymentDt.Text = invoicePaymentInfo.PaymentDate.ToShortDateString();
                ddlPaymentType.SelectedValue = invoicePaymentInfo.PaymentCode;
                txtPaymentAmt.Text = String.Format("{0:C}", invoicePaymentInfo.PaymentAmount);
                //miss file and comments
            }
        }
        protected void BindPaymentTypeDropDownList()
        {
            InvoicePaymentDTOCollection invoicePaymentCol = LookupDataBL.Instance.PaymentTypeGet();
            ddlFundingSource.DataSource = invoicePaymentCol;
            ddlFundingSource.DataTextField = "PaymentCode";
            ddlFundingSource.DataValueField = "PaymentCode";
            ddlFundingSource.DataBind();
        }

        protected void BindFundingSourceDropDownList()
        {
            FundingSourceDTOCollection fundingSourceCol = LookupDataBL.Instance.GetFundingSource();
            ddlFundingSource.DataSource = fundingSourceCol;
            ddlFundingSource.DataTextField = "FundingSourceName";
            ddlFundingSource.DataValueField = "FundingSourceID";
            ddlFundingSource.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoicePayment.aspx");
        }
    }
}