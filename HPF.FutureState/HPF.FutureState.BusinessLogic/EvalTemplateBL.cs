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
        public EvalSectionCollectionDTO RetrivEvalSectionByTemplateId(int? evalTemplateId)
        {
            return EvalTemplateDAO.Instance.GetEvalSectionByTemplateId(evalTemplateId);
        }
        public int? InsertEvalSection(EvalSectionDTO evalSection)
        {
            DataValidationException ex = new DataValidationException();
            if (string.IsNullOrEmpty(evalSection.SectionName))
            {
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1107, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1107) });
                throw ex;
            }
            return EvalTemplateDAO.Instance.InsertEvalSection(evalSection);
        }
        public void UpdateEvalSection(EvalSectionDTO evalSection)
        {
            var ex = ValidateSection(evalSection);
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
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
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            EvalTemplateDAO.Instance.UpdateEvalQuestion(evalQuestion);
        }
        private DataValidationException ValidateSection(EvalSectionDTO evalSection)
        {
            DataValidationException ex = new DataValidationException();
            if (evalSection.IsInUse!=null && evalSection.IsInUse && (evalSection.ActiveInd == Constant.INDICATOR_NO))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1108, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1108) });
            if (string.IsNullOrEmpty(evalSection.SectionName))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1107, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1107) });
            return ex;
        }
        private DataValidationException ValidateQuestion(EvalQuestionDTO evalQuestion)
        {
            DataValidationException ex = new DataValidationException();
            if (evalQuestion.IsInUse && (evalQuestion.ActiveInd == Constant.INDICATOR_NO))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1109, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1109) });
            if (string.IsNullOrEmpty(evalQuestion.Question))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1110, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1110) });
            if (string.IsNullOrEmpty(evalQuestion.QuestionType))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1111, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1111) });
            return ex;
        }
        public int? InsertEvalTemplate(EvalTemplateDTO evalTemplate)
        {
            var ex = ValidateTemplate(evalTemplate);
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            return EvalTemplateDAO.Instance.InsertEvalTemplate(evalTemplate);
        }
        public void UpdateEvalTemplate(EvalTemplateDTO evalTemplate)
        {
            var ex = ValidateTemplate(evalTemplate);
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            EvalTemplateDAO.Instance.UpdateEvalTemplate(evalTemplate);
        }
        private DataValidationException ValidateTemplate(EvalTemplateDTO evalTemplate)
        {
            DataValidationException ex = new DataValidationException();
            if (evalTemplate.IsInUse && (evalTemplate.ActiveInd == Constant.INDICATOR_NO))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1113, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1113) });
            if (string.IsNullOrEmpty(evalTemplate.TemplateName))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1112, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1112) });
            return ex;
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
                    if (section.StatusChanged == (byte)StatusChanged.Insert)
                        instance.InsertEvalTemplateSection(section);
                    else if (section.StatusChanged == (byte)StatusChanged.Update)
                        instance.UpdateEvalTemplateSection(section);
                    else
                        instance.RemoveEvalTemplateSection(section);
                }
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Rollback();
                throw ex;
            }
            finally
            {
                instance.CloseConnection();
            }
        }
        public DataValidationException ValidateTemplateSection(EvalTemplateSectionDTOCollection sectionCollection)
        {
            DataValidationException ex = new DataValidationException();
            foreach (EvalTemplateSectionDTO section in sectionCollection)
                if (section.SectionOrder == null)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1114, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1114) });
                    break;
                }
            if (ex.ExceptionMessages.Count > 0) return ex;
            //Check duplicate section order
            for (int i=0;i<sectionCollection.Count;i++)
                for (int j = i+1; j < sectionCollection.Count; j++)
                {
                    if (sectionCollection[i].SectionOrder == sectionCollection[j].SectionOrder)
                    {
                        ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1117, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1117) });
                        break;
                    }
                }
            return ex;
        }

        public EvalSectionQuestionDTOCollection RetriveAllEvalQuestionByTemplateId(int? evalTemplateId)
        {
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            return instance.GetEvalTemplateQuestionByTemplateId(evalTemplateId);
        }
        public void ManageEvalSectionQuestion(EvalSectionQuestionDTOCollection questionCollection,string loginName)
        {
            int totalScore = 0;
            if (questionCollection.Count==0) return;
            var exValidate = ValidateSectionQuestion(questionCollection);
            if (exValidate.ExceptionMessages.Count > 0) throw exValidate;
            var instance = EvalTemplateSectionQuestionDAO.CreateInstance();
            try
            {
                instance.Begin();
                foreach (EvalSectionQuestionDTO question in questionCollection)
                {
                    if (question.StatusChanged == (byte)StatusChanged.Insert)
                    {
                        totalScore += (int)question.EvalQuestion.QuestionScore;
                        instance.InsertEvalQuestionSection(question);
                    }
                    else if (question.StatusChanged == (byte)StatusChanged.Update)
                    {
                        totalScore += (int)question.EvalQuestion.QuestionScore;
                        instance.UpdateEvalSectionQuestion(question);
                    }
                    else
                        instance.RemoveEvalSectionQuestion(question);
                }
                EvalTemplateDTO evalTemplate = new EvalTemplateDTO();
                evalTemplate.EvalTemplateId = questionCollection[0].EvalTemplateId;
                evalTemplate.TotalScore = totalScore;
                evalTemplate.SetUpdateTrackingInformation(loginName);
                instance.UpdateEvalTemplateTotalScore(evalTemplate);
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Rollback();
                throw ex;
            }
            finally
            {
                instance.CloseConnection();
            }
        }
        public DataValidationException ValidateSectionQuestion(EvalSectionQuestionDTOCollection questionCollection)
        {
            DataValidationException ex = new DataValidationException();
            foreach (EvalSectionQuestionDTO question in questionCollection)
            {
                if (question.EvalSectionId <=0)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1115, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1115) });
                    break;
                }
                if (question.QuestionOrder == null)
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1116, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1116) });
                    break;
                }
            }
            if (ex.ExceptionMessages.Count > 0) return ex;
            //Check duplicate order of questions in the same section
            for (int i=0;i<questionCollection.Count;i++)
                for (int j = i + 1; j < questionCollection.Count; j++)
                {
                    if (questionCollection[i].QuestionOrder == questionCollection[j].QuestionOrder
                        && questionCollection[i].EvalSectionId == questionCollection[j].EvalSectionId)
                    {
                        ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = ErrorMessages.ERR1118, Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1118) });
                        break;
                    }
                }
            return ex;
        }
    }
}
