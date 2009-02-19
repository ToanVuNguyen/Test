using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerDTOCollection : BaseDTOCollection<ServicerDTO>
    {
        /// <summary>
        /// Get servicer by their delivery method.
        /// </summary>
        /// <param name="deliveryMethod"></param>
        /// <returns></returns>
        public ServicerDTOCollection ExtractServicerByDeliveryMethod(string deliveryMethod)
        {
            var returnValue = new ServicerDTOCollection();
            var result = this.Where(c => c.SecureDeliveryMethodCd == deliveryMethod);
            foreach (var servicer in result)
            {
                returnValue.Add(servicer);
            }
            return returnValue;
        }

        public ServicerDTO GetServicerById(int? servicerId)
        {
            return this.SingleOrDefault(i => i.ServicerID == servicerId);
        }
    }
}
