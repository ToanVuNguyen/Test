using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class ForclosureCaseBL : BaseBusinessLogic, IForclosureCaseBL
    {
        private static readonly ForclosureCaseBL instance = new ForclosureCaseBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForclosureCaseBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected ForclosureCaseBL()
        {
            
        }

        #region Implementation of IForclosureCaseBL

        /// <summary>
        /// Save a ForeClosureCase
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeClosureCaseSetDTO</param>
        public void SaveForeClosureCaseSet(ForeClosureCaseSetDTO foreClosureCaseSet)
        {
            //Validation here     
       
            var foreClosureCaseSetDAO = ForeClosureCaseSetDAO.CreateInstance();
            //
            try
            {
                foreClosureCaseSetDAO.Begin();
                //
                //Business process here
                //
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {                
                foreClosureCaseSetDAO.Cancel();
                throw;
            }            
        }

        #endregion
    }
}
