using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data;

namespace HPF.FutureState.DataAccess
{
    public class EvalTemplateSectionQuestionDAO:BaseDAO
    {
        public SqlConnection dbConnection;
        public SqlTransaction trans;
        public static EvalTemplateSectionQuestionDAO CreateInstance()
        {
            return new EvalTemplateSectionQuestionDAO();
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
        public EvalTemplateSectionDTOCollection GetEvalTemplateSectionByTemplateId(int? evalTemplateId)
        {
            EvalTemplateSectionDTOCollection result = new EvalTemplateSectionDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_template_get_section_all", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalTemplateId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalTemplateSectionDTO evalTemplateSection = new EvalTemplateSectionDTO();
                        evalTemplateSection.EvalSection = new EvalSectionDTO();
                        evalTemplateSection.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        evalTemplateSection.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalTemplateSection.SectionOrder = ConvertToInt(reader["section_order"]);
                        evalTemplateSection.EvalSection.SectionName = ConvertToString(reader["section_name"]);
                        evalTemplateSection.IsInUse = (ConvertToInt(reader["count_in_use"]) > 0 ? true : false);
                        result.Add(evalTemplateSection);
                    }
                }
                reader.NextResult();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalTemplateSectionDTO evalTemplateSection = new EvalTemplateSectionDTO();
                        evalTemplateSection.EvalSection = new EvalSectionDTO();
                        evalTemplateSection.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        evalTemplateSection.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalTemplateSection.SectionOrder = ConvertToInt(reader["section_order"]);
                        evalTemplateSection.EvalSection.SectionName = ConvertToString(reader["section_name"]);
                        evalTemplateSection.IsInUse = (ConvertToInt(reader["count_in_use"]) > 0 ? true : false);
                        result.Add(evalTemplateSection);
                    }

                }
                reader.Close();

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
        public void UpdateEvalTemplateSection(EvalTemplateSectionDTO evalTemplateSection)
        {
            var command = CreateCommand("hpf_eval_template_section_update", dbConnection);
            var sqlParam = new SqlParameter[6];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalTemplateSection.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalTemplateSection.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_section_order", evalTemplateSection.SectionOrder);

                sqlParam[3] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalTemplateSection.ChangeLastDate));
                sqlParam[4] = new SqlParameter("@pi_chg_lst_user_id", evalTemplateSection.ChangeLastUserId);
                sqlParam[5] = new SqlParameter("@pi_chg_lst_app_name", evalTemplateSection.ChangeLastAppName);
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
        public void InsertEvalTemplateSection(EvalTemplateSectionDTO evalTemplateSection)
        {
            var command = CreateCommand("hpf_eval_template_section_insert", dbConnection);
            var sqlParam = new SqlParameter[9];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalTemplateSection.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalTemplateSection.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_section_order", evalTemplateSection.SectionOrder);

                sqlParam[3] = new SqlParameter("@pi_create_dt", NullableDateTime(evalTemplateSection.CreateDate));
                sqlParam[4] = new SqlParameter("@pi_create_user_id", evalTemplateSection.CreateUserId);
                sqlParam[5] = new SqlParameter("@pi_create_app_name", evalTemplateSection.CreateAppName);
                sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalTemplateSection.ChangeLastDate));
                sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", evalTemplateSection.ChangeLastUserId);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", evalTemplateSection.ChangeLastAppName);
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
        public void RemoveEvalTemplateSection(EvalTemplateSectionDTO evalTemplateSection)
        {
            var command = CreateCommand("hpf_eval_template_section_delete", dbConnection);
            var sqlParam = new SqlParameter[2];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalTemplateSection.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalTemplateSection.EvalSectionId);

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

        public EvalSectionQuestionDTOCollection GetEvalTemplateQuestionByTemplateId(int? evalTemplateId)
        {
            EvalSectionQuestionDTOCollection result = new EvalSectionQuestionDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_eval_template_get_question_all", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalTemplateId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalSectionQuestionDTO evalSectionQuestion = new EvalSectionQuestionDTO();
                        evalSectionQuestion.EvalQuestion = new EvalQuestionDTO();
                        evalSectionQuestion.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        evalSectionQuestion.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalSectionQuestion.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalSectionQuestion.QuestionOrder = ConvertToInt(reader["question_order"]);
                        evalSectionQuestion.EvalQuestion.Question = ConvertToString(reader["question"]);
                        result.Add(evalSectionQuestion);
                    }
                }
                reader.NextResult();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EvalSectionQuestionDTO evalSectionQuestion = new EvalSectionQuestionDTO();
                        evalSectionQuestion.EvalQuestion = new EvalQuestionDTO();
                        evalSectionQuestion.EvalTemplateId = ConvertToInt(reader["eval_template_id"]);
                        evalSectionQuestion.EvalSectionId = ConvertToInt(reader["eval_section_id"]);
                        evalSectionQuestion.EvalQuestionId = ConvertToInt(reader["eval_question_id"]);
                        evalSectionQuestion.QuestionOrder = ConvertToInt(reader["question_order"]);
                        evalSectionQuestion.EvalQuestion.Question = ConvertToString(reader["question"]);
                        result.Add(evalSectionQuestion);
                    }
                }
                reader.Close();
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
        public void UpdateEvalSectionQuestion(EvalSectionQuestionDTO evalSectionQuestion)
        {
            var command = CreateCommand("hpf_eval_section_question_update", dbConnection);
            var sqlParam = new SqlParameter[7];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalSectionQuestion.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalSectionQuestion.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_eval_question_id", evalSectionQuestion.EvalQuestionId);
                sqlParam[3] = new SqlParameter("@pi_question_order", evalSectionQuestion.QuestionOrder);

                sqlParam[4] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalSectionQuestion.ChangeLastDate));
                sqlParam[5] = new SqlParameter("@pi_chg_lst_user_id", evalSectionQuestion.ChangeLastUserId);
                sqlParam[6] = new SqlParameter("@pi_chg_lst_app_name", evalSectionQuestion.ChangeLastAppName);
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
        public void InsertEvalQuestionSection(EvalSectionQuestionDTO evalSectionQuestion)
        {
            var command = CreateCommand("hpf_eval_section_question_insert", dbConnection);
            var sqlParam = new SqlParameter[10];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalSectionQuestion.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalSectionQuestion.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_eval_question_id", evalSectionQuestion.EvalQuestionId);
                sqlParam[3] = new SqlParameter("@pi_question_order", evalSectionQuestion.QuestionOrder);

                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(evalSectionQuestion.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", evalSectionQuestion.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", evalSectionQuestion.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(evalSectionQuestion.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", evalSectionQuestion.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", evalSectionQuestion.ChangeLastAppName);
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
        public void RemoveEvalSectionQuestion(EvalSectionQuestionDTO evalSectionQuestion)
        {
            var command = CreateCommand("hpf_eval_section_question_delete", dbConnection);
            var sqlParam = new SqlParameter[3];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_eval_template_id", evalSectionQuestion.EvalTemplateId);
                sqlParam[1] = new SqlParameter("@pi_eval_section_id", evalSectionQuestion.EvalSectionId);
                sqlParam[2] = new SqlParameter("@pi_eval_question_id", evalSectionQuestion.EvalQuestionId);

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
    }
}
