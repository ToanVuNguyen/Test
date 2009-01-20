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
    public class OutcomeItemBL : BaseBusinessLogic
    {
        private static readonly OutcomeItemBL instance = new OutcomeItemBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeItemBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeItemBL()
        {

        }

        public OutcomeItemDTOCollection RetrieveOutcomeItems(int fcId)
        {
            return OutcomeItemDAO.Instance.RetrieveOutcomeItems(fcId);
        }

        public bool DeleteOutcomeItem(int outcomeItemId, string workingUserId)
        {
            OutcomeItemDTO item = new OutcomeItemDTO();
            item.OutcomeItemId = outcomeItemId;
            item.SetUpdateTrackingInformation(workingUserId);
            return OutcomeItemDAO.Instance.DeleteOutcomeItem(item);
        }

        public bool InstateOutcomeItem(int outcomeItemId, string workingUserId)
        {
            OutcomeItemDTO item = new OutcomeItemDTO();
            item.OutcomeItemId = outcomeItemId;
            item.SetUpdateTrackingInformation(workingUserId);
            return OutcomeItemDAO.Instance.InstateOutcomeItem(item);
        }
        

    }
}
