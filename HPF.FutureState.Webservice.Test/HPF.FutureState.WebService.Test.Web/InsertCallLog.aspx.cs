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
                lstMessage.Items.Add("page load: " + _pageloadno);
                SetSampleData();
                
            }
            
        }

        private void SetSampleData()
        {
            ArrayList al = Get_CallCenterID();
            
            txtAuthorizedInd.Text = "Y";
            if (al.Count > 0)
            {
                txtCallCenterID.Text = al[0].ToString();
                //txtCallCenterID.Enabled = false;
                txtCallCenter.Text = al[1].ToString();
                //txtCallCenter.Enabled = false;
            }
            txtCallSourceCd.Text = "1";
            txtCcAgentIdKey.Text = "CCAgentIdKey" + _pageloadno.ToString();
            txtCcCallKey.Text = "CcCallKey" + _pageloadno.ToString();
            txtDNIS.Text = "DNIS" + _pageloadno.ToString();
            txtEndDate.SelectedDate = DateTime.Now;
            txtFinalDispoCd.Text = "FinalDispoCd" + _pageloadno.ToString();
            txtFirstName.Text = "FirstName" + _pageloadno.ToString();
            txtHomeownerInd.Text = "Y";
            txtLastName.Text = "LastName" + _pageloadno.ToString();
            txtLoanAccountNumber.Text = "LoanAccountNumber" + _pageloadno.ToString();
            txtLoanDelinqStatusCd.Text = "0123456789";
            txtOtherServicerName.Text = "OtherServicerName" + _pageloadno.ToString();

            txtPowerOfAttorneyInd.Text = "Y";
            txtPrevAgencyId.Text = "PrevAgencyId" + _pageloadno.ToString();
            txtPropZipFull9.Text = "123456789";
            txtReasonToCall.Text = "ReasonToCall" + _pageloadno.ToString();
            txtScreenRout.Text = "ScreenRout" + _pageloadno.ToString();
            txtSelectedAgencyId.Text = "SelectedAgencyId" + _pageloadno.ToString();
            txtSelectedCounselor.Text = "SelectedCounselor" + _pageloadno.ToString();
            txtServiceID.Text = "1";
            txtStartDate.SelectedDate = DateTime.Now;
            txtTransNumber.Text = "TransNumber" + _pageloadno.ToString();
            

        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CallLogInsertRequest request = new CallLogInsertRequest();
            CallLogWSDTO aWSCallLog = GetCallLogWSDTO();

            CallCenterService proxy = new CallCenterService();

            request.CallLog = aWSCallLog;
            CallLogInsertResponse response = proxy.SaveCallLog(request);

            if (response.Status == ResponseStatus.Success)
            {
                lblResult.Text = response.CallLogID;
                _pageloadno++;
                lstMessage.Items.Add("page load: " + _pageloadno);
                SetSampleData();

                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
            }
            else
            {
                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
                
            }         

        }

        private CallLogWSDTO GetCallLogWSDTO()
        {
            CallLogWSDTO aWSCallLog = new CallLogWSDTO();


            #region set value
            aWSCallLog.AuthorizedInd = txtAuthorizedInd.Text.Trim();
            
            aWSCallLog.CallCenter = txtCallCenter.Text.Trim();

            int temp = 0;
            int.TryParse(txtCallCenterID.Text.Trim(), out temp);
            aWSCallLog.CallCenterID = temp;
            aWSCallLog.CallSourceCd = txtCallSourceCd.Text.Trim();
            aWSCallLog.CcCallKey = txtCcCallKey.Text.Trim();
            aWSCallLog.CcAgentIdKey = txtCcAgentIdKey.Text.Trim();            
            aWSCallLog.DNIS = txtDNIS.Text.Trim();
            aWSCallLog.EndDate = txtEndDate.SelectedDate;
            int.TryParse(txtFinalDispoCd.Text.Trim(), out temp);
            aWSCallLog.FinalDispoCd = temp;            
            aWSCallLog.FirstName = txtFirstName.Text.Trim();
            aWSCallLog.HomeownerInd = txtHomeownerInd.Text.Trim();
            aWSCallLog.LastName = txtLastName.Text.Trim();
            aWSCallLog.LoanAccountNumber = txtLoanAccountNumber.Text.Trim();
            aWSCallLog.LoanDelinqStatusCd = txtLoanDelinqStatusCd.Text.Trim();
            aWSCallLog.OtherServicerName = txtOtherServicerName.Text.Trim();
            aWSCallLog.PowerOfAttorneyInd = txtPowerOfAttorneyInd.Text.Trim();
            int.TryParse(txtPrevAgencyId.Text.Trim(), out temp);
            aWSCallLog.PrevAgencyId = temp;            
            aWSCallLog.PropZipFull9 = txtPropZipFull9.Text.Trim();
            aWSCallLog.ReasonToCall = txtReasonToCall.Text.Trim();
            aWSCallLog.ScreenRout = txtScreenRout.Text.Trim();
            aWSCallLog.SelectedAgencyId = txtSelectedAgencyId.Text.Trim();
            aWSCallLog.SelectedCounselor = txtSelectedCounselor.Text.Trim();
            int.TryParse(txtServiceID.Text.Trim(), out temp);
            aWSCallLog.ServicerId = temp;
            aWSCallLog.StartDate = txtStartDate.SelectedDate;
            aWSCallLog.TransNumber = txtTransNumber.Text.Trim();

#endregion
return aWSCallLog;
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
