using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class CaseEvalSetDAO:BaseDAO
    {
        public SqlConnection dbConnection;
        public SqlTransaction trans;
        public static CaseEvalSetDAO CreateInstance()
        {
            return new CaseEvalSetDAO();
        }
        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary>
        /// Commit Working
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        /// <summary>
        /// Rollback working
        /// </summary>
        public void Cancel()
        {
            try
            {
                trans.Rollback();
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }

        public void UpdateCaseEvalHeader(CaseEvalHeaderDTO caseEvalHeader)
        {
            var command = CreateCommand("hpf_case_eval_header_update", dbConnection);
            var sqlParam = new SqlParameter[5];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_header_id",caseEvalHeader.CaseEvalHeaderId);
                sqlParam[1] = new SqlParameter("@pi_eval_status",caseEvalHeader.EvalStatus);

                sqlParam[2] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseEvalHeader.ChangeLastDate));
                sqlParam[3] = new SqlParameter("@pi_chg_lst_user_id", caseEvalHeader.ChangeLastUserId);
                sqlParam[4] = new SqlParameter("@pi_chg_lst_app_name", caseEvalHeader.ChangeLastAppName);
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void UpdateCaseEvalSet(CaseEvalSetDTO caseEvalSet)
        {
            var command = CreateCommand("hpf_case_eval_set_update", dbConnection);
            var sqlParam = new SqlParameter[11];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_set_id", caseEvalSet.CaseEvalSetId);
                sqlParam[1] = new SqlParameter("@pi_evaluation_dt", NullableDateTime(caseEvalSet.EvaluationDt));
                sqlParam[2] = new SqlParameter("@pi_auditor_name", caseEvalSet.AuditorName);
                sqlParam[3] = new SqlParameter("@pi_total_audit_score", caseEvalSet.TotalAuditScore);
                sqlParam[4] = new SqlParameter("@pi_total_possible_score", caseEvalSet.TotalPossibleScore);
                sqlParam[5] = new SqlParameter("@pi_result_level", caseEvalSet.ResultLevel);
                sqlParam[6] = new SqlParameter("@pi_fatal_error_ind", caseEvalSet.FatalErrorInd);
                sqlParam[7] = new SqlParameter("@pi_comments", caseEvalSet.Comments);

                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseEvalSet.ChangeLastDate));
                sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", caseEvalSet.ChangeLastUserId);
                sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", caseEvalSet.ChangeLastAppName);
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void UpdateCaseEvalDetail(CaseEvalDetailDTO caseEvalDetail)
        {
            var command = CreateCommand("hpf_case_eval_detail_update", dbConnection);
            var sqlParam = new SqlParameter[7];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_detail_id", caseEvalDetail.CaseEvalDetailId);
                sqlParam[1] = new SqlParameter("@pi_eval_answer", caseEvalDetail.EvalAnswer);
                sqlParam[2] = new SqlParameter("@pi_audit_score", caseEvalDetail.AuditScore);
                sqlParam[3] = new SqlParameter("@pi_comments", caseEvalDetail.Comments);

                sqlParam[4] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseEvalDetail.ChangeLastDate));
                sqlParam[5] = new SqlParameter("@pi_chg_lst_user_id", caseEvalDetail.ChangeLastUserId);
                sqlParam[6] = new SqlParameter("@pi_chg_lst_app_name", caseEvalDetail.ChangeLastAppName);
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public int? InsertCaseEvalSet(CaseEvalSetDTO aCaseEvalSet)
        {
            var command = CreateCommand("hpf_case_eval_set_insert", dbConnection);
            var sqlParam = new SqlParameter[16];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_header_id", aCaseEvalSet.CaseEvalHeaderId);
                sqlParam[1] = new SqlParameter("@pi_evaluation_dt", NullableDateTime(aCaseEvalSet.EvaluationDt));
                sqlParam[2] = new SqlParameter("@pi_auditor_name", aCaseEvalSet.AuditorName);
                sqlParam[3] = new SqlParameter("@pi_total_audit_score", aCaseEvalSet.TotalAuditScore);
                sqlParam[4] = new SqlParameter("pi_total_possible_score",aCaseEvalSet.TotalPossibleScore);
                sqlParam[5] = new SqlParameter("@pi_result_level", aCaseEvalSet.ResultLevel);
                sqlParam[6] = new SqlParameter("@pi_fatal_error_ind", aCaseEvalSet.FatalErrorInd);
                sqlParam[7] = new SqlParameter("@pi_hpf_audit_ind", aCaseEvalSet.HpfAuditInd);
                sqlParam[8] = new SqlParameter("@pi_comments", aCaseEvalSet.Comments);

                sqlParam[9] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalSet.CreateDate));
                sqlParam[10] = new SqlParameter("@pi_create_user_id", aCaseEvalSet.CreateUserId);
                sqlParam[11] = new SqlParameter("@pi_create_app_name", aCaseEvalSet.CreateAppName);
                sqlParam[12] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalSet.ChangeLastDate));
                sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalSet.ChangeLastUserId);
                sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalSet.ChangeLastAppName);

                sqlParam[15] = new SqlParameter("@po_case_eval_set_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
                return ConvertToInt(command.Parameters["@po_case_eval_set_id"].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void InsertCaseEvalDetail(CaseEvalDetailDTO aCaseEvalDetail)
        {
            var command = CreateCommand("hpf_case_eval_detail_insert", dbConnection);
            var sqlParam = new SqlParameter[18];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_set_id", aCaseEvalDetail.CaseEvalSetId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", aCaseEvalDetail.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_section_name", aCaseEvalDetail.SectionName);
                sqlParam[3] = new SqlParameter("@pi_section_order", aCaseEvalDetail.SectionOrder);
                sqlParam[4] = new SqlParameter("@pi_eval_question_id", aCaseEvalDetail.EvalQuestionId);
                sqlParam[5] = new SqlParameter("@pi_eval_question", aCaseEvalDetail.EvalQuestion);
                sqlParam[6] = new SqlParameter("@pi_question_example",aCaseEvalDetail.QuestionExample);
                sqlParam[7] = new SqlParameter("@pi_question_order", aCaseEvalDetail.QuestionOrder);
                sqlParam[8] = new SqlParameter("@pi_eval_answer", aCaseEvalDetail.EvalAnswer);
                sqlParam[9] = new SqlParameter("@pi_question_score", aCaseEvalDetail.QuestionScore);
                sqlParam[10] = new SqlParameter("@pi_audit_score", aCaseEvalDetail.AuditScore);
                sqlParam[11] = new SqlParameter("@pi_comments",aCaseEvalDetail.Comments);

                sqlParam[12] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalDetail.CreateDate));
                sqlParam[13] = new SqlParameter("@pi_create_user_id", aCaseEvalDetail.CreateUserId);
                sqlParam[14] = new SqlParameter("@pi_create_app_name", aCaseEvalDetail.CreateAppName);
                sqlParam[15] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalDetail.ChangeLastDate));
                sqlParam[16] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalDetail.ChangeLastUserId);
                sqlParam[17] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalDetail.ChangeLastAppName);

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void InsertCaseEvalFile(CaseEvalFileDTO aCaseEvalFile)
        {
            SqlCommand command = CreateSPCommand("hpf_case_eval_file_insert", dbConnection);
            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@pi_case_eval_header_id", aCaseEvalFile.CaseEvalHeaderId);
            sqlParam[1] = new SqlParameter("@pi_file_name", aCaseEvalFile.FileName);
            sqlParam[2] = new SqlParameter("@pi_file_path", aCaseEvalFile.FilePath);

            sqlParam[3] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalFile.CreateDate));
            sqlParam[4] = new SqlParameter("@pi_create_user_id", aCaseEvalFile.CreateUserId);
            sqlParam[5] = new SqlParameter("@pi_create_app_name", aCaseEvalFile.CreateAppName);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalFile.ChangeLastDate));
            sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalFile.ChangeLastUserId);
            sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalFile.ChangeLastAppName);

            //</Parameter>
            #endregion
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        /// <summary>
        /// Get CaseEval Latest Set for HPF and Agency depending on hpfAuditInd
        /// </summary>
        /// <param name="case_eval_header_id">case_eval_header_id</param>
        /// <param name="hpfAuditInd">hpfAuditInd</param>
        /// <returns>CaseEvalSetDTO</returns>
        public CaseEvalSetDTO GetCaseEvalLatestSet(int? caseEvalHeaderId,string hpfAuditInd)
        {
            CaseEvalSetDTO result = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_get_latest_set", dbConnection);
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_case_eval_header_id", caseEvalHeaderId);
            sqlParam[1] = new SqlParameter("@pi_hpf_audit_ind", hpfAuditInd);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new CaseEvalSetDTO();
                    if (reader.Read())
                    {
                        result.CaseEvalHeaderId = caseEvalHeaderId;
                        result.CaseEvalSetId = ConvertToInt(reader["case_eval_set_id"]);
                        result.EvaluationDt = ConvertToDateTime(reader["evaluation_dt"]);
                        result.AuditorName = ConvertToString(reader["auditor_name"]);
                        result.TotalAuditScore = ConvertToInt(reader["total_audit_score"]);
                        result.TotalPossibleScore = ConvertToInt(reader["total_possible_score"]);
                        result.ResultLevel = ConvertToString(reader["result_level"]);
                        result.FatalErrorInd = ConvertToString(reader["fatal_error_ind"]);
                        result.Comments = ConvertToString(reader["comments"]);
                    }
                    reader.NextResult();

                    //Read Case Eval Details
                    while (reader.Read())
                    {
                        CaseEvalDetailDTO evalDetail = new CaseEvalDetailDTO();
                        evalDetail.CaseEvalSetId = result.CaseEvalSetId;
                        evalDetail.CaseEvalDetailId =ConvertToInt(reader["case_eval_detail_id"]);
                        evalDetail.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalDetail.SectionName = ConvertToString(reader["section_name"]);
                        evalDetail.SectionOrder = ConvertToInt(reader["section_order"]);
                        evalDetail.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalDetail.EvalQuestion = ConvertToString(reader["eval_question"]);
                        evalDetail.QuestionExample = ConvertToString(reader["question_example"]);
                        evalDetail.QuestionOrder = ConvertToInt(reader["question_order"]);
                        evalDetail.EvalAnswer = ConvertToString(reader["eval_answer"]);
                        evalDetail.QuestionScore = ConvertToInt(reader["question_score"]);
                        evalDetail.AuditScore = ConvertToInt(reader["audit_score"]);
                        evalDetail.Comments = ConvertToString(reader["comments"]);

                        result.CaseEvalDetails.Add(evalDetail);
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
        public CaseEvalSetDTOCollection GetCaseEvalLatestSetAll(int fcId)
        {
            CaseEvalSetDTOCollection result = new CaseEvalSetDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_get_all_latest_set", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //Read case_eval_set Latest of Agency
                    CaseEvalSetDTO evalSetLatestAgency = new CaseEvalSetDTO();
                    if (reader.Read())
                    {
                        evalSetLatestAgency.CaseEvalSetId = ConvertToInt(reader["case_eval_set_id"]);
                        evalSetLatestAgency.EvaluationDt = ConvertToDateTime(reader["evaluation_dt"]);
                        evalSetLatestAgency.AuditorName = ConvertToString(reader["auditor_name"]);
                        evalSetLatestAgency.TotalAuditScore = ConvertToInt(reader["total_audit_score"]);
                        evalSetLatestAgency.TotalPossibleScore = ConvertToInt(reader["total_possible_score"]);
                        evalSetLatestAgency.ResultLevel = ConvertToString(reader["result_level"]);
                        evalSetLatestAgency.FatalErrorInd = ConvertToString(reader["fatal_error_ind"]);
                        evalSetLatestAgency.Comments = ConvertToString(reader["comments"]);
                    }
                    reader.NextResult();
                    //Read Case Eval Details
                    while (reader.Read())
                    {
                        CaseEvalDetailDTO evalDetail = new CaseEvalDetailDTO();
                        evalDetail.CaseEvalDetailId = ConvertToInt(reader["case_eval_detail_id"]);
                        evalDetail.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalDetail.SectionName = ConvertToString(reader["section_name"]);
                        evalDetail.SectionOrder = ConvertToInt(reader["section_order"]);
                        evalDetail.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalDetail.EvalQuestion = ConvertToString(reader["eval_question"]);
                        evalDetail.QuestionExample = ConvertToString(reader["question_example"]);
                        evalDetail.QuestionOrder = ConvertToInt(reader["question_order"]);
                        evalDetail.EvalAnswer = ConvertToString(reader["eval_answer"]);
                        evalDetail.QuestionScore = ConvertToInt(reader["question_score"]);
                        evalDetail.AuditScore = ConvertToInt(reader["audit_score"]);
                        evalDetail.Comments = ConvertToString(reader["comments"]);

                        evalSetLatestAgency.CaseEvalDetails.Add(evalDetail);
                    }
                    result.Add(evalSetLatestAgency);
                    reader.NextResult();

                    //Read case_eval_set Latest of HPF
                    CaseEvalSetDTO evalSetLatestHPF = new CaseEvalSetDTO();
                    if (reader.Read())
                    {
                        evalSetLatestHPF.CaseEvalSetId = ConvertToInt(reader["case_eval_set_id"]);
                        evalSetLatestHPF.EvaluationDt = ConvertToDateTime(reader["evaluation_dt"]);
                        evalSetLatestHPF.AuditorName = ConvertToString(reader["auditor_name"]);
                        evalSetLatestHPF.TotalAuditScore = ConvertToInt(reader["total_audit_score"]);
                        evalSetLatestHPF.TotalPossibleScore = ConvertToInt(reader["total_possible_score"]);
                        evalSetLatestHPF.ResultLevel = ConvertToString(reader["result_level"]);
                        evalSetLatestHPF.FatalErrorInd = ConvertToString(reader["fatal_error_ind"]);
                        evalSetLatestHPF.Comments = ConvertToString(reader["comments"]);
                    }
                    reader.NextResult();
                    //Read Case Eval Details
                    while (reader.Read())
                    {
                        CaseEvalDetailDTO evalDetail = new CaseEvalDetailDTO();
                        evalDetail.CaseEvalDetailId = ConvertToInt(reader["case_eval_detail_id"]);
                        evalDetail.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalDetail.SectionName = ConvertToString(reader["section_name"]);
                        evalDetail.SectionOrder = ConvertToInt(reader["section_order"]);
                        evalDetail.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalDetail.EvalQuestion = ConvertToString(reader["eval_question"]);
                        evalDetail.QuestionExample = ConvertToString(reader["question_example"]);
                        evalDetail.QuestionOrder = ConvertToInt(reader["question_order"]);
                        evalDetail.EvalAnswer = ConvertToString(reader["eval_answer"]);
                        evalDetail.QuestionScore = ConvertToInt(reader["question_score"]);
                        evalDetail.AuditScore = ConvertToInt(reader["audit_score"]);
                        evalDetail.Comments = ConvertToString(reader["comments"]);

                        evalSetLatestHPF.CaseEvalDetails.Add(evalDetail);
                    }
                    result.Add(evalSetLatestHPF);
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
