using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects.BillingAdmin;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace HPF.FutureState.DataAccess.BillingAdmin
{
    public class AppForeclosureCaseDAO:BaseDAO
    {
        public static AppForeclosureCaseDAO CreateInstance()
        {
            return new AppForeclosureCaseDAO();
        }
        public AppForeclosureCaseSearchResult AppSearchForeclosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        { 
            AppForeclosureCaseSearchResult results = new AppForeclosureCaseSearchResult();
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_app_foreclosure_case_search", dbConnection);

            var sqlParam = new SqlParameter[11];
            sqlParam[0] = new SqlParameter("@pi_last4SSN", searchCriteria.Last4SSN);
            sqlParam[1] = new SqlParameter("@pi_fname", searchCriteria.FirstName);
            sqlParam[2] = new SqlParameter("@pi_lname", searchCriteria.LastName);
            sqlParam[3] = new SqlParameter("@pi_fc_id", searchCriteria.ForeclosureCaseID);
            sqlParam[4] = new SqlParameter("@pi_agencycaseid", searchCriteria.AgencyCaseID);
            sqlParam[5] = new SqlParameter("@pi_loannum", searchCriteria.LoanNumber);
            sqlParam[6] = new SqlParameter("@pi_propzip", searchCriteria.PropertyZip);
            sqlParam[7] = new SqlParameter("@pi_propstate", searchCriteria.PropertyState);
            sqlParam[8] = new SqlParameter("@pi_duplicate", searchCriteria.Duplicates);
            sqlParam[9] = new SqlParameter("@pi_agencyid", searchCriteria.Agency);
            sqlParam[10] = new SqlParameter("@pi_programid ", searchCriteria.Program);

            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AppForeclosureCaseSearchResult item = new AppForeclosureCaseSearchResult();
                        item.CaseID = ConvertToString(reader["fc_id"]);
                        item.AgencyCaseID = ConvertToString(reader["agency_id"]);
                        item.CaseCompleteDate = ConvertToString(reader["completed_dt"]);
                        item.CaseDate = ConvertToString(reader["intake_dt"]);
                        item.BorrowerFirstName = ConvertToString(reader["borrower_fname"]);
                        item.BorrowerLastName = ConvertToString(reader["borrower_lname"]);
                        item.Last4SSN = ConvertToString(reader["borrower_last4_SSN"]);
                        item.CoborrowerFirstName = ConvertToString(reader["co_borrower_fname"]);
                        item.CoborrowerLastName = ConvertToString(reader["co_borrower_lname"]);
                        item.CoborrowerLast4SSN = ConvertToString(reader["co_borrower_last4_SSN"]);
                        item.PropertyAddress = ConvertToString(reader["prop_addr1"]);
                        item.PropertyCity = ConvertToString(reader["prop_city"]);
                        item.PropertyState = ConvertToString(reader["prop_state_cd"]);
                        item.PropertyZip = ConvertToString(reader["prop_zip"]);
                        item.AgencyName = ConvertToString(reader["agency_name"]);
                        item.AgentName = ConvertToString(reader["counselor_fullname"]);
                        item.AgentEmail = ConvertToString(reader["counselor_email"]);
                        item.AgentPhone = ConvertToString(reader["counselor_phone"]);
                        item.AgentExtension = ConvertToString(reader["counselor_ext"]);
                        item.LoanList = ConvertToString(reader["loan_list"]);
                        item.BankruptcyIndicator = ConvertToString(reader["bankruptcy_ind"]);
                        item.ForeclosureNoticeReceivedIndicator = ConvertToString(reader["fc_notice_received_ind"]);

                        results.Add(item);
                    }
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return results;
            
        }
    }
}
