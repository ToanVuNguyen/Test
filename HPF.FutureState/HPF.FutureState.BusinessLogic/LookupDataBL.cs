using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace HPF.FutureState.BusinessLogic
{
    public class LookupDataBL : BaseBusinessLogic
    {
        private static readonly LookupDataBL instance = new LookupDataBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static LookupDataBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected LookupDataBL()
        {
            
        }
        /// <summary>
        /// Get Program Name and Program ID to display in DDLB
        /// </summary>
        /// <returns>ProgramDTOCollection containts all Programs</returns>
        public ProgramDTOCollection GetProgram()
        {
            ProgramDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetProgram();
            return result;
        }
        /// <summary>
        /// Get State Name and State ID to display in DDLB
        /// </summary>
        /// <returns>StateDTOCollection containts all State</returns>
        public StateDTOCollection GetState()
        {
            StateDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetState();
            return result;
        }
        /// <summary>
        /// Get Agency Name and Agency ID to display in DDLB
        /// </summary>
        /// <returns>AgencyDTOCollection containts all Agency</returns>
        public AgencyDTOCollection GetAgency()
        {
            AgencyDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetAgency();
            return result;
        }
        /// <summary>
        /// Get Funding Source ID and Funding Source Name to display in DDLB
        /// </summary>
        /// <returns>FundingSourceDTOCollection containts all FundingSource</returns>
        public FundingSourceDTOCollection GetFundingSource()
        {
            FundingSourceDTOCollection result = InvoiceDAO.CreateInstance().AppGetFundingSource();
            return result;

        }
        /// <summary>
        /// Get Servicer Name from FundingSourceId
        /// </summary>
        /// <param name="fundingSourceId"></param>
        /// <returns></returns>
        public ServicerDTOCollection GetServicerByFundingSourceId(int fundingSourceId)
        {
            ServicerDTOCollection result = InvoiceDAO.CreateInstance().AppGetServicerByFundingSourceId(fundingSourceId);
            return result;
        }


    }
}
