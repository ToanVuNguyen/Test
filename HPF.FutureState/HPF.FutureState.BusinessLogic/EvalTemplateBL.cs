using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using System.Xml;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class EvalTemplateBL:BaseBusinessLogic
    {
        private static readonly EvalTemplateBL instance = new EvalTemplateBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static EvalTemplateBL Instance
        {
            get { return instance; }
        }
        protected EvalTemplateBL()
        {
        }
        public enum StatusChanged : byte
        {
            Insert=0,Remove=1,Update=2
        }
        public EvalTemplateDTO RetriveTemplate(int? templateId)
        {
            return EvalTemplateDAO.Instance.GetEvalTemplateById(templateId);
        }
        public EvalTemplateDTOCollection RetriveAllTemplate()
        {
            return EvalTemplateDAO.Instance.GetEvalTemplateAll();
        }
        public EvalSectionCollectionDTO RetriveAllEvalSection()
        {
            return EvalTemplateDAO.Instance.GetEvalSectionAll();
        }
        public int? InsertEvalSection(EvalSectionDTO evalSection)
        {
            if (string.IsNullOrEmpty(evalSection.SectionName))
                throw new Exception("Section name is required!");
            return EvalTemplateDAO.Instance.InsertEvalSection(evalSection);
        }
        public void UpdateEvalSection(EvalSectionDTO evalSection)
        {
            if (evalSection.IsInUse && (evalSection.ActiveInd == Constant.INDICATOR_NO))
                throw new Exception("This section is in used, Can not set it inactive!");
            if (string.IsNullOrEmpty(evalSection.SectionName))
                throw new Exception("Section name is required!");
            EvalTemplateDAO.Instance.UpdateEvalSection(evalSection);
        }
        public EvalQuestionDTOCollection RetriveAllQuestion()
        {
            return EvalTemplateDAO.Instance.GetEvalQuestionAll();
        }
        public int? InsertEvalQuestion(EvalQuestionDTO evalQuestion)
        {
            var ex = ValidateQuestion(evalQuestion);
            if (ex.ExceptionMessages.Count > 0) throw ex;
            return EvalTemplateDAO.Instance.InsertEvalQuestion(evalQuestion);
        }
        public void UpdateEvalQuestion(EvalQuestionDTO evalQuestion)
        {
            var ex = ValidateQuestion(evalQuestion);
            if (evalQuestion.IsInUse && (evalQuestion.ActiveInd == Constant.INDICATOR_NO))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "This section is in used, Can not set it inactive!" });
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            EvalTemplateDAO.Instance.UpdateEvalQuestion(evalQuestion);
        }
        private DataValidationException ValidateQuestion(EvalQuestionDTO evalQuestion)
        {
            DataValidationException ex = new DataValidationException();
            if (string.IsNullOrEmpty(evalQuestion.Question))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Question is required!" });
            if (string.IsNullOrEmpty(evalQuestion.QuestionType))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Question type is reqiured!" });
            return ex;
        }
        public int? InsertEvalTemplate(EvalTemplateDTO evalTemplate)
        {
            if (string.IsNullOrEmpty(evalTemplate.TemplateName))
                throw new Exception("Template name is required!");
            return EvalTemplateDAO.Instance.InsertEvalTemplate(evalTemplate);
        }
        public void UpdateEvalTemplate(EvalTemplateDTO evalTemplate)
        {
            if (evalTemplate.IsInUse && (evalTemplate.ActiveInd == Constant.INDICATOR_NO))
                throw new Exception("This template is in used, Can not set it inactive!");
            if (string.IsNullOrEmpty(evalTemplate.TemplateName))
                throw new Exception("Template name is required!");
            EvalTemplateDAO.Instance.UpdateEvalTemplate(evalTemplate);
        }
        public EvalTemplateSectionDTOCollection RetriveAllEvalSectionsByTemplateId(int? evalTemplateId)
        {
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            return instance.GetEvalTemplateSectionByTemplateId(evalTemplateId);
        }
        public void ManageEvalTemplateSection(EvalTemplateSectionDTOCollection sectionCollection)
        {
            var exValidate = ValidateTemplateSection(sectionCollection);
            if (exValidate.ExceptionMessages.Count > 0) throw exValidate;
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            try
            {
                instance.Begin();
                foreach (EvalTemplateSectionDTO section in sectionCollection)
                {
                    if (section.StatusChanged ==(byte) StatusChanged.Insert)
                        instance.InsertEvalTemplateSection(section);
                    else if (section.StatusChanged ==(byte) StatusChanged.Update)
                        instance.UpdateEvalTemplateSection(section);
                    else 
                        instance.RemoveEvalTemplateSection(section);
                }
            }
            catch (Exception ex)
            {
                instance.Cancel();
                throw ex;
            }
            finally
            {
                instance.Commit();
            }
        }
        public DataValidationException ValidateTemplateSection(EvalTemplateSectionDTOCollection sectionCollection)
        {
            DataValidationException ex = new DataValidationException();
            foreach (EvalTemplateSectionDTO section in sectionCollection)
                if (section.SectionOrder == null)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Section order must is numeric!" });
                    break;
                }
            return ex;
        }

        public EvalSectionQuestionDTOCollection RetriveAllEvalQuestionByTemplateId(int? evalTemplateId)
        {
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            return instance.GetEvalTemplateQuestionByTemplateId(evalTemplateId);
        }
        public void ManageEvalSectionQuestion(EvalSectionQuestionDTOCollection questionCollection)
        {
            var exValidate = ValidateSectionQuestion(questionCollection);
            if (exValidate.ExceptionMessages.Count > 0) throw exValidate;
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            try
            {
                instance.Begin();
                foreach (EvalSectionQuestionDTO question in questionCollection)
                {
                    if (question.StatusChanged == (byte)StatusChanged.Insert)
                        instance.InsertEvalQuestionSection(question);
                    else if (question.StatusChanged == (byte)StatusChanged.Update)
                        instance.UpdateEvalSectionQuestion(question);
                    else
                        instance.RemoveEvalSectionQuestion(question);
                }
            }
            catch (Exception ex)
            {
                instance.Cancel();
                throw ex;
            }
            finally
            {
                instance.Commit();
            }
        }
        public DataValidationException ValidateSectionQuestion(EvalSectionQuestionDTOCollection questionCollection)
        {
            DataValidationException ex = new DataValidationException();
            foreach (EvalSectionQuestionDTO question in questionCollection)
            {
                if (question.EvalSectionId <=0)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Choose section which question belong to!" });
                    break;
                }
                if (question.QuestionOrder == null)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Question order must is numeric!" });
                    break;
                }
            }
            return ex;
        }
    }
}
