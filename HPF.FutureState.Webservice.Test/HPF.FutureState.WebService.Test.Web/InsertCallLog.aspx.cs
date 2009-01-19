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
            //txtCallCenterID.Text = aCallLogWS.CallCenterID.ToString();
            txtCallCenter.Text = aCallLogWS.CallCenter;
            txtAuthorizedInd.Text = aCallLogWS.AuthorizedInd;
            txtCallSourceCd.Text = aCallLogWS.CallSourceCd;
            txtCcAgentIdKey.Text = aCallLogWS.CcAgentIdKey; //"CCAgentIdKey" + _pageloadno.ToString();
            txtCcCallKey.Text = aCallLogWS.CcCallKey; //"CcCallKey" + _pageloadno.ToString();
            txtDNIS.Text = aCallLogWS.DNIS; //"DNIS" + _pageloadno.ToString();
            txtEndDate.Text = aCallLogWS.EndDate.ToString();
            txtFinalDispoCd.Text = aCallLogWS.FinalDispoCd; //"FinalDispoCd" + _pageloadno.ToString();
            txtFirstName.Text = aCallLogWS.FirstName; //"FirstName" + _pageloadno.ToString();
            txtHomeownerInd.Text = aCallLogWS.HomeownerInd; //"Y";
            txtLastName.Text = aCallLogWS.LastName; //"LastName" + _pageloadno.ToString();
            txtLoanAccountNumber.Text = aCallLogWS.LoanAccountNumber; //"LoanAccountNumber" + _pageloadno.ToString();
            txtLoanDelinqStatusCd.Text = aCallLogWS.LoanDelinqStatusCd; //"0123456789";
            txtOtherServicerName.Text = aCallLogWS.OtherServicerName; //"OtherServicerName" + _pageloadno.ToString();

            txtPowerOfAttorneyInd.Text = aCallLogWS.PowerOfAttorneyInd; //"Y";
            txtPrevAgencyId.Text = aCallLogWS.PrevAgencyId.ToString(); //"PrevAgencyId" + _pageloadno.ToString();
            txtPropZipFull9.Text = aCallLogWS.PropZipFull9; //"123456789";
            txtReasonToCall.Text = aCallLogWS.ReasonToCall; //"ReasonToCall" + _pageloadno.ToString();
            txtScreenRout.Text = aCallLogWS.ScreenRout; //"ScreenRout" + _pageloadno.ToString();
            txtSelectedAgencyId.Text = aCallLogWS.SelectedAgencyId; //"SelectedAgencyId" + _pageloadno.ToString();
            txtSelectedCounselor.Text = aCallLogWS.SelectedCounselor; //"SelectedCounselor" + _pageloadno.ToString();
            txtServiceID.Text = aCallLogWS.ServicerId.ToString(); //"1";
            txtStartDate.Text = aCallLogWS.StartDate.ToString(); //DateTime.Now;
            txtTransNumber.Text = aCallLogWS.TransNumber; //"TransNumber" + _pageloadno.ToString();

            txtLastChangeUserId.Text = aCallLogWS.ChangeLastUserId;
            txtCreateUserId.Text = aCallLogWS.CreateUserId;
        }

        private CallLogWSDTO FormToCallLogWSDTO()
        {
            CallLogWSDTO aWSCallLog = new CallLogWSDTO();

            #region set value
            aWSCallLog.AuthorizedInd = txtAuthorizedInd.Text.Trim();
            aWSCallLog.CallCenter = txtCallCenter.Text.Trim();            
            //aWSCallLog.CallCenterID = Util.ConvertToInt(txtCallCenterID.Text.Trim());
            aWSCallLog.CallSourceCd = txtCallSourceCd.Text.Trim();
            aWSCallLog.CcCallKey = txtCcCallKey.Text.Trim();
            aWSCallLog.CcAgentIdKey = txtCcAgentIdKey.Text.Trim();            
            aWSCallLog.DNIS = txtDNIS.Text.Trim();
            aWSCallLog.EndDate = Util.ConvertToDateTime(txtEndDate.Text.Trim());
            aWSCallLog.FinalDispoCd = txtFinalDispoCd.Text.Trim();            
            aWSCallLog.FirstName = txtFirstName.Text.Trim();
            aWSCallLog.HomeownerInd = txtHomeownerInd.Text.Trim();
            aWSCallLog.LastName = txtLastName.Text.Trim();
            aWSCallLog.LoanAccountNumber = txtLoanAccountNumber.Text.Trim();
            aWSCallLog.LoanDelinqStatusCd = txtLoanDelinqStatusCd.Text.Trim();
            aWSCallLog.OtherServicerName = txtOtherServicerName.Text.Trim();
            aWSCallLog.PowerOfAttorneyInd = txtPowerOfAttorneyInd.Text.Trim();
            aWSCallLog.PrevAgencyId = Util.ConvertToInt(txtPrevAgencyId.Text.Trim());
            aWSCallLog.PropZipFull9 = txtPropZipFull9.Text.Trim();
            aWSCallLog.ReasonToCall = txtReasonToCall.Text.Trim();
            aWSCallLog.ScreenRout = txtScreenRout.Text.Trim();
            aWSCallLog.SelectedAgencyId = txtSelectedAgencyId.Text.Trim();
            aWSCallLog.SelectedCounselor = txtSelectedCounselor.Text.Trim();
            aWSCallLog.ServicerId = Util.ConvertToInt(txtServiceID.Text.Trim());
            aWSCallLog.StartDate = Util.ConvertToDateTime(txtStartDate.Text.Trim());
            aWSCallLog.TransNumber = txtTransNumber.Text.Trim();
            aWSCallLog.CreateUserId = txtCreateUserId.Text.Trim();
            aWSCallLog.ChangeLastUserId = txtLastChangeUserId.Text.Trim();

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
