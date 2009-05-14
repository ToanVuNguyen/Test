using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common;

using System.Collections.Generic;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class OutcomeBL : BaseBusinessLogic
    {
        private static readonly OutcomeBL instance = new OutcomeBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeBL()
        {

        }

        public OutcomeItemDTOCollection RetrieveOutcomeItems(int fcId)
        {
            return OutcomeDAO.Instance.RetrieveOutcomeItems(fcId);
        }

        public bool DeleteOutcomeItem(int? outcomeItemId, string workingUserId)
        {
            OutcomeItemDTO item = new OutcomeItemDTO();
            item.OutcomeItemId = outcomeItemId;
            item.SetUpdateTrackingInformation(workingUserId);
            return OutcomeDAO.Instance.DeleteOutcomeItem(item);
        }

        public bool InstateOutcomeItem(int? outcomeItemId, string workingUserId)
        {
            OutcomeItemDTO item = new OutcomeItemDTO();
            item.OutcomeItemId = outcomeItemId;
            item.SetUpdateTrackingInformation(workingUserId);
            return OutcomeDAO.Instance.InstateOutcomeItem(item);
        }

        public OutcomeTypeDTOCollection GetOutcomeType()
        {
            return OutcomeDAO.Instance.GetOutcomeType();
        }        

        public bool DeleteOutcomeItem(OutcomeItemDTO outcomeItem)
        {
            return OutcomeDAO.Instance.DeleteOutcomeItem(outcomeItem);
        }

        public bool InstateOutcomeItem(OutcomeItemDTO outcomeItem)
        {
            return OutcomeDAO.Instance.InstateOutcomeItem(outcomeItem);
        }
        public OutcomeItemDTOCollection GetOutcomeItemCollection(int? fcId)
        {
            return OutcomeDAO.Instance.GetOutcomeItemCollection(fcId);
        }
    }
}
