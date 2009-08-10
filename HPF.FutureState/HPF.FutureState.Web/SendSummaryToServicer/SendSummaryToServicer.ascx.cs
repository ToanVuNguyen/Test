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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.SendSummaryToServicer
{
    public partial class SendSummaryToServicer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            ServicerDTOCollection servicers = LookupDataBL.Instance.GetCanSendSevicers();
            foreach (ServicerDTO servicer in servicers)
            {
                ListItem item = new ListItem();
                item.Value = servicer.ServicerID.ToString();
                item.Text = servicer.ServicerName + " - " + servicer.SummaryDeliveryMethod;
                ddlServicer.Items.Add(item);
            }

            txtPeriodStart.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            txtPeriodEnd.Text = DateTime.Now.ToShortDateString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                lstErrorMessage.Items.Clear();
                AppSummariesToServicerCriteriaDTO criteriaDTO = new AppSummariesToServicerCriteriaDTO();                
                criteriaDTO.ServicerId = int.Parse(ddlServicer.SelectedValue);
                DateTime datevalue;
                if (DateTime.TryParse(txtPeriodStart.Text.Trim(), out datevalue))
                    criteriaDTO.StartDt = datevalue;
                if (DateTime.TryParse(txtPeriodEnd.Text.Trim(), out datevalue))
                    criteriaDTO.EndDt = datevalue;
                int processedCount = ForeclosureCaseBL.Instance.SendSummariesToServicer(criteriaDTO, HPFWebSecurity.CurrentIdentity.LoginName);

                ServicerDTOCollection servicers = LookupDataBL.Instance.GetServicers();
                ServicerDTO servicer = servicers.GetServicerById(criteriaDTO.ServicerId);
                string message = string.Format("{0} summary cases for {1} have been sent by method {2} for complete date between {3} and {4}", processedCount, servicer.ServicerName, servicer.SummaryDeliveryMethod, criteriaDTO.StartDt.Value.ToShortDateString(), criteriaDTO.EndDt.Value.ToShortDateString());
                lstErrorMessage.Items.Add(message);
            }
            catch (DataValidationException dataEx)
            {
                lstErrorMessage.DataSource = dataEx.ExceptionMessages;
                lstErrorMessage.DataBind();
                ExceptionProcessor.HandleException(dataEx, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch(Exception Ex)
            {                
                lstErrorMessage.Items.Add(Ex.Message);
                ExceptionProcessor.HandleException(Ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}