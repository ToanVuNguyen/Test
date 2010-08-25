using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using System.Xml;

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
            return EvalTemplateDAO.Instance.InsertEvalSection(evalSection);
        }
        public void UpdateEvalSection(EvalSectionDTO evalSection)
        {
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
