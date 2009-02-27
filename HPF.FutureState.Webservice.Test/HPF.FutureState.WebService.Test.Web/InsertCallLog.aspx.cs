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
using System.Xml;
using System.Collections.Generic;

using System.Data.SqlClient;

using HPF.Webservice.CallCenter;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class InsertCallLog : System.Web.UI.Page
    {
        public static int _pageloadno = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Focus();
            if (!IsPostBack)
            {
                LoadDefaultCallLogWSDTO();
            }
        }

        private void LoadDefaultCallLogWSDTO()
        {
            string filename = MapPath(ConfigurationManager.AppSettings["CallLogWSDTOXML"]);
            XDocument xdoc = GetXmlDocument(filename);
            BindToForm(xdoc);
        }

        private void BindToForm(XDocument xdoc)
        {
            Session[SessionVariables.CALLLOG_WS] = CallCenterHelper.ParseCallLogWSDTO(xdoc);
            CallLogWSDTOToForm((CallLogWSDTO)Session[SessionVariables.CALLLOG_WS]);
        }

        private static XDocument GetXmlDocument(string filename)
        {
            XDocument xdoc = XDocument.Load(filename);
            return xdoc;
        }

        private static XDocument GetXmlDocument(XmlReader xmlreader)
        {
            XDocument xdoc = XDocument.Load(xmlreader);
            return xdoc;
        }

        private void CallLogWSDTOToForm(CallLogWSDTO aCallLogWS)
        {
            txtCallCenter.Text = aCallLogWS.CallCenter;
            txtAuthorizedInd.Text = aCallLogWS.AuthorizedInd;
            txtCallSourceCd.Text = aCallLogWS.CallSourceCd;
            txtCcAgentIdKey.Text = aCallLogWS.CcAgentIdKey;
            txtCcCallKey.Text = aCallLogWS.CcCallKey;
            txtDNIS.Text = aCallLogWS.DNIS;
            txtEndDate.Text = aCallLogWS.EndDate.ToString();
            txtFinalDispoCd.Text = aCallLogWS.FinalDispoCd;
            txtFirstName.Text = aCallLogWS.FirstName;
            txtHomeownerInd.Text = aCallLogWS.HomeownerInd;
            txtLastName.Text = aCallLogWS.LastName;
            txtLoanAccountNumber.Text = aCallLogWS.LoanAccountNumber;
            txtLoanDelinqStatusCd.Text = aCallLogWS.LoanDelinqStatusCd;
            txtOtherServicerName.Text = aCallLogWS.OtherServicerName;

            txtPowerOfAttorneyInd.Text = aCallLogWS.PowerOfAttorneyInd;
            txtPrevAgencyId.Text = aCallLogWS.PrevAgencyId.ToString();
            txtPropZipFull9.Text = aCallLogWS.PropZipFull9;
            txtReasonToCall.Text = aCallLogWS.ReasonForCall;
            txtScreenRout.Text = aCallLogWS.ScreenRout; 
            txtSelectedAgencyId.Text = aCallLogWS.SelectedAgencyId.ToString(); 
            txtSelectedCounselor.Text = aCallLogWS.SelectedCounselor;
            txtServiceID.Text = aCallLogWS.ServicerId.ToString();
            txtStartDate.Text = aCallLogWS.StartDate.ToString(); 
            txtTransNumber.Text = aCallLogWS.TransNumber; 

            txtState.Text = aCallLogWS.State;
            txtCity.Text = aCallLogWS.City;
            txtNonprofitReferral1.Text = aCallLogWS.NonprofitReferralKeyNum1;
            txtNonprofitReferral2.Text = aCallLogWS.NonprofitReferralKeyNum2;
            txtNonprofitReferral3.Text = aCallLogWS.NonprofitReferralKeyNum3;
        }

        private CallLogWSDTO FormToCallLogWSDTO()
        {
            CallLogWSDTO aWSCallLog = new CallLogWSDTO();

            #region set value
            aWSCallLog.AuthorizedInd = Util.ConvertToString(txtAuthorizedInd.Text.Trim());
            aWSCallLog.CallCenter = Util.ConvertToString(txtCallCenter.Text.Trim());
            aWSCallLog.CallSourceCd = Util.ConvertToString(txtCallSourceCd.Text.Trim());
            aWSCallLog.CcCallKey = Util.ConvertToString(txtCcCallKey.Text.Trim());
            aWSCallLog.CcAgentIdKey = Util.ConvertToString(txtCcAgentIdKey.Text.Trim());
            aWSCallLog.DNIS = Util.ConvertToString(txtDNIS.Text.Trim());
            aWSCallLog.EndDate = Util.ConvertToDateTime(txtEndDate.Text.Trim());
            aWSCallLog.FinalDispoCd = Util.ConvertToString(txtFinalDispoCd.Text.Trim());
            aWSCallLog.FirstName = Util.ConvertToString(txtFirstName.Text.Trim());
            aWSCallLog.HomeownerInd = Util.ConvertToString(txtHomeownerInd.Text.Trim());
            aWSCallLog.LastName = Util.ConvertToString(txtLastName.Text.Trim());
            aWSCallLog.LoanAccountNumber = Util.ConvertToString(txtLoanAccountNumber.Text.Trim());
            aWSCallLog.LoanDelinqStatusCd = Util.ConvertToString(txtLoanDelinqStatusCd.Text.Trim());
            aWSCallLog.OtherServicerName = Util.ConvertToString(txtOtherServicerName.Text.Trim());
            aWSCallLog.PowerOfAttorneyInd =Util.ConvertToString( txtPowerOfAttorneyInd.Text.Trim());
            aWSCallLog.PrevAgencyId = Util.ConvertToInt(txtPrevAgencyId.Text.Trim());
            aWSCallLog.PropZipFull9 = Util.ConvertToString(txtPropZipFull9.Text.Trim());
            aWSCallLog.ReasonForCall = Util.ConvertToString(txtReasonToCall.Text.Trim());
            aWSCallLog.ScreenRout = Util.ConvertToString(txtScreenRout.Text.Trim());
            aWSCallLog.SelectedAgencyId = Util.ConvertToInt(txtSelectedAgencyId.Text.Trim());
            aWSCallLog.SelectedCounselor = Util.ConvertToString(txtSelectedCounselor.Text.Trim());
            aWSCallLog.ServicerId = Util.ConvertToInt(txtServiceID.Text.Trim());
            aWSCallLog.StartDate = Util.ConvertToDateTime(txtStartDate.Text.Trim());
            aWSCallLog.TransNumber = Util.ConvertToString(txtTransNumber.Text.Trim());
            aWSCallLog.City = Util.ConvertToString(txtCity.Text.Trim());
            aWSCallLog.State = Util.ConvertToString(txtState.Text.Trim());
            aWSCallLog.NonprofitReferralKeyNum1 = Util.ConvertToString(txtNonprofitReferral1.Text.Trim());
            aWSCallLog.NonprofitReferralKeyNum2 = Util.ConvertToString(txtNonprofitReferral2.Text.Trim());
            aWSCallLog.NonprofitReferralKeyNum3 = Util.ConvertToString(txtNonprofitReferral3.Text.Trim());
            #endregion

            return aWSCallLog;
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CallLogInsertRequest request = new CallLogInsertRequest();
            
            CallLogWSDTO aWSCallLog = FormToCallLogWSDTO();
            
            CallCenterService proxy = new CallCenterService();            
            
            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            request.CallLog = aWSCallLog;
            CallLogInsertResponse response = proxy.SaveCallLog(request);

            if (response.Status == ResponseStatus.Success)
            {
                lblResult.Text = response.CallLogID;

                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
            }
            else
            {
                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
                grdvResult.Visible = true;
            }

        }

        

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            grdvResult.Visible = false;
            try
            {
                if (fileUpload.HasFile)
                {
                    XmlReader xmlReader = new XmlTextReader(fileUpload.FileContent);
                    XDocument xdoc = GetXmlDocument(xmlReader);
                    BindToForm(xdoc);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ExceptionMessage em = new ExceptionMessage();
                List<ExceptionMessage> exList = new List<ExceptionMessage>();
                em.Message = "Invalid XML format";
                exList.Add(em);
                em = new ExceptionMessage();
                em.Message = "Default Call Log is loaded";
                exList.Add(em);
                grdvResult.DataSource = exList;
                grdvResult.DataBind();
                grdvResult.Visible = true;

                ClearControls();

                LoadDefaultCallLogWSDTO();

            }
            
        }

        private void ClearControls()
        {
            foreach (Control item in Page.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Text = "";
            }
        }
    }
}
