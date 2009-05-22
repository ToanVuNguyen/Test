using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class ServicerBL : BaseBusinessLogic
    {
        private static readonly ServicerBL instance = new ServicerBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ServicerBL Instance
        {
            get
            {
                return instance;
            }
        }
        
        /// <summary>
        /// Get servicer information with servicer id supplied
        /// </summary>
        /// <param name="servicerId"></param>
        /// <returns></returns>
        public ServicerDTO GetServicer(int servicerId)
        {
            return ServicerDAO.Instance.GetServicer(servicerId);            
        }

        /// <summary>
        /// Get All servicers
        /// </summary>
        /// <returns></returns>
        public ServicerDTOCollection GetServicers()
        {
            return ServicerDAO.Instance.GetServicers();
        }
    }
}
