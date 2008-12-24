using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class ForeclosureCaseDAO : BaseDAO
    {
        public SqlConnection dbConnection;

        public SqlTransaction trans;

        protected ForeclosureCaseDAO()
        {

        }

        public static ForeclosureCaseDAO CreateInstance()
        {
            return new ForeclosureCaseDAO();
        }

        /// <summary>
        /// Begin working
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Commit work.
        /// </summary>
        public void Commit()
        {
            trans.Commit();
            dbConnection.Close();
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            trans.Rollback();
            dbConnection.Close();
        }        

        public CaseLoanDTO GetExistingCaseLoan(CaseLoanDTO caseLoan)
        {
            return null;
        }

        public void InsertCaseLoan(CaseLoanDTO caseLoan)
        {

        }

        public void UpdateCaseLoan(CaseLoanDTO caseLoan)
        {

        }

        /// <summary>
        /// return all search results retrieved from database
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria, int pageSize)
        {
            
            ForeclosureCaseSearchResult results = new ForeclosureCaseSearchResult();
            try
            {
                var dbConnection = base.CreateConnection();
                var command = base.CreateCommand("hpf_foreclosure_case_search_ws", dbConnection);  
                string whereClause = GenerateWhereClause(searchCriteria);
                //<Parameter>
                var sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", searchCriteria.AgencyCaseNumber );
                sqlParam[1] = new SqlParameter("@pi_borrower_fname", searchCriteria.FirstName);
                sqlParam[2] = new SqlParameter("@pi_borrower_lname", searchCriteria.LastName);
                sqlParam[3] = new SqlParameter("@pi_borrower_last4_SSN", searchCriteria.Last4_SSN);
                sqlParam[4] = new SqlParameter("@pi_prop_zip", searchCriteria.PropertyZip);
                sqlParam[5] = new SqlParameter("@pi_loan_number", searchCriteria.LoanNumber);
                sqlParam[6] = new SqlParameter("@pi_where_clause", whereClause);
                sqlParam[7] = new SqlParameter("@pi_number_of_rows", pageSize);
                sqlParam[8] = new SqlParameter("@po_total_rows", SqlDbType.Int) { Direction = ParameterDirection.Output };

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
            
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new ForeclosureCaseSearchResult();
                    while (reader.Read())
                    {
                        ForeclosureCaseWSDTO item = new ForeclosureCaseWSDTO();
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.IntakeDt = ConvertToDateTime(reader["intake_dt"]);
                        item.BorrowerFname = ConvertToString(reader["borrower_fname"]);
                        item.BorrowerLname = ConvertToString(reader["borrower_lname"]);
                        item.BorrowerLast4SSN= ConvertToString(reader["borrower_last4_SSN"]);
                        item.CoBorrowerFname = ConvertToString(reader["co_borrower_fname"]);
                        item.CoBorrowerLname = ConvertToString(reader["co_borrower_lname"]);
                        item.CoBorrowerLast4SSN = ConvertToString(reader["co_borrower_last4_SSN"]);
                        item.PropAddr1 = ConvertToString(reader["prop_addr1"]);
                        item.PropAddr2 = ConvertToString(reader["prop_addr2"]);
                        item.PropCity = ConvertToString(reader["prop_city"]);
                        item.PropStateCd = ConvertToString(reader["prop_state_cd"]);
                        item.PropZip = ConvertToString(reader["prop_zip"]);
                        item.AgencyName = ConvertToString(reader["agency_name"]);
                        item.CounselorFullName = ConvertToString(reader["counselor_fname"]) + " " + ConvertToString(reader["counselor_fname"]);
                        item.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        item.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        item.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        item.CompletedDt = ConvertToDateTime(reader["completed_dt"]);
                        item.DelinquentCd = ConvertToString(reader["Delinquent_cd"]);
                        item.BankruptcyInd = ConvertToString(reader["bankruptcy_ind"]);
                        item.FcNoticeReceivedInd = ConvertToString(reader["fc_notice_received_ind"]);
                        item.LoanNumber = ConvertToString(reader["loan_number"]);
                        item.LoanServicer = ConvertToString(reader["loan_servicer"]);
                        item.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        item.CaseLoanID = ConvertToString(reader["case_loan_id"]);
                        item.Counseled = GetCounseledProperty(item.CompletedDt);

                        results.Add(item);
                    }
                    reader.Close();
                }
                dbConnection.Close();
                results.SearchResultCount = ConvertToInt(sqlParam[8].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return results;
        }

        private string GenerateWhereClause(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            StringBuilder whereClause = new StringBuilder();
            whereClause.Append(" WHERE foreClosure_case.agency_id = Agency.agency_id");
            whereClause.Append(" AND foreclosure_case.fc_id = case_loan.fc_id");
            whereClause.Append(" AND case_loan.servicer_id = servicer.servicer_id");
            whereClause.Append(" AND case_loan.loan_1st_2nd_cd = '1st'");
            whereClause.Append((searchCriteria.AgencyCaseNumber == null) ? "" : " AND agency_case_num = @pi_agency_case_num");
            whereClause.Append((searchCriteria.FirstName == null) ? "" : " AND (borrower_fname like @pi_borrower_fname OR co_borrower_fname like @pi_borrower_fname)");
            whereClause.Append((searchCriteria.LastName == null) ? "" : " AND (borrower_lname like @pi_borrower_lname OR co_borrower_lname like @pi_borrower_lname)");
            whereClause.Append((searchCriteria.Last4_SSN == null) ? "" : " AND (borrower_last4_SSN = @pi_borrower_last4_SSN OR co_borrower_last4_SSN = @pi_borrower_last4_SSN)");
            whereClause.Append((searchCriteria.LoanNumber == null) ? "" : " AND loan_list like @pi_loan_number");
            whereClause.Append((searchCriteria.PropertyZip == null) ? "" : " AND prop_zip = @pi_prop_zip");
           

            return whereClause.ToString();        
        }

        private string GetCounseledProperty(DateTime completedDt)
        {
            DateTime oneYearBefore = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day - 1);
            if (completedDt.CompareTo(oneYearBefore) >= 0 && completedDt.CompareTo(DateTime.Now) <= 0)
                return "<1yr";
            else
                return ">1yr";
        }
    }
}

