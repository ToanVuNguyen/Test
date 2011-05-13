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
using HPF.Webservice.Agency;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class SaveEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefaultEvent();
            }
            else
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
            }
        }
        private void LoadDefaultEvent()
        {
            string filename = MapPath(ConfigurationManager.AppSettings["EventXML"]);
            XDocument xdoc = GetXmlDocument(filename);
            BindToForm(xdoc);
        }

        private void BindToForm(XDocument xdoc)
        {
            Session[SessionVariables.EVENT] = AgencyHelper.ParseEventDTO(xdoc);
            EventToForm((EventDTO)Session[SessionVariables.EVENT]);
        }
        private void EventToForm(EventDTO anEvent)
        {
            if (anEvent != null)
            {
                if (anEvent.FcId.HasValue)
                    txtFcID.Text = Util.ConvertToString(anEvent.FcId);
                txtProgramStageId.Text = Util.ConvertToString(anEvent.ProgramStageId);
                txtEventTypeCd.Text = anEvent.EventTypeCd;
                txtEventDt.Text = Util.ConvertToString(anEvent.EventDt);
                txtRpcInd.Text = anEvent.RpcInd;
                txtEventOutcomeCd.Text = anEvent.EventOutcomeCd;
                txtCompletedInd.Text = anEvent.CompletedInd;
                txtCounselorIdRef.Text = anEvent.CounselorIdRef;
                if (anEvent.ProgramRefusalDt.HasValue)
                    txtProgramRefusalDt.Text = Util.ConvertToString(anEvent.ProgramRefusalDt);
                txtWorkingUserID.Text = anEvent.ChgLstUserId;
            }
        }
        private EventDTO FormToEvent()
        {
            EventDTO anEvent = new EventDTO();
            anEvent.FcId = Util.ConvertToInt(txtFcID.Text.Trim());
            anEvent.ProgramStageId = Util.ConvertToInt(txtProgramStageId.Text.Trim());
            anEvent.EventTypeCd = txtEventTypeCd.Text.Trim();
            anEvent.EventDt = Util.ConvertToDateTime(txtEventDt.Text.Trim());
            anEvent.RpcInd = txtRpcInd.Text.Trim();
            anEvent.EventOutcomeCd = txtEventOutcomeCd.Text.Trim();
            anEvent.CompletedInd = txtCompletedInd.Text.Trim();
            anEvent.CounselorIdRef = txtCounselorIdRef.Text.Trim();
            anEvent.ProgramRefusalDt = Util.ConvertToDateTime(txtProgramRefusalDt.Text.Trim());
            return anEvent;
        }
        private EventSaveRequest CreateEventSaveRequest()
        {
            EventSaveRequest request = new EventSaveRequest();
            EventDTO anEvent = new EventDTO();
            anEvent = FormToEvent();
            request.Event = anEvent;
            request.Event.ChgLstUserId = txtWorkingUserID.Text.Trim();
            return request;
        }
        private static XDocument GetXmlDocument(string filename)
        {
            XDocument xdoc = XDocument.Load(filename);
            return xdoc;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EventSaveRequest request = CreateEventSaveRequest();
            EventSaveResponse response;

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            AgencyWebService proxy = new AgencyWebService();
            proxy.AuthenticationInfoValue = ai;

            response = proxy.SaveEvent(request);
            if (response.Status != ResponseStatus.Success)
            {
                if (response.Status == ResponseStatus.Warning)
                {
                    lblMessage.Text = "Congratulation - EventId is " + response.EventId;
                }
                else
                    lblMessage.Text = "Error Message: ";
                grdvMessages.Visible = true;
                grdvMessages.DataSource = response.Messages;
                grdvMessages.DataBind();
            }
            else
            {
                grdvMessages.Visible = false;
                lblMessage.Text = "Congratulation - EventId is " + response.EventId;
            }
        }
    }
}
