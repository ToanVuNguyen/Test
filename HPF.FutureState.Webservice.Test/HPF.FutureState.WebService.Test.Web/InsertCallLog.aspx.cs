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

using System.Data.SqlClient;

using HPF.Webservice.CallCenter;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class InsertCallLog : System.Web.UI.Page
    {
        public static int _pageloadno = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                _pageloadno++;
                string filename = ConfigurationManager.AppSettings["CallLogWSDTOXML"];
                XDocument xdoc = XDocument.Load(filename);
                Session[SessionVariables.CALLLOG_WS] = CallCenterHelper.ParseCallLogWSDTO(xdoc);
                CallLogWSDTOToForm((CallLogWSDTO)Session[SessionVariables.CALLLOG_WS]);
            }
            
        }

        private void CallLogWSDTOToForm(CallLogWSDTO aCallLogWS)
        {
            ArrayList al = Get_CallCenterID();

            if (al.Count == 0)
            {
                Create_CallCenter_TestData();
            }

            al = Get_CallCenterID();
            //if (al.Count > 0)
            //{
                aCallLogWS.CallCenterID = Util.ConvertToInt(al[0].ToString().Trim());
                aCallLogWS.CallCenter = al[1].ToString();
                //txtCallCenterID.Text = al[0].ToString();
                //txtCallCenterID.Enabled = false;
                //txtCallCenter.Text = al[1].ToString();
                //txtCallCenter.Enabled = false;
            //}

            txtCallCenterID.Text = aCallLogWS.CallCenterID.ToString();
            txtCallCenter.Text = aCallLogWS.CallCenter;
            txtAuthorizedInd.Text = aCallLogWS.AuthorizedInd;
            txtCallSourceCd.Text = aCallLogWS.CallSourceCd;
            txtCcAgentIdKey.Text = aCallLogWS.CcAgentIdKey; //"CCAgentIdKey" + _pageloadno.ToString();
            txtCcCallKey.Text = aCallLogWS.CcCallKey; //"CcCallKey" + _pageloadno.ToString();
            txtDNIS.Text = aCallLogWS.DNIS; //"DNIS" + _pageloadno.ToString();
            txtEndDate.SelectedDate = aCallLogWS.EndDate;
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
            txtStartDate.SelectedDate = aCallLogWS.StartDate; //DateTime.Now;
            txtTransNumber.Text = aCallLogWS.TransNumber; //"TransNumber" + _pageloadno.ToString();
            

        }

        private CallLogWSDTO FormToCallLogWSDTO()
        {
            CallLogWSDTO aWSCallLog = new CallLogWSDTO();

            #region set value
            aWSCallLog.AuthorizedInd = txtAuthorizedInd.Text.Trim();
            aWSCallLog.CallCenter = txtCallCenter.Text.Trim();            
            aWSCallLog.CallCenterID = Util.ConvertToInt(txtCallCenterID.Text.Trim());
            aWSCallLog.CallSourceCd = txtCallSourceCd.Text.Trim();
            aWSCallLog.CcCallKey = txtCcCallKey.Text.Trim();
            aWSCallLog.CcAgentIdKey = txtCcAgentIdKey.Text.Trim();            
            aWSCallLog.DNIS = txtDNIS.Text.Trim();
            aWSCallLog.EndDate = txtEndDate.SelectedDate;
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
            aWSCallLog.StartDate = txtStartDate.SelectedDate;
            aWSCallLog.TransNumber = txtTransNumber.Text.Trim();

            #endregion

            return aWSCallLog;
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CallLogInsertRequest request = new CallLogInsertRequest();
            CallLogWSDTO aWSCallLog = FormToCallLogWSDTO();

            CallCenterService proxy = new CallCenterService();

            request.CallLog = aWSCallLog;
            CallLogInsertResponse response = proxy.SaveCallLog(request);

            if (response.Status == ResponseStatus.Success)
            {
                lblResult.Text = response.CallLogID;
                _pageloadno++;
                lstMessage.Items.Add("page load: " + _pageloadno);
                //CallLogWSDTOToForm();

                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
            }
            else
            {
                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();

            }

        }

        private void Create_CallCenter_TestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Insert Into Call_Center(call_center_name, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                   " values ('aaaaa', '1/1/2208', 'test', 'test', '1/1/2008', 'test', 'test')";
            command.ExecuteNonQuery();
        }

        private ArrayList Get_CallCenterID()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            ArrayList al = new ArrayList();
            int id = 0;
            command.CommandText = "Select Max(call_center_id), call_center_name from call_center Group by call_center_name";
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();                
                var obj = reader.GetValue(0);
                
                id = 0;
                int.TryParse(obj.ToString(), out id);
                al.Add(id);
                al.Add(reader.GetString(1));                
            }
            reader.Close();
            dbConnection.Close();

            return al;
        }

       
    }
}
