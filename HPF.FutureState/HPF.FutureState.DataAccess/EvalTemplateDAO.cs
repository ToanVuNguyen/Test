using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common;
using System.Xml;

namespace HPF.FutureState.DataAccess
{
    public class EvalTemplateDAO:BaseDAO
    {
        private static readonly EvalTemplateDAO instance = new EvalTemplateDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static EvalTemplateDAO Instance
        {
            get { return instance; }
        }
        protected EvalTemplateDAO()
        {
        }
        public EvalTemplateDTOCollection GetEvalTemplateAll()
        {
            EvalTemplateDTOCollection result = new EvalTemplateDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_template_get_all", dbConnection);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalTemplateDTO evalTemplate = new EvalTemplateDTO();
                        evalTemplate.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        evalTemplate.TemplateName = ConvertToString(reader["template_name"]);
                        evalTemplate.TemplateDescription = ConvertToString(reader["template_description"]);
                        result.Add(evalTemplate);
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
        /// Get EvalTemplate detail with section in order and all questions in order
        /// </summary>
        /// <param name="templateId">templateId</param>
        /// <returns>EvalTemplateDTO</returns>
        public EvalTemplateDTO GetEvalTemplateById(int? templateId)
        {
            EvalTemplateDTO result;
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_template_get_detail", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_template_id", templateId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
                result = GetEvalTemplate(doc);
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
        private EvalTemplateDTO GetEvalTemplate(XmlDocument doc)
        {
            EvalTemplateDTO result = new EvalTemplateDTO();
            //The templateId is  exist
            if (!string.IsNullOrEmpty(doc.InnerXml))
            {
                //Get EvalTemplate
                XmlNode tNode = doc.SelectSingleNode("ROOT/TEMPLATE");
                result.EvalTemplateId = ConvertToInt(tNode.SelectSingleNode("eval_template_id").InnerText);
                result.TemplateName = tNode.SelectSingleNode("template_name").InnerText;
                result.TemplateDescription = tNode.SelectSingleNode("template_description").InnerText;
                //Get EvalSections
                XmlNodeList sNodes = tNode.SelectNodes("SECTION");
                foreach (XmlNode sNode in sNodes)
                {
                    EvalSectionDTO evalSection = new EvalSectionDTO();
                    EvalTemplateSectionDTO evalTemplateSection = new EvalTemplateSectionDTO();
                    evalSection.EvalSectionId = ConvertToInt(sNode.SelectSingleNode("eval_section_id").InnerText);
                    evalSection.SectionName = sNode.SelectSingleNode("section_name").InnerText;
                    evalTemplateSection.EvalTemplateId = result.EvalTemplateId;
                    evalTemplateSection.EvalSectionId = evalSection.EvalSectionId;
                    //Get EvalQuestions
                    XmlNodeList qNodes = sNode.SelectNodes("QUESTION");
                    foreach (XmlNode qNode in qNodes)
                    {
                        EvalQuestionDTO evalQuestion = new EvalQuestionDTO();
                        EvalSectionQuestionDTO evalSectionQuestion = new EvalSectionQuestionDTO();
                        evalQuestion.EvalQuestionId = ConvertToInt(qNode.SelectSingleNode("eval_question_id").InnerText);
                        evalQuestion.Question = qNode.SelectSingleNode("question").InnerText;
                        evalQuestion.QuestionDescription = qNode.SelectSingleNode("question_description").InnerText;
                        evalQuestion.QuestionExample = qNode.SelectSingleNode("question_example").InnerText;
                        evalQuestion.QuestionType = qNode.SelectSingleNode("question_type").InnerText;
                        evalQuestion.QuestionScore = ConvertToInt(qNode.SelectSingleNode("question_score").InnerText);

                        evalSectionQuestion.EvalQuestion = evalQuestion;
                        evalSectionQuestion.QuestionOrder = ConvertToInt(qNode.SelectSingleNode("question_order").InnerText);
                        evalSectionQuestion.EvalTemplateId = ConvertToInt(qNode.SelectSingleNode("eval_template_id").InnerText);
                        evalSectionQuestion.EvalQuestionId = evalQuestion.EvalQuestionId;
                        evalSectionQuestion.EvalSectionId = evalSection.EvalSectionId;
                        evalSection.EvalSectionQuestions.Add(evalSectionQuestion);
                    }
                    evalTemplateSection.EvalSection = evalSection;
                    evalTemplateSection.SectionOrder = ConvertToInt(sNode.SelectSingleNode("section_order").InnerText);
                    result.EvalTemplateSections.Add(evalTemplateSection);
                }
            }
            return result;
        }
        public EvalSectionCollectionDTO GetEvalSectionAll()
        {
            EvalSectionCollectionDTO result = new EvalSectionCollectionDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_section_get_all", dbConnection);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalSectionDTO evalSection = new EvalSectionDTO();
                        evalSection.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalSection.SectionName = ConvertToString(reader["section_name"]);
                        result.Add(evalSection);
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
        public int? InsertEvalSection(EvalSectionDTO evalSection)
        {
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_eval_section_insert", dbConnection);
            SqlParameter[] sqlParam = new SqlParameter[10];

            sqlParam[0] = new SqlParameter("@pi_section_name", evalSection.SectionName);
            sqlParam[1] = new SqlParameter("@pi_section_description", evalSection.SectionDescription);
            sqlParam[2] = new SqlParameter("@pi_active_ind", evalSection.ActiveInd);

            sqlParam[3] = new SqlParameter("@pi_create_dt", NullableDateTime(evalSection.CreateDate));
            sqlParam[4] = new SqlParameter("@pi_create_user_id", evalSection.CreateUserId);
            sqlParam[5] = new SqlParameter("@pi_create_app_name", evalSection.CreateAppName);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalSection.ChangeLastDate));
            sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", evalSection.ChangeLastUserId);
            sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", evalSection.ChangeLastAppName);

            sqlParam[9] = new SqlParameter("@po_eval_section_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                command.ExecuteNonQuery();
                evalSection.EvalSectionId = ConvertToInt(command.Parameters["@po_eval_section_id"].Value);
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return evalSection.EvalSectionId;
        }
        public void UpdateEvalSection(EvalSectionDTO evalSection)
        {
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_eval_section_update", dbConnection);
            SqlParameter[] sqlParam = new SqlParameter[7];

            sqlParam[0] = new SqlParameter("@pi_eval_section_id", evalSection.EvalSectionId);
            sqlParam[1] = new SqlParameter("@pi_section_name", evalSection.SectionName);
            sqlParam[2] = new SqlParameter("@pi_section_description", evalSection.SectionDescription);
            sqlParam[3] = new SqlParameter("@pi_active_ind", evalSection.ActiveInd);

            sqlParam[4] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalSection.ChangeLastDate));
            sqlParam[5] = new SqlParameter("@pi_chg_lst_user_id", evalSection.ChangeLastUserId);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_app_name", evalSection.ChangeLastAppName);

            command.Parameters.AddRange(sqlParam);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public EvalQuestionDTOCollection GetEvalQuestionAll()
        {
            EvalQuestionDTOCollection result = new EvalQuestionDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_question_get_all", dbConnection);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalQuestionDTO evalQuestion = new EvalQuestionDTO();
                        evalQuestion.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalQuestion.Question = ConvertToString(reader["question"]);
                        result.Add(evalQuestion);
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
        public int? InsertEvalQuestion(EvalQuestionDTO evalQuestion)
        {
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_eval_question_insert", dbConnection);
            SqlParameter[] sqlParam = new SqlParameter[13];

            sqlParam[0] = new SqlParameter("@pi_question", evalQuestion.Question);
            sqlParam[1] = new SqlParameter("@pi_question_description", evalQuestion.QuestionDescription);
            sqlParam[2] = new SqlParameter("@pi_question_example", evalQuestion.QuestionExample);
            sqlParam[3] = new SqlParameter("@pi_question_type", evalQuestion.QuestionType);
            sqlParam[4] = new SqlParameter("@pi_question_score", evalQuestion.QuestionScore);
            sqlParam[5] = new SqlParameter("@pi_active_ind", evalQuestion.ActiveInd);

            sqlParam[6] = new SqlParameter("@pi_create_dt", NullableDateTime(evalQuestion.CreateDate));
            sqlParam[7] = new SqlParameter("@pi_create_user_id", evalQuestion.CreateUserId);
            sqlParam[8] = new SqlParameter("@pi_create_app_name", evalQuestion.CreateAppName);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalQuestion.ChangeLastDate));
            sqlParam[10] = new SqlParameter("@pi_chg_lst_user_id", evalQuestion.ChangeLastUserId);
            sqlParam[11] = new SqlParameter("@pi_chg_lst_app_name", evalQuestion.ChangeLastAppName);

            sqlParam[12] = new SqlParameter("@po_eval_question_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                command.ExecuteNonQuery();
                evalQuestion.EvalQuestionId = ConvertToInt(command.Parameters["@po_eval_question_id"].Value);
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return evalQuestion.EvalQuestionId;
        }
        public void UpdateEvalQuestion(EvalQuestionDTO evalQuestion)
        {
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_eval_section_update", dbConnection);
            SqlParameter[] sqlParam = new SqlParameter[10];

            sqlParam[0] = new SqlParameter("@pi_eval_question_id", evalQuestion.EvalQuestionId);
            sqlParam[1] = new SqlParameter("@pi_question", evalQuestion.Question);
            sqlParam[2] = new SqlParameter("@pi_question_description", evalQuestion.QuestionDescription);
            sqlParam[3] = new SqlParameter("@pi_question_example", evalQuestion.QuestionExample);
            sqlParam[4] = new SqlParameter("@pi_question_type", evalQuestion.QuestionType);
            sqlParam[5] = new SqlParameter("@pi_question_score", evalQuestion.QuestionScore);
            sqlParam[6] = new SqlParameter("@pi_active_ind", evalQuestion.ActiveInd);

            sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalQuestion.ChangeLastDate));
            sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", evalQuestion.ChangeLastUserId);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", evalQuestion.ChangeLastAppName);

            command.Parameters.AddRange(sqlParam);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
