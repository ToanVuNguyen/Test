using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Xml;

namespace HPF.FutureState.DataAccess
{
    public class CaseEvalHeaderDAO:BaseDAO
    {
        private static readonly CaseEvalHeaderDAO instance = new CaseEvalHeaderDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseEvalHeaderDAO Instance
        {
            get { return instance; }
        }
        protected CaseEvalHeaderDAO() { }

        /// <summary>
        /// Insert a CaseEvalHeader to database.
        /// </summary>
        /// <param name="aCaseEvalHeader">CaseEvalHeaderDTO</param>
        /// <returns>a new CallLogId</returns>
        public int? InsertCaseEvalHeader(CaseEvalHeaderDTO aCaseEvalHeader)
        {

            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_case_eval_header_insert", dbConnection);
            
            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@pi_fc_id", aCaseEvalHeader.FcId);
            sqlParam[1] = new SqlParameter("@pi_agency_id", aCaseEvalHeader.AgencyId);
            sqlParam[2] = new SqlParameter("@pi_eval_template_id", aCaseEvalHeader.EvalTemplateId);
            sqlParam[3] = new SqlParameter("@pi_eval_year_month", aCaseEvalHeader.EvaluationYearMonth);
            sqlParam[4] = new SqlParameter("@pi_eval_type", aCaseEvalHeader.EvalType);
            sqlParam[5] = new SqlParameter("@pi_eval_status", aCaseEvalHeader.EvalStatus);
            
            sqlParam[6] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalHeader.CreateDate));
            sqlParam[7] = new SqlParameter("@pi_create_user_id", aCaseEvalHeader.CreateUserId);
            sqlParam[8] = new SqlParameter("@pi_create_app_name", aCaseEvalHeader.CreateAppName);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalHeader.ChangeLastDate));
            sqlParam[10] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalHeader.ChangeLastUserId);
            sqlParam[11] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalHeader.ChangeLastAppName);

            sqlParam[12] = new SqlParameter("@po_case_eval_header_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>
            #endregion
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                aCaseEvalHeader.CaseEvalHeaderId = ConvertToInt(command.Parameters["@po_case_eval_header_id"].Value);
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return aCaseEvalHeader.CaseEvalHeaderId;
        }
        /// <summary>
        /// Get Case Eval Header By Fc_Id
        /// </summary>
        /// <param name="fc_Id"></param>
        /// <returns>CaseEvalHeaderDTO</returns>
        public CaseEvalHeaderDTO GetCaseEvalHeaderByCaseId(int fc_Id)
        {
            CaseEvalHeaderDTO result = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_header_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fc_Id);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        result = new CaseEvalHeaderDTO();
                        result.CaseEvalHeaderId = ConvertToInt(reader["case_eval_header_id"]);
                        result.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        result.EvalStatus = ConvertToString(reader["eval_status"]);
                        result.EvalType = ConvertToString(reader["eval_type"]);
                        result.EvaluationYearMonth = ConvertToString(reader["eval_year_month"]);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return result;
        }
        /// <summary>
        /// Search Case Eval
        /// </summary>
        /// <param name="caseEvalCriteria"></param>
        /// <returns>CaseEvalSearchResultDTOCollection</returns>
        public CaseEvalSearchResultDTOCollection SearchCaseEval(CaseEvalSearchCriteriaDTO caseEvalCriteria)
        {
            CaseEvalSearchResultDTOCollection results = new CaseEvalSearchResultDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_search", dbConnection);
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_agency_id", caseEvalCriteria.AgencyId);
            sqlParam[1] = new SqlParameter("@pi_eval_year_month_from", caseEvalCriteria.YearMonthFrom);
            sqlParam[2] = new SqlParameter("@pi_eval_year_month_to", caseEvalCriteria.YearMonthTo);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CaseEvalSearchResultDTO result = new CaseEvalSearchResultDTO();
                        result.FcId = ConvertToInt(reader["fc_id"]);
                        result.CaseEvalHeaderId = ConvertToInt(reader["case_eval_header_id"]);
                        result.EvalStatus = ConvertToString(reader["eval_status"]);
                        result.AgencyName = ConvertToString(reader["agency_name"]);
                        result.CounselorName = ConvertToString(reader["counselor_name"]);
                        result.HomeowenerFirstName = ConvertToString(reader["borrower_fname"]);
                        result.HomeowenerLastName = ConvertToString(reader["borrower_lname"]);
                        result.ServicerName = ConvertToString(reader["servicer_name"]);
                        result.ZipCode = ConvertToString(reader["zip_code"]);
                        result.LoanNumber = ConvertToString(reader["loan_number"]);

                        results.Add(result);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return results;
        }
        public CaseEvalSearchResultDTO SearchCaseEvalByFcId(int fcId)
        {
            CaseEvalSearchResultDTO result = new CaseEvalSearchResultDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_search_by_fc_id", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        result.FcId = ConvertToInt(reader["fc_id"]);
                        result.CaseEvalHeaderId = ConvertToInt(reader["case_eval_header_id"]);
                        result.EvalStatus = ConvertToString(reader["eval_status"]);
                        result.EvaluationYearMonth = ConvertToString(reader["eval_year_month"]);
                        result.AgencyName = ConvertToString(reader["agency_name"]);
                        result.CounselorName = ConvertToString(reader["counselor_name"]);
                        result.HomeowenerFirstName = ConvertToString(reader["borrower_fname"]);
                        result.HomeowenerLastName = ConvertToString(reader["borrower_lname"]);
                        result.ServicerName = ConvertToString(reader["servicer_name"]);
                        result.ZipCode = ConvertToString(reader["zip_code"]);
                        result.LoanNumber = ConvertToString(reader["loan_number"]);
                        result.CallDate = ConvertToDateTime(reader["start_dt"]);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return result;
        }
        public CaseEvalFileDTOCollection GetCaseEvalFileByEvalHeaderIdAll(int? evalHeaderId)
        {
            CaseEvalFileDTOCollection result = new CaseEvalFileDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_file_get_all_by_header_id", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_case_eval_header_id", evalHeaderId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CaseEvalFileDTO evalFile = new CaseEvalFileDTO();
                        evalFile.CaseEvalHeaderId = evalHeaderId;
                        evalFile.FileName = ConvertToString(reader["file_name"]);
                        evalFile.FilePath = ConvertToString(reader["file_path"]);
                        result.Add(evalFile);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return result;
        }
    }
}
