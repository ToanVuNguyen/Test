using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class EventBL:BaseBusinessLogic
    {
        private static readonly EventBL instance = new EventBL();
        private string _workingUserID;
        /// <summary>
        /// Singleton
        /// </summary>
        public static EventBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected EventBL()
        {
        }
        public ExceptionMessageCollection WarningMessage { get; private set; }
        /// <summary>
        /// Insert or update an event
        /// </summary>
        /// <param name="anEvent"></param>
        /// <returns></returns>
        public EventDTO SaveEvent(EventDTO anEvent, int? currentAgencyId)
        {
            int? eventId;
            if (anEvent == null)
                ThrowDataValidationException(ErrorMessages.ERR1210);
            var exceptionList = CheckRequiredFields(anEvent);
            exceptionList.Add(CheckInvalidFormatData(anEvent));
            exceptionList.Add(CheckInvalidCodes(anEvent));
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
            ForeclosureCaseDTO fc = LoadForeclosureCaseFromDB(anEvent.FcId);
            if ((fc == null) || (fc.AgencyId != currentAgencyId))
                ThrowDataValidationException(ErrorMessages.ERR1213);
            
            ProgramStageDTO programStage = LoadProgramStageFromDB(anEvent.ProgramStageId);
            if (programStage == null)
                ThrowDataValidationException(ErrorMessages.ERR1214);
            if (fc.ProgramId != programStage.ProgramId)
                ThrowDataValidationException(ErrorMessages.ERR1215);
            
            LoadEventFromDB(anEvent);
            _workingUserID = anEvent.ChgLstUserId;
            if (anEvent.EventId.HasValue)
                UpdateEvent(anEvent);
            else
            {
                eventId = InsertEvent(anEvent);
                anEvent.EventId = eventId;
            }
            
            return anEvent;
        }
                
        /// <summary>
        /// Check all fields are required by event
        /// </summary>
        /// <param name="anEvent"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckRequiredFields(EventDTO anEvent)
        {
            return ValidateFieldsByRuleSet(anEvent, Constant.RULESET_MIN_REQUIRE_FIELD);
        }
        /// <summary>
        /// Check data input format
        /// </summary>
        /// <param name="anEvent"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckInvalidFormatData(EventDTO anEvent)
        {
            return ValidateFieldsByRuleSet(anEvent, Constant.RULESET_LENGTH);
        }
        /// <summary>
        /// Check to make sure codes are valid
        /// </summary>
        /// <param name="anEvent"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckInvalidCodes(EventDTO anEvent)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            ExceptionMessageCollection msgEventSet = new ExceptionMessageCollection();
            if (!referenceCode.Validate(ReferenceCode.EVENT_TYPE_CODE, anEvent.EventTypeCd))
                msgEventSet.AddExceptionMessage(ErrorMessages.ERR1211, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1211));
            if (!referenceCode.Validate(ReferenceCode.EVENT_OUTCOME_CODE, anEvent.EventOutcomeCd))
                msgEventSet.AddExceptionMessage(ErrorMessages.ERR1212, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1212));
            return msgEventSet;
        }
        private ExceptionMessageCollection ValidateFieldsByRuleSet(EventDTO anEvent, string ruleSet)
        {
            var msgEventSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(anEvent, ruleSet) };
            return msgEventSet;
        }

        private void LoadEventFromDB(EventDTO anEvent)
        {
            if (!anEvent.EventId.HasValue) return;
            EventDTO anEventDB = EventDAO.Instance.GetEvent(anEvent.EventId);
            if (anEventDB == null)
                ThrowDataValidationException(ErrorMessages.ERR1217);
            if (anEvent.FcId != anEventDB.FcId)
                ThrowDataValidationException(ErrorMessages.ERR1218,ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1218,anEvent.EventId,anEvent.FcId));
        }

        private ForeclosureCaseDTO LoadForeclosureCaseFromDB(int? fcId)
        {
            ForeclosureCaseSetDAO foreclosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            return foreclosureCaseSetDAO.GetForeclosureCase(fcId);
        }
        private ProgramStageDTO LoadProgramStageFromDB(int? programStageId)
        {
            return EventDAO.Instance.GetProgramStage(programStageId);
        }
        private EventDTO GetEvent(int? eventId)
        {
            return EventDAO.Instance.GetEvent(eventId);
        }
        private int? InsertEvent(EventDTO anEvent)
        {
            anEvent.SetInsertTrackingInformation(_workingUserID);
            return EventDAO.Instance.InsertEvent(anEvent);
        }
        private void UpdateEvent(EventDTO anEvent)
        {
            anEvent.SetUpdateTrackingInformation(_workingUserID);
            EventDAO.Instance.UpdateEvent(anEvent);
        }
        #region Throw Detail Exception
        private void ThrowDataValidationException(string errorCode)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, ErrorMessages.GetExceptionMessageCombined(errorCode));
            throw ex;
        }
        private void ThrowDataValidationException(string errorCode, string errorMessage)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, errorMessage);
            throw ex;
        }
        private static void ThrowDataValidationException(ExceptionMessageCollection exDetailCollection)
        {
            var ex = new DataValidationException(exDetailCollection);
            throw ex;
        }
        #endregion
    }
}
