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
            if (evalSection.InUse && (evalSection.ActiveInd == Constant.INDICATOR_NO))
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
            return EvalTemplateDAO.Instance.InsertEvalQuestion(evalQuestion);
        }
        public void UpdateEvalQuestion(EvalQuestionDTO evalQuestion)
        {
            EvalTemplateDAO.Instance.UpdateEvalQuestion(evalQuestion);
        }
    }
}
