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
            SqlTransaction trans = null;

            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[12];
            sqlParam[0] = new SqlParameter("@pi_fc_id", aCaseEvalHeader.FcId);
            sqlParam[1] = new SqlParameter("@pi_agency_id", aCaseEvalHeader.AgencyId);
            sqlParam[2] = new SqlParameter("@pi_eval_template_id", aCaseEvalHeader.EvalTemplateId);
            sqlParam[3] = new SqlParameter("pi_eval_type", aCaseEvalHeader.EvalType);
            sqlParam[4] = new SqlParameter("pi_eval_status", aCaseEvalHeader.EvalStatus);

            sqlParam[5] = new SqlParameter("@pi_create_dt", NullableDateTime(aCaseEvalHeader.CreateDate));
            sqlParam[6] = new SqlParameter("@pi_create_user_id", aCaseEvalHeader.CreateUserId);
            sqlParam[7] = new SqlParameter("@pi_create_app_name", aCaseEvalHeader.CreateAppName);
            sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCaseEvalHeader.ChangeLastDate));
            sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", aCaseEvalHeader.ChangeLastUserId);
            sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", aCaseEvalHeader.ChangeLastAppName);

            sqlParam[11] = new SqlParameter("@po_case_eval_header_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
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
        /// <summary>
        /// Get CaseEval Latest Set
        /// </summary>
        /// <param name="fc_Id">fc_Id</param>
        /// <returns>CaseEvalHeaderDTO</returns>
        public CaseEvalHeaderDTO GetCaseEvalLatestSet(int fc_Id)
        {
            CaseEvalHeaderDTO result;
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_get_latest_set", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fc_Id);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
                result = GetCaseEvalHeader(doc);
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
        /// Get All CaseEval Set
        /// </summary>
        /// <param name="fc_Id">fc_Id</param>
        /// <returns>CaseEvalHeaderDTO</returns>
        public CaseEvalHeaderDTO GetCaseEvalAllSet(int fc_Id)
        {
            CaseEvalHeaderDTO result;
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_case_eval_get_all_set", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fc_Id);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
                result = GetCaseEvalHeader(doc);
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
        private CaseEvalHeaderDTO GetCaseEvalHeader(XmlDocument doc)
        {
            CaseEvalHeaderDTO result = new CaseEvalHeaderDTO();
            //If fc_id is exsit
            if (!string.IsNullOrEmpty(doc.InnerXml))
            {
                //Get EvalHeader
                XmlNode hNode = doc.SelectSingleNode("ROOT/CASEEVAL");
                result.FcId = ConvertToInt(hNode.SelectSingleNode("fc_id").InnerText);
                result.EvalStatus = hNode.SelectSingleNode("eval_status").InnerText;
                result.CallDate = ConvertToDateTime(hNode.SelectSingleNode("call_date").InnerText);
                result.AgencyName = hNode.SelectSingleNode("agency_name").InnerText;
                result.ZipCode = hNode.SelectSingleNode("zip_code").InnerText;
                result.LoanNumber = hNode.SelectSingleNode("loan_number").InnerText;
                //Get EvalSet
                XmlNodeList sNodes = hNode.SelectNodes("EVALSET");
                foreach (XmlNode sNode in sNodes)
                {
                    CaseEvalSetDTO evalSet = new CaseEvalSetDTO();
                    evalSet.CaseEvalSetId =ConvertToInt(sNode.SelectSingleNode("case_eval_set_id"));
                    evalSet.EvaluationDt = ConvertToDateTime(sNode.SelectSingleNode("evaluation_dt").InnerText);
                    evalSet.AuditorName = sNode.SelectSingleNode("auditor_name").InnerText;
                    evalSet.TotalAuditScore =ConvertToInt(sNode.SelectSingleNode("total_audit_score").InnerText);
                    evalSet.TotalPossibleScore =ConvertToInt(sNode.SelectSingleNode("total_possible_score").InnerText);
                    evalSet.ResultLevel = sNode.SelectSingleNode("result_level").InnerText;
                    evalSet.FatalErrorInd = ConvertToBool(sNode.SelectSingleNode("fatal_error_ind").InnerText);
                    evalSet.HpfAuditInd = ConvertToBool(sNode.SelectSingleNode("hpf_audit_ind").InnerText);
                    evalSet.Comments = sNode.SelectSingleNode("comments").InnerText;
                    //Get Section
                    XmlNodeList sectionNodes = sNode.SelectNodes("SECTION");
                    foreach (XmlNode sectionNode in sectionNodes)
                    {
                        EvalSectionDTO section = new EvalSectionDTO();
                        section.SectionName = sectionNode.SelectSingleNode("section_name").InnerText;
                        section.SectionDescription = sectionNode.SelectSingleNode("section_description").InnerText;
                        //Get EvalDetail - Question and Answer
                        XmlNodeList dNodes = sectionNode.SelectNodes("QUESTION");
                        foreach (XmlNode dNode in dNodes)
                        {
                            CaseEvalDetailDTO evalDetail = new CaseEvalDetailDTO();
                            evalDetail.CaseEvalDetailId = ConvertToInt(dNode.SelectSingleNode("case_eval_detail_id").InnerText);
                            evalDetail.EvalQuestion = dNode.SelectSingleNode("eval_question").InnerText;
                            evalDetail.EvalAnswer = dNode.SelectSingleNode("eval_answer").InnerText;
                            evalDetail.QuestionScore =ConvertToInt(dNode.SelectSingleNode("question_score").InnerText);
                            evalDetail.AuditScore = ConvertToInt(dNode.SelectSingleNode("audit_score").InnerText);
                            evalDetail.Comments = dNode.SelectSingleNode("comments").InnerText;

                            section.CaseEvalDetails.Add(evalDetail);
                        }
                        evalSet.EvalSections.Add(section);
                    }
                    result.CaseEvalSets.Add(evalSet);
                }
            }
            return result;
        }
    }
}
