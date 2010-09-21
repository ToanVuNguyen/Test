﻿using System;
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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.SendSummaryToServicer
{
    public partial class SendSummaryToServicer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (!IsPostBack)
            {
                Initialize();
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_SEND_SUMMARY_TO_SERVICER))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }

        private void Initialize()
        {
            SendSummaryServicerCollectionDTO servicers = LookupDataBL.Instance.GetSendSummarySevicers();
            servicers.Insert(0, new SendSummaryServicerDTO {ServicerID = null, Description = ""});
            ddlServicer.DataTextField = "Description";
            ddlServicer.DataValueField = "ServicerID";
            ddlServicer.DataSource = servicers;
            ddlServicer.DataBind();
            txtPeriodStart.Text = DateTime.Now.ToShortDateString();
            txtPeriodEnd.Text = DateTime.Now.ToShortDateString();
            rbtnSendBasedDateRange.Checked = true;
            SetSendType();
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
                string message = "";
                int processedCount = 0;
                if (rbtnSendBasedDateRange.Checked)
                {
                    AppSummariesToServicerCriteriaDTO criteriaDTO = new AppSummariesToServicerCriteriaDTO();
                    if (!string.IsNullOrEmpty(ddlServicer.SelectedValue))
                        criteriaDTO.ServicerId = int.Parse(ddlServicer.SelectedValue);
                    DateTime datevalue;
                    if (DateTime.TryParse(txtPeriodStart.Text.Trim(), out datevalue))
                        criteriaDTO.StartDt = datevalue;
                    if (DateTime.TryParse(txtPeriodEnd.Text.Trim(), out datevalue))
                        criteriaDTO.EndDt = datevalue;
                    processedCount = ForeclosureCaseBL.Instance.SendSummariesToServicer(criteriaDTO, HPFWebSecurity.CurrentIdentity.LoginName);

                    SendSummaryServicerCollectionDTO servicers = LookupDataBL.Instance.GetSendSummarySevicers();
                    SendSummaryServicerDTO servicer = servicers.GetServicerById(criteriaDTO.ServicerId);
                    message = string.Format("{0} summary cases have been sent to {1} via {2} for cases completed between {3} and {4}.", processedCount, servicer.ServicerName, servicer.SummaryDeliveryMethodDesc, criteriaDTO.StartDt.Value.ToShortDateString(), criteriaDTO.EndDt.Value.ToShortDateString());
                }
                else
                {
                    ForeclosureCaseBL workingInstance = ForeclosureCaseBL.Instance;
                    processedCount = workingInstance.SendSummariesToServicerBasedOnFile(fileUpload.FileContent, HPFWebSecurity.CurrentIdentity.LoginName);
                    message = string.Format("{0} summary cases have been sent for cases completed as per uploaded excel file", processedCount);
                    if (workingInstance.WarningMessage.Count > 0)
                    {
                        lstErrorMessage.DataSource = workingInstance.WarningMessage;
                        lstErrorMessage.DataBind();
                    }
                }
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

        protected void rbtnSendBasedDateRange_CheckedChanged(object sender, EventArgs e)
        {
            SetSendType();
        }
        protected void rbtnSendBasedFiled_CheckedChanged(object sender, EventArgs e)
        {
            SetSendType();
        }
        private void SetSendType()
        {
            ddlServicer.Enabled = rbtnSendBasedDateRange.Checked;
            txtPeriodStart.Enabled = rbtnSendBasedDateRange.Checked;
            txtPeriodEnd.Enabled = rbtnSendBasedDateRange.Checked;
            fileUpload.Enabled = rbtnSendBasedFiled.Checked;
        }
    }
}