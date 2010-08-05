using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    class CaseEvalHeaderDAO:BaseDAO
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
            SqlTransaction trans = null;

            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@pi_fc_id", aCaseEvalHeader.FcId);
            sqlParam[1] = new SqlParameter("@pi_agency_id", aCaseEvalHeader.AgencyId);
            sqlParam[2] = new SqlParameter("@pi_eval_template_id", aCaseEvalHeader.EvalTemplateId);
            
            sqlParam[3] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalHeader.CreateDate));
            sqlParam[4] = new SqlParameter("@pi_create_user_id", aCaseEvalHeader.CreateUserId);
            sqlParam[5] = new SqlParameter("@pi_create_app_name", aCaseEvalHeader.CreateAppName);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalHeader.ChangeLastDate));
            sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalHeader.ChangeLastUserId);
            sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalHeader.ChangeLastAppName);

            sqlParam[9] = new SqlParameter("@po_case_eval_header_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>
            #endregion

            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
                trans.Commit();
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
    }
}
