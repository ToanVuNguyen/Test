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



namespace HPF.FutureState.WebService.Test.Web
{
    public partial class InsertCallLog : System.Web.UI.Page
    {
        public static int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CallCenterService proxy = new CallCenterService();
            
            i++;
            lstMessage.Items.Add("page load: " + i);
            ArrayList al = Get_CallCenterID();
            txtCallCenterID.Text = al[0].ToString();
            txtCallCenter.Text = al[1].ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CallLogInsertRequest request = new CallLogInsertRequest();
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

            CallCenterService proxy = new CallCenterService();

            request.CallLog = aWSCallLog;
            CallLogInsertResponse response = proxy.SaveCallLog(request);

            if (response.Status == ResponseStatus.Success)
                lblResult.Text = response.CallLogID;
            else
            {
                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
            }         

        }

        int _id = -1;
        protected void btnGenerateTestData_Click(object sender, EventArgs e)
        {
            lstMessage.Items.Add("Generating testing data ...");
            lstMessage.Items.Add("=>Generating CallCenter ...");
            GenerateTestData_CallCenter();
            ArrayList al = Get_CallCenterID();
            txtCallCenterID.Text = al[0].ToString();
            txtCallCenter.Text = al[1].ToString();
            
            //txtCallCenterID.Text = _id.ToString();
            lstMessage.Items.Add(string.Format("==>CallCenter {0} is created", al[0].ToString()));
        }

        private void GenerateTestData_CallCenter()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Insert into call_center(call_center_name) values ('call_center_name_1')";
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        private void DeleteTestData_CallCenter()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Delete call_center where call_center_id = " + Get_CallCenterID();//_id;
            command.ExecuteNonQuery();



            dbConnection.Close();
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
                //if (reader.GetSqlInt32(0) == null)
                //    id = 0;
                //else
                //    id = reader.GetInt32(0);
                var obj = reader.GetValue(0);
                
                id = 0; //(reader.GetValue(0)==null) ? 0 : reader.GetInt32(0);
                int.TryParse(obj.ToString(), out id);
                al.Add(id);
                al.Add(reader.GetString(1));
                //id = (int)reader.GetSqlInt32(0);
            }
            reader.Close();
            dbConnection.Close();

            return al;
        }

        protected void btnDeleteTestData_Click(object sender, EventArgs e)
        {
            lstMessage.Items.Add("Deleting testing data ...");
            DeleteTestData_CallCenter();

            
            
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            int id = 0;
            command.CommandText = "Select count(call_center_id) from call_center";
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                id = reader.GetInt32(0);
            }
            reader.Close();
            dbConnection.Close();

            lstMessage.Items.Add(string.Format("There'r still {0} rows in Call_Center", id));

        }
    }
}
