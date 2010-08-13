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
        private static readonly CaseEvalSetDAO instance = new CaseEvalSetDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseEvalSetDAO Instance
        {
            get { return instance; }
        }
        protected CaseEvalSetDAO() { }
        public SqlConnection dbConnection;
        public SqlTransaction trans;
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
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
                return ConvertToInt(command.Parameters["po_case_eval_set_id"].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void InsertCaseEvalDetail(CaseEvalDetailDTO aCaseEvalDetail)
        {
            var command = CreateCommand("hpf_case_eval_detail_insert", dbConnection);
            var sqlParam = new SqlParameter[17];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_case_eval_set_id", aCaseEvalDetail.CaseEvalSetId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", aCaseEvalDetail.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_section_name", aCaseEvalDetail.SectionName);
                sqlParam[3] = new SqlParameter("@pi_section_order", aCaseEvalDetail.SectionOrder);
                sqlParam[4] = new SqlParameter("@pi_eval_question_id", aCaseEvalDetail.EvalQuestionId);
                sqlParam[5] = new SqlParameter("@pi_eval_question", aCaseEvalDetail.EvalQuestion);
                sqlParam[6] = new SqlParameter("@pi_question_order", aCaseEvalDetail.QuestionOrder);
                sqlParam[7] = new SqlParameter("@pi_eval_answer", aCaseEvalDetail.EvalAnswer);
                sqlParam[8] = new SqlParameter("@pi_question_score", aCaseEvalDetail.QuestionScore);
                sqlParam[9] = new SqlParameter("@pi_audit_score", aCaseEvalDetail.AuditScore);
                sqlParam[10] = new SqlParameter("@pi_comments",aCaseEvalDetail.Comments);

                sqlParam[11] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalDetail.CreateDate));
                sqlParam[12] = new SqlParameter("@pi_create_user_id", aCaseEvalDetail.CreateUserId);
                sqlParam[13] = new SqlParameter("@pi_create_app_name", aCaseEvalDetail.CreateAppName);
                sqlParam[14] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalDetail.ChangeLastDate));
                sqlParam[15] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalDetail.ChangeLastUserId);
                sqlParam[16] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalDetail.ChangeLastAppName);

                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
    }
}
