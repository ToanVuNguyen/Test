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
                        evalSection.EvalSectionQuestions.Add(evalSectionQuestion);
                    }
                    evalTemplateSection.EvalSection = evalSection;
                    evalTemplateSection.SectionOrder = ConvertToInt(sNode.SelectSingleNode("section_order").InnerText);
                    result.EvalTemplateSections.Add(evalTemplateSection);
                }
            }
            return result;
        }
    }
}
